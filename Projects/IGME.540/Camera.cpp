#include "Camera.h"
#include <Windows.h>
#include <iostream>
#include "Vertex.h"
#include <vector>

using namespace DirectX;

// Creates a camera at the specified position
Camera::Camera(float x, float y, float z, float aspectRatio, float mouseLookSpeed)
{
	// Save speed and pos
	this->mouseLookSpeed = mouseLookSpeed;
	transform.SetPosition(x, y, z);

	// Update our view & proj
	UpdateViewMatrix();
	UpdateProjectionMatrix(aspectRatio);
}

// Clean up if necessary
Camera::~Camera()
{ }


// Camera's update, which looks for key presses
void Camera::Update(float dt, HWND windowHandle, std::vector<Vertex> terrainVerts)
{
	// Come up with a speed
	float speed = dt * .1; // Hardcoding "3" as speed!

	// Adjust speed if necessary based on keys...

	// Basic movement
	bool onRails = true;
	if (!onRails)
	{
		if (GetAsyncKeyState('W') & 0x8000) { transform.MoveRelative(0, 0, speed); }
		if (GetAsyncKeyState('S') & 0x8000) { transform.MoveRelative(0, 0, -speed); }
		if (GetAsyncKeyState('A') & 0x8000) { transform.MoveRelative(-speed, 0, 0); }
		if (GetAsyncKeyState('D') & 0x8000) { transform.MoveRelative(speed, 0, 0); }

		if (GetAsyncKeyState(VK_SPACE) & 0x8000) { transform.MoveAbsolute(0, speed, 0); }
		if (GetAsyncKeyState('X') & 0x8000) { transform.MoveAbsolute(0, -speed, 0); }
	}
	else
	{	

		distanceY = terrainVerts[index].Position.y - transform.GetPosition().y;
		distanceZ = terrainVerts[index].Position.z - transform.GetPosition().z;

		

		transform.MoveAbsolute(0, (distanceY + .35) * 5 * dt, distanceZ * 5 * dt);
		//transform.SetPosition(transform.GetPosition().x, transform.GetPosition().y + .25, transform.GetPosition().z);

		if(distanceZ <= 0.1f)
		{ 
			index += 513;
		
		}

		if((float)index > terrainVerts.size())
		{
			index = 256;
		}
		
	}

	// Mouse look...
	POINT mousePos = {};
	GetCursorPos(&mousePos);
	ScreenToClient(windowHandle, &mousePos);

	if (GetAsyncKeyState(VK_LBUTTON) & 0x8000)
	{
		float yaw = mousePos.x - prevMousePosition.x;
		float pitch = mousePos.y - prevMousePosition.y;
		yaw *= mouseLookSpeed * dt;
		pitch *= mouseLookSpeed * dt;
		transform.Rotate(pitch, yaw, 0);
	}

	// Save the current mouse position as prevMousePosition for the next frame
	prevMousePosition.x = mousePos.x;
	prevMousePosition.y = mousePos.y;

	// Every frame, update the view matrix
	UpdateViewMatrix();
}

// Creates a new view matrix based on current position and orientation
void Camera::UpdateViewMatrix()
{
	// Figure out our "forward"  vector and
	// create a quaternion using OUR rotation
	XMFLOAT3 rotation = transform.GetRotation();
	XMVECTOR rotQuat = XMQuaternionRotationRollPitchYawFromVector(XMLoadFloat3(&rotation));

	// Define the "standard" forward vector w/o rotation (0,0,1)
	XMVECTOR basicForward = XMVectorSet(0, 0, 1, 0);

	// Rotate the "standard forward" by OUR rotation
	XMVECTOR ourForward = XMVector3Rotate(basicForward, rotQuat);

	// Create the final view matrix
	XMMATRIX view = XMMatrixLookToLH(
		XMLoadFloat3(&transform.GetPosition()),
		ourForward,
		XMVectorSet(0, 1, 0, 0));
	XMStoreFloat4x4(&viewMatrix, view);
}

// Updates the projection matrix
void Camera::UpdateProjectionMatrix(float aspectRatio)
{
	XMMATRIX proj = XMMatrixPerspectiveFovLH(
		XM_PIDIV4,
		aspectRatio,
		0.01f,
		100.0f);
	XMStoreFloat4x4(&projMatrix, proj);
}

Transform* Camera::GetTransform()
{
	return &transform;
}
