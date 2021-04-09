#include "Transform.h"

using namespace DirectX;


// Transform Constructor
// Initializes the transformation values
Transform::Transform()
{
	position = { 0, 0, 0 };
	rotation = { 0, 0, 0 };
	scale = { 1, 1, 1 };
	XMStoreFloat4x4(&world, XMMatrixIdentity());

	verticalForce = 0.02f;
}

Transform::Transform(DirectX::XMFLOAT3 _position)
{
	position = _position;
	rotation = { 0, 0, 0 };
	scale = { 1, 1, 1 };
	XMStoreFloat4x4(&world, XMMatrixIdentity());

	verticalForce = 0.02f;
}


// Destructor
Transform::~Transform()
{

}


// Create World Matrix method
// Creates the world matrix from the position, rotation, and scale vectors
void Transform::CreateWorldMatrix()
{
	// make the necessary matrices
	XMMATRIX m4Translation = XMMatrixTranslation(position.x, position.y, position.z);
	XMMATRIX m4Scale = XMMatrixScaling(scale.x, scale.y, scale.z);
	XMMATRIX m4Rotation = XMMatrixRotationRollPitchYaw(rotation.x, rotation.y, rotation.z);

	// apply them to our world matrix
	// applies translation, then rotation, then scale
	XMMATRIX m4World = m4Scale * m4Rotation * m4Translation;

	// store the world matrix as a float4x4
	XMStoreFloat4x4(&world, m4World);
}


void Transform::MoveAbsolute(float x, float y, float z)
{
	position.x += x;
	position.y += y;
	position.z += z;
}

void Transform::MoveRelative(float x, float y, float z)
{
	// Create a direction vector from the parameters
	//  and a rotation quaternion
	XMVECTOR movement = XMVectorSet(x, y, z, 0);
	XMVECTOR rotQuat = XMQuaternionRotationRollPitchYawFromVector(XMLoadFloat3(&rotation));

	// Rotate the movement by the quaternion
	XMVECTOR dir = XMVector3Rotate(movement, rotQuat);

	// Add and store
	XMStoreFloat3(&position, XMLoadFloat3(&position) + dir);
}


void Transform::Rotate(float pitch, float yaw, float roll)
{
	rotation.x += pitch;
	rotation.y += yaw;
	rotation.z += roll;
}


void Transform::Scale(float x, float y, float z)
{
	scale.x *= x;
	scale.y *= y;
	scale.z *= z;
}


// SetPosition method
// Sets the position vector3 from 3 floats
void Transform::SetPosition(float x, float y, float z)
{
	position = { x, y, z };
}


// SetRotation method
// Sets the rotation vector3 from 3 floats
void Transform::SetRotation(float pitch, float yaw, float roll)
{
	rotation = { pitch, yaw, roll };
}


// SetScale method
// Sets the scale vector3 from 3 floats
void Transform::SetScale(float x, float y, float z)
{
	scale = { x, y, z };
}


DirectX::XMFLOAT3 Transform::GetPosition()
{
	return position;
}


DirectX::XMFLOAT3 Transform::GetRotation()
{
	return rotation;
}


DirectX::XMFLOAT3 Transform::GetScale()
{
	return scale;
}


DirectX::XMFLOAT4X4 Transform::GetWorldMatrix()
{
	return world;
}

float Transform::GetVerticalForce()
{
	return verticalForce;
}

void Transform::AddVerticalForce(float force)
{
	verticalForce += force;
}
