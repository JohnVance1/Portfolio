#include "ShaderIncludes.hlsli"


cbuffer externalData : register(b0)
{
	float3 environmentAmbientColor;

	float3 lightDirection;
	float3 lightColor;
	float lightIntensity;

	float3 pointLightPos;
	float pointLightRange;
	float3 pointLightColor;
	float pointLightIntensity;


	float3 cameraPosition;

	float uvScale0;
	float uvScale1;
	float uvScale2;

	float specularAdjust;
}

// Texture-related resources
Texture2D blendMap			: register(t0);

Texture2D texture0			: register(t1);
Texture2D texture1			: register(t2);
Texture2D texture2			: register(t3);

Texture2D normalMap0		: register(t4);
Texture2D normalMap1		: register(t5);
Texture2D normalMap2		: register(t6);

SamplerState samplerOptions : register(s0);


// Struct representing the data we expect to receive from earlier pipeline stages
//struct VertexToPixel
//{
//	float4 position		: SV_POSITION;	// XYZW position (System Value Position)
//	float4 color		: COLOR;        // RGBA color
//	float2 uv			: TEXCOORD;
//	float3 normal		: NORMAL;
//	float3 tangent		: TANGENT;
//	float3 worldPos		: POSITION;
//
//
//};

// Range-based attenuation function
float Attenuate(float3 lightPos, float lightRange, float3 worldPos)
{
	float dist = distance(lightPos, worldPos);

	// Ranged-based attenuation
	float att = saturate(1.0f - (dist * dist / (lightRange * lightRange)));

	// Soft falloff
	return att * att;
}

// Calculates the diffuse (Lambert) light amount, given a
// surface normal and a direction FROM the light
// Note: This function will "reverse" the light direction
//       so we're comparing the direction back TO the light
float Diffuse(float3 normal, float3 lightDir)
{
	// Ensure the light direction is normalized!
	// Note: It would be optimal to do this once and pass in an
	//       already-normalized vector than potentailly have
	//       to do it over and over for each light
	return saturate(dot(normal, -normalize(lightDir)));
}

// Calculates the specular reflection using the Phong lighting
// model, given a surface normal, a direction FROM the light,
// a direction TO the camera (from the surface) and a specular 
// exponent value (the shininess of the surface)
float SpecularPhong(float3 normal, float3 lightDir, float3 toCamera, float specExp)
{
	// Calculate light reflection vector
	float3 refl = reflect(normalize(lightDir), normal);

	// 1. Get the dot product between the perfect reflection of the light
	//    and the vector to the camera, as that tells us how "close" we 
	//    are to seeing the perfect reflection.  
	// 2. Saturate to ensure it doesn't go negative on the back of the object
	// 3. Raise to a power to create a very quick "falloff" (so the highlight
	//    appears smaller if the object is shinier)
	return pow(saturate(dot(refl, toCamera)), specExp);
}

// --------------------------------------------------------
// The entry point (main method) for our pixel shader
// 
// - Input is the data coming down the pipeline (defined by the struct)
// - Output is a single color (float4)
// - Has a special semantic (SV_TARGET), which means 
//    "put the output of this into the current render target"
// - Named "main" because that's the default the shader compiler looks for
// --------------------------------------------------------
float4 main(VertexToPixelNormalMap input) : SV_TARGET
{
	// Re-normalize interpolated normals!
	input.normal = normalize(input.normal);
	input.tangent = normalize(input.tangent);

	// Sample the texture, using the provided options, as
	// the specified UV coordinates
	float4 blend = blendMap.Sample(samplerOptions, input.uv);

	float4 color0 = texture0.Sample(samplerOptions, input.uv * uvScale0);
	float4 color1 = texture1.Sample(samplerOptions, input.uv * uvScale1);
	float4 color2 = texture2.Sample(samplerOptions, input.uv * uvScale2);

	float3 normalFromMap0 = normalMap0.Sample(samplerOptions, input.uv * uvScale0).rgb * 2 - 1;
	float3 normalFromMap1 = normalMap1.Sample(samplerOptions, input.uv * uvScale1).rgb * 2 - 1;
	float3 normalFromMap2 = normalMap2.Sample(samplerOptions, input.uv * uvScale2).rgb * 2 - 1;

	// Blend the textures together
	float4 surfaceColor =
		color0 * blend.r +
		color1 * blend.g +
		color2 * blend.b;
	

	float3 normalFromMap =
		normalize(
			normalFromMap0 * blend.r +
			normalFromMap1 * blend.g +
			normalFromMap2 * blend.b);

	

	// Create the three vectors for normal mapping
	float3 N = input.normal;
	float3 T = normalize(input.tangent - N * dot(input.tangent, N)); // Orthogonalize!
	float3 B = cross(T, N);
	float3x3 TBN = float3x3(T, B, N);
	
	// Adjust and overwrite the existing normal
	input.normal = normalize(mul(normalFromMap, TBN));
	
	// Calculate the vector from the pixel's world position to the camera
	float3 toCamera = normalize(cameraPosition - input.worldPos);

	// DIRECTIONAL LIGHT ---------------------------

	// Perform the diffuse light calculation
	float diffuse = Diffuse(input.normal, lightDirection);

	// Perform the specular calculation
	float spec = SpecularPhong(input.normal, lightDirection, toCamera, 64.0f) * specularAdjust;

	// Cut the specular if the diffuse contribution is zero
	// - The any() function returns 1 if any component of the parameter is non-zero
	// - In this case, our parameter has one component, so it returns 1 if the param is non-zero
	// - In other words: 
	//     - If the diffuse amount is zero, any(diffuse) returns zero
	//     - If the diffuse amount is != 0, any(diffuse) returns one
	//     - So when diffuse is zero, specular becomes zero due to the multiply
	spec *= any(diffuse);

	// Combine the diffuse and specular
	// Note: There's no ambient here, though you could
	//       add a very small portion of the light's color
	//       as an ambient, since that light is theoretically
	//       bouncing around the scene.
	// Feel free to simplify the math here
	// NOTE: Explicitely NOT tinting specular by surface color here
	float3 finalDLColor =
		diffuse * lightIntensity * lightColor * surfaceColor +
		spec * lightIntensity * lightColor;

	// POINT LIGHT ------------------------------------
	float3 pointLightDirection = normalize(input.worldPos - pointLightPos);
	float pl_diffuse = Diffuse(input.normal, pointLightDirection);
	float pl_spec = SpecularPhong(input.normal, pointLightDirection, toCamera, 64.0f) * specularAdjust;

	// Cut the specular if the diffuse contribution is zero
	// - The any() function returns 1 if any component of the parameter is non-zero
	// - In this case, our parameter has one component, so it returns 1 if the param is non-zero
	// - In other words: 
	//     - If the diffuse amount is zero, any(diffuse) returns zero
	//     - If the diffuse amount is != 0, any(diffuse) returns one
	//     - So when diffuse is zero, specular becomes zero due to the multiply
	pl_spec *= any(pl_diffuse);
	
	
	// Combine the diffuse and specular
	// Note: There's no ambient here, though you could
	//       add a very small portion of the light's color
	//       as an ambient, since that light is theoretically
	//       bouncing around the scene.
	// Feel free to simplify the math here
	// NOTE: Explicitely NOT tinting specular by surface color here
	float3 finalPLColor =
		pl_diffuse * pointLightIntensity * pointLightColor * surfaceColor +
		pl_spec * pointLightIntensity * pointLightColor;
	
	// Attenuation
	float att = Attenuate(pointLightPos, pointLightRange, input.worldPos);
	finalPLColor *= att;

	// Final combine =====================================

	// Combine each light, as well as the environment's ambient color
	float3 totalLight = environmentAmbientColor * surfaceColor + finalDLColor + finalPLColor;

	// The light tints the surface color, which means we're multiplying!
	// Note: Surface color contribution has been moved above
	return float4(totalLight, 1);
}