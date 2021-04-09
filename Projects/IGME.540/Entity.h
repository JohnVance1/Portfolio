#pragma once
#include "DXCore.h"
#include <DirectXMath.h>
#include "Transform.h"
#include "Mesh.h"
#include "Material.h"
#include "Camera.h"
#include <DirectXMath.h>

class Entity
{
	Transform transform;
	Mesh* mesh;
	Material* material;

public:
	Entity(Mesh* mesh, Material* material);
	Entity(Mesh* mesh, Material* material, DirectX::XMFLOAT3 _position, DirectX::XMFLOAT3 _rotation);
	~Entity();

	void Draw();
	void Draw(Microsoft::WRL::ComPtr<ID3D11DeviceContext> context, SimpleVertexShader* vs, SimplePixelShader* ps, Camera* camera);


	Mesh* GetMesh();
	Transform* GetTransform();
	Material* GetMaterial();
};

