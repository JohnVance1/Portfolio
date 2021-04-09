#include "Entity.h"

Entity::Entity(Mesh* mesh, Material* material)
{
	this->mesh  = mesh;
	this->material = material;
}

Entity::Entity(Mesh* mesh, Material* material, DirectX::XMFLOAT3 _position, DirectX::XMFLOAT3 _rotation)
{
	this->mesh = mesh;
	this->material = material;
	this->transform = Transform(_position);
	this->transform.SetRotation(_rotation.x, _rotation.y, _rotation.z);
}

Entity::~Entity()
{
	delete mesh;
	delete material;
}

void Entity::Draw()
{

}

void Entity::Draw(Microsoft::WRL::ComPtr<ID3D11DeviceContext> context, SimpleVertexShader* vs, SimplePixelShader* ps, Camera* camera)
{
	vs->SetFloat4("colorTint", DirectX::XMFLOAT4(1.0f, 1.0f, 1.0f, 1.0f));
	vs->SetMatrix4x4("world", transform.GetWorldMatrix());
	vs->SetMatrix4x4("view", camera->GetView());
	vs->SetMatrix4x4("projection", camera->GetProjection());

	vs->CopyAllBufferData();

	mesh->SetBuffersAndDraw(context);

}

Mesh* Entity::GetMesh()
{
	return mesh;
}

Transform* Entity::GetTransform()
{
	return &transform;
}

Material* Entity::GetMaterial()
{
	return material;
}
