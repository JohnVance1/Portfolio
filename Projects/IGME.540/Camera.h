#pragma once
#include <DirectXMath.h>
#include <Windows.h>
#include "Transform.h"
#include <iostream>
#include <vector>
#include "Vertex.h"


class Camera
{
public:
	Camera(float x, float y, float z, float aspectRatio, float mouseLookSpeed);
	~Camera();

	// Updating
	void Update(float dt, HWND windowHandle, std::vector<Vertex> terrainVerts);
	void UpdateViewMatrix();
	void UpdateProjectionMatrix(float aspectRatio);

	// Getters
	DirectX::XMFLOAT4X4 GetView() { return viewMatrix; }
	DirectX::XMFLOAT4X4 GetProjection() { return projMatrix; }

	Transform* GetTransform();

private:
	// Camera matrices
	DirectX::XMFLOAT4X4 viewMatrix;
	DirectX::XMFLOAT4X4 projMatrix;

	// Room to add FOV, near/far clip distances, movement speed, etc.
	float mouseLookSpeed;
	POINT prevMousePosition;
	Transform transform;
	int index = 256;
	float distanceY = 0;
	float distanceZ = 0;

};

