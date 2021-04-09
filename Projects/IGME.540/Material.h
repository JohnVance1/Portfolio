#pragma once
#include "DXCore.h"
#include <DirectXMath.h>
#include <wrl/client.h>
#include <Windows.h>
#include "SimpleShader.h"

class Material
{
	DirectX::XMFLOAT4 colorTint;

	SimplePixelShader* pixelShader;
	SimpleVertexShader* vertexShader;

	float specularIntensity;

	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> srv;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> normalMap;
	Microsoft::WRL::ComPtr<ID3D11SamplerState> samplerState;

public:
	Material(
		SimplePixelShader* ps,
		SimpleVertexShader* vs,
		DirectX::XMFLOAT4 colorTint,
		ID3D11ShaderResourceView* srv,
		ID3D11SamplerState* samplerState);
	Material(
		SimplePixelShader* ps,
		SimpleVertexShader* vs,
		DirectX::XMFLOAT4 colorTint,
		float specularIntensity,
		ID3D11ShaderResourceView* srv,
		ID3D11SamplerState* samplerState);
	Material(
		SimplePixelShader* ps,
		SimpleVertexShader* vs,
		DirectX::XMFLOAT4 colorTint,
		float specularIntensity,
		ID3D11ShaderResourceView* srv,
		ID3D11ShaderResourceView* normalMap,
		ID3D11SamplerState* samplerState);

	void SetColorTint(DirectX::XMFLOAT4 tint);

	DirectX::XMFLOAT4 GetColorTint();
	SimplePixelShader* GetPixelShader();
	SimpleVertexShader* GetVertexShader();
	float GetSpecularIntensity();
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> GetSRV();
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> GetNormalMap();
	Microsoft::WRL::ComPtr<ID3D11SamplerState> GetSamplerState();
};

