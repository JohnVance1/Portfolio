
struct VertexToPixel
{
	float4 position		: SV_POSITION;	// XYZW position (System Value Position)
	float3 cubeSampleDir : DIRECTION;
};

// Texture reqs
TextureCube sky : register(t0);
SamplerState samplerOptions : register(s0);


float4 main(VertexToPixel input) : SV_TARGET
{
	return sky.Sample(samplerOptions, input.cubeSampleDir);
}