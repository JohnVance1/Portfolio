#pragma once
#include "DXCore.h"
#include <DirectXMath.h>
using namespace DirectX;

struct DirectionalLight
{
	XMFLOAT3 ambientColor;
	float padding1;
	XMFLOAT3 diffuseColor;
	float padding2;
	XMFLOAT3 direction;
};

struct PointLight
{
	XMFLOAT3 ambientColor;
	float padding1;
	XMFLOAT3 diffuseColor;
	float padding2;
	XMFLOAT3 position;
};