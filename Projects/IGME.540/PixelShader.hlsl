#include "ShaderIncludes.hlsli"


cbuffer ExternalData : register(b0)
{
	DirectionalLight dLight1;

	PointLight pLight1;

	float specInt;

	float3 cameraPosition;
}


// Texture-related resources
Texture2D diffuseTexture	: register(t0);
SamplerState samplerOptions : register(s0);



// Calculates the diffuse given the surface normal and the direction of the light
float Diffuse(float3 normal, float3 lightDir)
{
	// we need the direction to the light so we get the negative normal of the light's direction
	return saturate(dot(normal, -normalize(lightDir)));
}


float SpecularPhong(float3 normal, float3 lightDir, float3 toCamera, float specExp)
{
	// reflection vector
	float3 refl = reflect(normalize(lightDir), normal);

	// 1. Get the dot product between the perfect reflection of the light
	//    and the vector to the camera, as that tells us how "close" we 
	//    are to seeing the perfect reflection.  
	// 2. Saturate to ensure it doesn't go negative on the back of the object
	// 3. Raise to a power to create a very quick "falloff" (so the highlight
	//    appears smaller if the object is shinier)
	return pow(saturate(dot(refl, toCamera)), specExp);
}


float4 GetFinalColorDir(DirectionalLight directionalLight, float3 surfaceNormal, float3 surfaceColor)
{
	// Calculate the final pixel color
	float3 finalColor = 
		Diffuse(surfaceNormal, directionalLight.Direction) * directionalLight.DiffuseColor * surfaceColor +
		directionalLight.AmbientColor * surfaceColor;

	return float4(finalColor, 1);
}


float4 GetFinalColorPoint(PointLight pointLight, float3 worldPos, float3 surfaceNormal, float3 surfaceColor)
{
	// Calculate the final pixel color
	float3 finalColor =
		Diffuse(surfaceNormal, worldPos - pointLight.Position) * pointLight.DiffuseColor * surfaceColor +
		pointLight.AmbientColor * surfaceColor;

	return float4(finalColor, 1);
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
float4 main(VertexToPixel input) : SV_TARGET
{
	input.normal = normalize(input.normal);

	float3 surfaceColor = diffuseTexture.Sample(samplerOptions, input.uv).rgb;
	surfaceColor *= input.color.rgb;

	// Calculate the vector from the pixel's world position to the camera
	float3 toCamera = normalize(cameraPosition - input.worldPos);

	// Directional Light
	float diffuse = Diffuse(input.normal, dLight1.Direction);
	float spec = SpecularPhong(input.normal, dLight1.Direction, toCamera, specInt * 64.0f);

	spec *= any(diffuse);

	float3 finalDLColor =
		diffuse * dLight1.DiffuseColor * surfaceColor +
		spec * dLight1.DiffuseColor;


	// Point Light
	float3 pointLightDirection = normalize(input.worldPos - pLight1.Position);
	float pl_diffuse = Diffuse(input.normal, pointLightDirection);
	float pl_spec = SpecularPhong(input.normal, pointLightDirection, toCamera, specInt * 64.0f);

	pl_spec *= any(pl_diffuse);

	float3 finalPLColor =
		pl_diffuse * pLight1.DiffuseColor * surfaceColor +
		pl_spec * pLight1.DiffuseColor;


	float3 ambientColor =
		dLight1.AmbientColor * surfaceColor +
		pLight1.AmbientColor * surfaceColor;

	float3 totalLight = finalDLColor + finalPLColor + ambientColor;


	return float4(totalLight, 1);

	//float4 directionalLightColor =
	//	GetFinalColorDir(dLight1, input.normal, surfaceColor) +
	//	SpecularPhong(input.normal, dLight1.Direction, toCamera, specInt * 64.0f); //+
	//	//GetFinalColorDir(dLight2, input.normal, input.color) +
	//	//GetFinalColorDir(dLight3, input.normal, input.color);

	//float4 pointLightColor =
	//	GetFinalColorPoint(pLight1, input.worldPos, input.normal, surfaceColor) +
	//	SpecularPhong(input.normal, input.worldPos - pLight1.Position, toCamera, specInt * 64.0f);

	//return directionalLightColor + pointLightColor;
	//return pointLightColor;
}

