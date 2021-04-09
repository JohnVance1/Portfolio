#include "Material.h"

Material::Material(SimplePixelShader* ps, SimpleVertexShader* vs, DirectX::XMFLOAT4 colorTint, ID3D11ShaderResourceView* srv, ID3D11SamplerState* samplerState)
{
	pixelShader = ps;
	vertexShader = vs;
	this->colorTint = colorTint;
	specularIntensity = 0.5f;
	this->srv = srv;
	this->samplerState = samplerState;
	normalMap = nullptr;
}

Material::Material(SimplePixelShader* ps, SimpleVertexShader* vs, DirectX::XMFLOAT4 colorTint, float specularIntensity, ID3D11ShaderResourceView* srv, ID3D11SamplerState* samplerState)
{
	pixelShader = ps;
	vertexShader = vs;
	this->colorTint = colorTint;
	this->specularIntensity = specularIntensity;
	this->srv = srv;
	this->samplerState = samplerState;
	normalMap = nullptr;
}

Material::Material(SimplePixelShader* ps, SimpleVertexShader* vs, DirectX::XMFLOAT4 colorTint, float specularIntensity, ID3D11ShaderResourceView* srv, ID3D11ShaderResourceView* normalMap, ID3D11SamplerState* samplerState)
{
	pixelShader = ps;
	vertexShader = vs;
	this->colorTint = colorTint;
	this->specularIntensity = specularIntensity;
	this->srv = srv;
	this->samplerState = samplerState;
	this->normalMap = normalMap;
}

void Material::SetColorTint(DirectX::XMFLOAT4 tint)
{
	colorTint = tint;
}

DirectX::XMFLOAT4 Material::GetColorTint()
{
	return colorTint;
}

SimplePixelShader* Material::GetPixelShader()
{
	return pixelShader;
}

SimpleVertexShader* Material::GetVertexShader()
{
	return vertexShader;
}

float Material::GetSpecularIntensity()
{
	return specularIntensity;
}

Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> Material::GetSRV()
{
	return srv;
}

Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> Material::GetNormalMap()
{
	return normalMap;
}

Microsoft::WRL::ComPtr<ID3D11SamplerState> Material::GetSamplerState()
{
	return samplerState;
}
