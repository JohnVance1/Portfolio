

cbuffer ExternalData : register(b0)
{
	matrix view;
	matrix proj;
}

// Struct representing a single vertex worth of data
struct VertexShaderInput
{ 
	// Data type
	//  |
	//  |   Name          Semantic
	//  |    |                |
	//  v    v                v
	float3 position		: POSITION;     // XYZ position
	float2 uv			: TEXCOORD;
	float3 normal		: NORMAL;
	float3 tangent		: TANGENT;
};

// Struct representing the data we're sending down the pipeline
struct VertexToPixel
{
	// Data type
	//  |
	//  |   Name          Semantic
	//  |    |                |
	//  v    v                v
	float4 position		: SV_POSITION;	// XYZW position (System Value Position)
	float3 cubeSampleDir : DIRECTION;
};

// --------------------------------------------------------
// The entry point (main method) for our vertex shader
// --------------------------------------------------------
VertexToPixel main( VertexShaderInput input )
{
	// Set up output struct
	VertexToPixel output;

	// Adjust the view matrix by removing translation
	matrix viewNoTranslation = view;
	viewNoTranslation._14 = 0;
	viewNoTranslation._24 = 0;
	viewNoTranslation._34 = 0;

	// Modifying the position using the provided transformation (world) matrix
	matrix vp = mul(proj, viewNoTranslation);
	output.position = mul(vp, float4(input.position, 1.0f));

	// Set the Z to the W, so the perspective divide 
	// (which happens later) will result in 1 (aka the far clip plane)
	output.position.z = output.position.w;


	// Sample in the direction of the vertex
	output.cubeSampleDir = input.position;


	// Whatever we return will make its way through the pipeline to the
	// next programmable stage we're using (the pixel shader for now)
	return output;
}