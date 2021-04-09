#include "Game.h"
#include "Vertex.h"
#include <fstream>
#include "BufferStructs.h"
#include "WICTextureLoader.h"
#include "DDSTextureLoader.h"
#include <iostream>

// Needed for a helper function to read compiled shader files from the hard drive
#pragma comment(lib, "d3dcompiler.lib")
#include <d3dcompiler.h>

// For the DirectX Math library
using namespace DirectX;

// --------------------------------------------------------
// Constructor
//
// DXCore (base class) constructor will set up underlying fields.
// DirectX itself, and our window, are not ready yet!
//
// hInstance - the application's OS-level handle (unique ID)
// --------------------------------------------------------
Game::Game(HINSTANCE hInstance)
	: DXCore(
		hInstance,		   // The application's handle
		"DirectX Game",	   // Text for the window's title bar
		720,			   // Width of the window's client area
		720,			   // Height of the window's client area
		true)			   // Show extra stats (fps) in title bar?
{

	camera = 0;
	currentEntity = 0;
	prevTab = false;
	/*pixelShader = 0;
	vertexShader = 0;*/
	pixelShaderNormalMap = 0;
	vertexShaderNormalMap = 0;
	currentPS = 0;
	currentVS = 0;
	terrainEntity = 0;
	terrainMesh = 0;
	terrainPS = 0;
	vertices = std::vector<Vertex>();

#if defined(DEBUG) || defined(_DEBUG)
	// Do we want a console window?  Probably only in debug mode
	CreateConsoleWindow(500, 120, 32, 120);
	printf("Console window created successfully.  Feel free to printf() here.\n");
#endif
}

// --------------------------------------------------------
// Destructor - Clean up anything our game has created:
//  - Release all DirectX objects created here
//  - Delete any objects to prevent memory leaks
// --------------------------------------------------------
Game::~Game()
{
	// Note: Since we're using smart pointers (ComPtr),
	// we don't need to explicitly clean up those DirectX objects
	// - If we weren't using smart pointers, we'd need
	//   to call Release() on each DirectX object
	for (int i = 0; i < entities.size(); i++)
	{
		delete entities[i];
	}

	for (int i = 0; i < bulletList.size(); i++) {
		delete bulletList[i];
	}

	/*delete vertexShader;
	delete pixelShader;*/
	delete vertexShaderNormalMap;
	delete pixelShaderNormalMap;
	delete currentPS;
	delete currentVS;
	delete camera;

	delete terrainPS;
	delete terrainMesh;
	delete terrainEntity;

	delete skyVS;
	delete skyPS;
}

// --------------------------------------------------------
// Called once per program, after DirectX and the window
// are initialized but before the game loop.
// --------------------------------------------------------
void Game::Init()
{
	// Helper methods for loading shaders, creating some basic
	// geometry to draw and some simple camera matrices.
	//  - You'll be expanding and/or replacing these later
	LoadShaders();
	CreateBasicGeometry();

	// Tell the input assembler stage of the pipeline what kind of
	// geometric primitives (points, lines or triangles) we want to draw.  
	// Essentially: "What kind of shape should the GPU draw with our data?"
	context->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

	// Get the size as the next multiple of 16
	unsigned int size = vertexShaderNormalMap->GetBufferSize(0);
	size = (size + 15) / 16 * 16;

	// Describe the constant buffer
	CD3D11_BUFFER_DESC cbDesc = {}; // Sets to all zeros
	cbDesc.BindFlags = D3D11_BIND_CONSTANT_BUFFER;
	cbDesc.ByteWidth = size;
	cbDesc.CPUAccessFlags = D3D11_CPU_ACCESS_WRITE;
	cbDesc.Usage = D3D11_USAGE_DYNAMIC;

	// Create the camera
	//camera = new Camera(x, y, z, aspectRatio, mouseLookSpeed);
	camera = new Camera(vertices[256].Position.x, vertices[256].Position.y + 5, vertices[256].Position.z, (float)(this->width / this->height), 0.5f);

	// Add Terrain Relevent Textures
	CreateWICTextureFromFile(device.Get(), context.Get(), GetFullPathTo_Wide(L"../../Assets/Textures/terrain_splat.png").c_str(), 0, &terrainBlendMapSRV);
	CreateWICTextureFromFile(device.Get(), context.Get(), GetFullPathTo_Wide(L"../../Assets/Textures/snow.jpg").c_str(), 0, &terrainTexture0SRV);
	CreateWICTextureFromFile(device.Get(), context.Get(), GetFullPathTo_Wide(L"../../Assets/Textures/grass3.png").c_str(), 0, &terrainTexture1SRV);
	CreateWICTextureFromFile(device.Get(), context.Get(), GetFullPathTo_Wide(L"../../Assets/Textures/mountain3.png").c_str(), 0, &terrainTexture2SRV);
	CreateWICTextureFromFile(device.Get(), context.Get(), GetFullPathTo_Wide(L"../../Assets/Textures/snow_normals.jpg").c_str(), 0, &terrainNormals0SRV);
	CreateWICTextureFromFile(device.Get(), context.Get(), GetFullPathTo_Wide(L"../../Assets/Textures/grass3_normals.png").c_str(), 0, &terrainNormals1SRV);
	CreateWICTextureFromFile(device.Get(), context.Get(), GetFullPathTo_Wide(L"../../Assets/Textures/mountain3_normals.png").c_str(), 0, &terrainNormals2SRV);


	// Initialize lights
	DirectionalLight light1;
	light1.ambientColor = XMFLOAT3(0.1f, 0.1f, 0.1f);
	light1.diffuseColor = XMFLOAT3(1, 1, 1);
	light1.direction = XMFLOAT3(-1, 0, 0);
	dLights.push_back(light1);
	DirectionalLight light2;
	light2.ambientColor = XMFLOAT3(0.1f, 0.1f, 0.1f);
	light2.diffuseColor = XMFLOAT3(0, 1, 1);
	light2.direction = XMFLOAT3(-1, -1, 0);
	dLights.push_back(light2);
	DirectionalLight light3;
	light3.ambientColor = XMFLOAT3(0.1f, 0.1f, 0.1f);
	light3.diffuseColor = XMFLOAT3(1, 0, 1);
	light3.direction = XMFLOAT3(1, 1, 0);
	dLights.push_back(light3);

	PointLight pLight1;
	pLight1.ambientColor = XMFLOAT3(0.1f, 0.1f, 0.1f);
	pLight1.diffuseColor = XMFLOAT3(0, 0, 1);
	pLight1.position = XMFLOAT3(-10, 0, 0);
	pLights.push_back(pLight1);


	skyMesh = new Mesh(GetFullPathTo("../../Assets/Models/cube.obj").c_str(), device);

	// Load the sky box texture
	CreateDDSTextureFromFile(
		device.Get(),
		GetFullPathTo_Wide(L"../../Assets/Skies/SunnyCubeMap.dds").c_str(),
		0,
		skySRV.GetAddressOf());

	// Make the sky rasterizer state
	D3D11_RASTERIZER_DESC rastDesc = {};
	rastDesc.FillMode = D3D11_FILL_SOLID;
	rastDesc.CullMode = D3D11_CULL_FRONT;
	rastDesc.DepthClipEnable = true;
	device->CreateRasterizerState(&rastDesc, skyRasterState.GetAddressOf());

	// Make the sky depth state
	D3D11_DEPTH_STENCIL_DESC dsDesc = {};
	dsDesc.DepthEnable = true;
	dsDesc.DepthWriteMask = D3D11_DEPTH_WRITE_MASK_ALL;
	dsDesc.DepthFunc = D3D11_COMPARISON_LESS_EQUAL;
	device->CreateDepthStencilState(&dsDesc, skyDepthState.GetAddressOf());

	// Texture releated init
	CreateWICTextureFromFile(
		device.Get(),
		context.Get(),	// Passing in the context auto-generates mipmaps!!
		GetFullPathTo_Wide(L"../../Assets/Textures/rock.png").c_str(),
		nullptr,		// We don't need the texture ref ourselves
		diffuseTexture.GetAddressOf()); // We do need an SRV

	CreateWICTextureFromFile(
		device.Get(),
		context.Get(),	// Passing in the context auto-generates mipmaps!!
		GetFullPathTo_Wide(L"../../Assets/Textures/rock_normals.png").c_str(),
		nullptr,		// We don't need the texture ref ourselves
		normalMap.GetAddressOf()); // We do need an SRV


	// Describe the sampler state that I want
	D3D11_SAMPLER_DESC sampDesc = {};
	sampDesc.AddressU = D3D11_TEXTURE_ADDRESS_WRAP;
	sampDesc.AddressV = D3D11_TEXTURE_ADDRESS_WRAP;
	sampDesc.AddressW = D3D11_TEXTURE_ADDRESS_WRAP;
	sampDesc.Filter = D3D11_FILTER_ANISOTROPIC;
	sampDesc.MaxAnisotropy = 16;
	sampDesc.MaxLOD = D3D11_FLOAT32_MAX;
	device->CreateSamplerState(&sampDesc, samplerOptions.GetAddressOf());

	// Sprite batch setup (and sprite font loading)
	spriteBatch = std::make_unique<SpriteBatch>(context.Get());
	spriteFont = std::make_unique<SpriteFont>(device.Get(), GetFullPathTo_Wide(L"../../Assets/Fonts/Arial.spritefont").c_str());

	prevLButton = false;
}

// --------------------------------------------------------
// Loads shaders from compiled shader object (.cso) files
// and also created the Input Layout that describes our 
// vertex data to the rendering pipeline. 
// - Input Layout creation is done here because it must 
//    be verified against vertex shader byte code
// - We'll have that byte code already loaded below
// --------------------------------------------------------
void Game::LoadShaders()
{
	/*vertexShader = new SimpleVertexShader(device.Get(), context.Get(),
		GetFullPathTo_Wide(L"VertexShader.cso").c_str());
	pixelShader = new SimplePixelShader(device.Get(), context.Get(),
		GetFullPathTo_Wide(L"PixelShader.cso").c_str());*/

	vertexShaderNormalMap = new SimpleVertexShader(device.Get(), context.Get(),
		GetFullPathTo_Wide(L"NormalMapVS.cso").c_str());
	pixelShaderNormalMap = new SimplePixelShader(device.Get(), context.Get(),
		GetFullPathTo_Wide(L"NormalMapPS.cso").c_str());

	// Terrain Specific Pixel Shader
	terrainPS = new SimplePixelShader(
		device.Get(),
		context.Get(),
		GetFullPathTo_Wide(L"TerrainPS.cso").c_str());

	skyVS = new SimpleVertexShader(
		device.Get(),
		context.Get(),
		GetFullPathTo_Wide(L"SkyVS.cso").c_str());

	skyPS = new SimplePixelShader(
		device.Get(),
		context.Get(),
		GetFullPathTo_Wide(L"SkyPS.cso").c_str());
}



// --------------------------------------------------------
// Creates the geometry we're going to draw
// --------------------------------------------------------
void Game::CreateBasicGeometry()
{
	// ********************************************************
	// Texture initialization
	// ********************************************************

	CreateWICTextureFromFile(
		device.Get(),
		context.Get(),	// Passing in the context auto-generates mipmaps!!
		GetFullPathTo_Wide(L"../../Assets/Textures/targetTexture.png").c_str(),
		nullptr,		// We don't need the texture ref ourselves
		diffuseTexture1.GetAddressOf()); // We do need an SRV
	CreateWICTextureFromFile(
		device.Get(),
		context.Get(),	// Passing in the context auto-generates mipmaps!!
		GetFullPathTo_Wide(L"../../Assets/Textures/targetNormal.png").c_str(),
		nullptr,		// We don't need the texture ref ourselves
		normalMap1.GetAddressOf()); // We do need an SRV

	
	// Describe the sampler state that I want
	D3D11_SAMPLER_DESC sampDesc = {};
	sampDesc.AddressU = D3D11_TEXTURE_ADDRESS_WRAP;
	sampDesc.AddressV = D3D11_TEXTURE_ADDRESS_WRAP;
	sampDesc.AddressW = D3D11_TEXTURE_ADDRESS_WRAP;
	sampDesc.Filter = D3D11_FILTER_ANISOTROPIC;
	sampDesc.MaxAnisotropy = 16;
	sampDesc.MaxLOD = D3D11_FLOAT32_MAX;
	device->CreateSamplerState(&sampDesc, samplerOptions.GetAddressOf());


	//skyMesh = new Mesh(GetFullPathTo("../../Assets/OBJ Files/cube.obj").c_str(), device);

	// ********************************************************
	// Entity initialization
	// ********************************************************



	// Terrain Creation
	/*terrainMesh = new TerrainMesh(
		device,
		GetFullPathTo("../../Assets/Textures/valley.raw16").c_str(),
		513,
		513,
		BitDepth_16,
		5.0f,
		0.05f,
		1.0f);*/

	terrainMesh = new TerrainMesh(
		device,
		GetFullPathTo("../../Assets/Textures/terrain_height.r16").c_str(),
		513,
		513,
		BitDepth_16,
		5.0f,
		0.05f,
		1.0f);

	terrainEntity = new Entity(terrainMesh, nullptr);


	//bullet creation
	bulletMesh = new Mesh(GetFullPathTo("../../Assets/Models/sphere.obj").c_str(), device);
	bulletMaterial = new Material(pixelShaderNormalMap, vertexShaderNormalMap, XMFLOAT4(1.0f, 1.0f, 1.0f, 1.0f), 1.0f, diffuseTexture1.Get(), normalMap1.Get(), samplerOptions.Get());

	
	// Target creation
	vertices = terrainEntity->GetMesh()->GetVertices();
	float x = 0;
	float y = 0;
	float z = 0;
	float xUpperBound = 1000;
	float xLowerBound = -1000;
	float zUpperBound = 1000;
	float zLowerBound = -1000;
	int numberOfTargets = 10;

	for (int i = 0; i < numberOfTargets; i++)
	{
		// Generate the entity
		entities.push_back(new Entity(
			new Mesh(GetFullPathTo("../../Assets/Models/target.obj").c_str(), device),
			new Material(pixelShaderNormalMap, vertexShaderNormalMap, XMFLOAT4(1.0f, 1.0f, 1.0f, 1.0f), 1.0f, diffuseTexture1.Get(), normalMap1.Get(), samplerOptions.Get())
		));

		// Find a random vertex within set bounts and put a target there
		do {
			int index = ((double)rand() / (RAND_MAX)) * vertices.size();
			x = vertices[index].Position.x;
			y = vertices[index].Position.y;
			z = vertices[index].Position.z;
		} while (!(x > -1000 && x < 1000 && z > -1000 && z < 1000));

			entities[i]->GetTransform()->SetPosition(x, y+0.2f, z);
	}
}


// --------------------------------------------------------
// Loads six individual textures (the six faces of a cube map), then
// creates a blank cube map and copies each of the six textures to
// another face.  Afterwards, creates a shader resource view for
// the cube map and cleans up all of the temporary resources.
// --------------------------------------------------------
Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> Game::CreateCubemap(
	const wchar_t* right,
	const wchar_t* left,
	const wchar_t* up,
	const wchar_t* down,
	const wchar_t* front,
	const wchar_t* back)
{
	// Load the 6 textures into an array.
	// - We need references to the TEXTURES, not the SHADER RESOURCE VIEWS!
	// - Specifically NOT generating mipmaps, as we don't need them for the sky!
	ID3D11Texture2D* textures[6] = {};
	CreateWICTextureFromFile(device.Get(), right, (ID3D11Resource**)&textures[0], 0);
	CreateWICTextureFromFile(device.Get(), left, (ID3D11Resource**)&textures[1], 0);
	CreateWICTextureFromFile(device.Get(), up, (ID3D11Resource**)&textures[2], 0);
	CreateWICTextureFromFile(device.Get(), down, (ID3D11Resource**)&textures[3], 0);
	CreateWICTextureFromFile(device.Get(), front, (ID3D11Resource**)&textures[4], 0);
	CreateWICTextureFromFile(device.Get(), back, (ID3D11Resource**)&textures[5], 0);

	// We'll assume all of the textures are the same color format and resolution,
	// so get the description of the first shader resource view
	D3D11_TEXTURE2D_DESC faceDesc = {};
	textures[0]->GetDesc(&faceDesc);

	// Describe the resource for the cube map, which is simply 
	// a "texture 2d array".  This is a special GPU resource format, 
	// NOT just a C++ arrray of textures!!!
	D3D11_TEXTURE2D_DESC cubeDesc = {};
	cubeDesc.ArraySize = 6; // Cube map!
	cubeDesc.BindFlags = D3D11_BIND_SHADER_RESOURCE; // We'll be using as a texture in a shader
	cubeDesc.CPUAccessFlags = 0; // No read back
	cubeDesc.Format = faceDesc.Format; // Match the loaded texture's color format
	cubeDesc.Width = faceDesc.Width;  // Match the size
	cubeDesc.Height = faceDesc.Height; // Match the size
	cubeDesc.MipLevels = 1; // Only need 1
	cubeDesc.MiscFlags = D3D11_RESOURCE_MISC_TEXTURECUBE; // This should be treated as a CUBE, not 6 separate textures
	cubeDesc.Usage = D3D11_USAGE_DEFAULT; // Standard usage
	cubeDesc.SampleDesc.Count = 1;
	cubeDesc.SampleDesc.Quality = 0;

	// Create the actual texture resource
	ID3D11Texture2D* cubeMapTexture = 0;
	device->CreateTexture2D(&cubeDesc, 0, &cubeMapTexture);

	// Loop through the individual face textures and copy them,
	// one at a time, to the cube map texure
	for (int i = 0; i < 6; i++)
	{
		// Calculate the subresource position to copy into
		unsigned int subresource = D3D11CalcSubresource(
			0,	// Which mip (zero, since there's only one)
			i,	// Which array element?
			1); // How many mip levels are in the texture?

		// Copy from one resource (texture) to another
		context->CopySubresourceRegion(
			cubeMapTexture, // Destination resource
			subresource,	// Dest subresource index (one of the array elements)
			0, 0, 0,		// XYZ location of copy
			textures[i],	// Source resource
			0,				// Source subresource index (we're assuming there's only one)
			0);				// Source subresource "box" of data to copy (zero means the whole thing)
	}

	// At this point, all of the faces have been copied into the 
	// cube map texture, so we can describe a shader resource view for it
	D3D11_SHADER_RESOURCE_VIEW_DESC srvDesc = {};
	srvDesc.Format = cubeDesc.Format; // Same format as texture
	srvDesc.ViewDimension = D3D11_SRV_DIMENSION_TEXTURECUBE; // Treat this as a cube!
	srvDesc.TextureCube.MipLevels = 1;	// Only need access to 1 mip
	srvDesc.TextureCube.MostDetailedMip = 0; // Index of the first mip we want to see

	// Make the SRV
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> cubeSRV;
	device->CreateShaderResourceView(cubeMapTexture, &srvDesc, cubeSRV.GetAddressOf());

	// Now that we're done, clean up the stuff we don't need anymore
	cubeMapTexture->Release();  // Done with this particular reference (the SRV has another)
	for (int i = 0; i < 6; i++)
		textures[i]->Release();

	// Send back the SRV, which is what we need for our shaders
	return cubeSRV;
}


// --------------------------------------------------------
// Handle resizing DirectX "stuff" to match the new window size.
// For instance, updating our projection matrix's aspect ratio.
// --------------------------------------------------------
void Game::OnResize()
{
	// Handle base-level DX resize stuff
	DXCore::OnResize();

	if (camera)
		camera->UpdateProjectionMatrix(this->width / (float)this->height);
}

// --------------------------------------------------------
// Update your game here - user input, move objects, AI, etc.
// --------------------------------------------------------
void Game::Update(float deltaTime, float totalTime)
{
	// Quit if the escape key is pressed
	if (GetAsyncKeyState(VK_ESCAPE))
		Quit();


	//--- Shooting Code ---
	if (GetAsyncKeyState(VK_RBUTTON) && !prevLButton) {
		bulletList.push_back(new Entity(bulletMesh, bulletMaterial, camera->GetTransform()->GetPosition(), camera->GetTransform()->GetRotation()));
		bulletList[bulletList.size() - 1]->GetTransform()->SetScale(0.1f, 0.1f, 0.1f);
		prevLButton = true;
	}
	if (!GetAsyncKeyState(VK_RBUTTON)) {
		prevLButton = false;
	}

	for (Entity* bullet : bulletList) {
		bullet->GetTransform()->MoveRelative(0, 0, 0.1f);
		bullet->GetTransform()->AddVerticalForce(GRAVITY * 100);
		bullet->GetTransform()->MoveAbsolute(0, bullet->GetTransform()->GetVerticalForce(), 0);
	}

	//delete bullets that travel too far away
	std::vector<Entity*> deleteList = std::vector<Entity*>();
	auto it = bulletList.begin();
	while (it != bulletList.end()) {
		float distance = sqrt(
			pow((*it)->GetTransform()->GetPosition().x - camera->GetTransform()->GetPosition().x, 2) +
			pow((*it)->GetTransform()->GetPosition().y - camera->GetTransform()->GetPosition().y, 2) +
			pow((*it)->GetTransform()->GetPosition().z - camera->GetTransform()->GetPosition().z, 2));
		if (distance > 40) {
			it = bulletList.erase(it);
		}
		else {
			++it;
		}
	}
	//delete bullets that are too far away
	/*for (Entity* toDelete : deleteList) {
		bool deleteEnt = false;
		int deletePos = 0;
		for (int i = 0; i < bulletList.size(); i++) {
			if (toDelete == bulletList[i]) {
				deleteEnt = bulletList[i];
				deletePos = i;
			}
		}
		if (deleteEnt) {
			delete bulletList[deletePos];
		}
	}*/

	for(Entity* ent : entities)
	{
		ent->GetTransform()->CreateWorldMatrix();
	}

	int bulletIndex = -1;
	int targetIndex = -1;
	collisionRadius = 1;

	//for (Entity* bullet : bulletList)
	for (int i = 0; i < bulletList.size(); i++)
	{
		bulletList[i]->GetTransform()->CreateWorldMatrix();

		// Bullet-target collision detection
		//for (Entity* target : entities)
		for (int j = 0; j < entities.size(); j++)
		{
			if (sqrt(
				pow(entities[j]->GetTransform()->GetPosition().x - bulletList[i]->GetTransform()->GetPosition().x, 2) +
				pow(entities[j]->GetTransform()->GetPosition().y - bulletList[i]->GetTransform()->GetPosition().y, 2) +
				pow(entities[j]->GetTransform()->GetPosition().z - bulletList[i]->GetTransform()->GetPosition().z, 2))
				<= collisionRadius)
			{
				// Collision detected!
				// target that needs to be removed
				targetIndex = j;
				// bullet that needs to be removed
				bulletIndex = i;
			}
		}
	}

	if (targetIndex != -1 && bulletIndex != -1)
	{
		// Remove the target and bullet that collided
		entities.erase(entities.begin() + targetIndex);
		bulletList.erase(bulletList.begin() + bulletIndex);
	}

	if (entities.size() == 0)
	{
		Quit();
	}

	// Update the camera
	camera->Update(deltaTime, this->hWnd, vertices);
}


// --------------------------------------------------------
// Perform all the steps necessary to render the sky
// --------------------------------------------------------
void Game::RenderSky()
{
	// Set up my render states
	context->RSSetState(skyRasterState.Get());
	context->OMSetDepthStencilState(skyDepthState.Get(), 0);

	// Set up the sky shaders
	skyVS->SetShader();
	skyPS->SetShader();

	skyVS->SetMatrix4x4("view", camera->GetView());
	skyVS->SetMatrix4x4("proj", camera->GetProjection());
	skyVS->CopyAllBufferData();

	skyPS->SetShaderResourceView("sky", skySRV.Get());
	skyPS->SetSamplerState("samplerOptions", samplerOptions.Get());

	// Actually draw the geometry
	skyMesh->SetBuffersAndDraw(context);

	// Reset any states back to the default
	context->RSSetState(0); // Sets the default state
	context->OMSetDepthStencilState(0, 0);
}


// --------------------------------------------------------
// Clear the screen, redraw everything, present to the user
// --------------------------------------------------------
void Game::Draw(float deltaTime, float totalTime)
{
	// Background color (Cornflower Blue in this case) for clearing
	const float color[4] = { 0.4f, 0.6f, 0.75f, 0.0f };

	// Clear the render target and depth buffer (erases what's on the screen)
	//  - Do this ONCE PER FRAME
	//  - At the beginning of Draw (before drawing *anything*)
	context->ClearRenderTargetView(backBufferRTV.Get(), color);
	context->ClearDepthStencilView(
		depthStencilView.Get(),
		D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL,
		1.0f,
		0);

	// Set texture resources for the next draw
	pixelShaderNormalMap->SetShaderResourceView("diffuseTexture", diffuseTexture.Get());
	pixelShaderNormalMap->SetShaderResourceView("normalMap", normalMap.Get());
	pixelShaderNormalMap->SetSamplerState("samplerOptions", samplerOptions.Get());

	// Set the vertex and pixel shaders to use for the next Draw() command ** done below in the for loop
	//  - These don't technically need to be set every frame
	//  - Once you start applying different shaders to different objects,
	//    you'll need to swap the current shaders before each draw

	// Ensure the pipeline knows how to interpret the data (numbers)
	// from the vertex buffer.  
	// - If all of your 3D models use the exact same vertex layout,
	//    this could simply be done once in Init()
	// - However, this isn't always the case (but might be for this course)
	//context->IASetInputLayout(inputLayout.Get()); // Removed due to SimpleShader implementation


	// loop through entities
	for (Entity* ent : entities)
	{
		currentPS = ent->GetMaterial()->GetPixelShader();
		currentVS = ent->GetMaterial()->GetVertexShader();

		// Activate the current material's shaders
		currentVS->SetShader();
		currentPS->SetShader();

		// Lights
	//==========================================================================
	//  Which pixel shader do we use for lights and camera when we have a bunch?
	//  Currently using the last entity's PS
		currentPS->SetData("dLight1", &dLights[0], sizeof(DirectionalLight));
		currentPS->SetData("pLight1", &pLights[0], sizeof(PointLight));
		currentPS->SetFloat3("cameraPosition", camera->GetTransform()->GetPosition());
	// =========================================================================
		

		currentPS->SetFloat("specInt", ent->GetMaterial()->GetSpecularIntensity());
		currentPS->CopyAllBufferData();

		currentPS->SetShaderResourceView("diffuseTexture", ent->GetMaterial()->GetSRV().Get());
		// check for normal map
		if (ent->GetMaterial()->GetNormalMap().Get() != nullptr)
		{
			currentPS->SetShaderResourceView("normalMap", ent->GetMaterial()->GetNormalMap().Get());
		}
		currentPS->SetSamplerState("samplerOptions", ent->GetMaterial()->GetSamplerState().Get());


		ent->Draw(context, currentVS, currentPS, camera);

		// Collecting data locally
		/*SimpleVertexShader* vsData = currentVS;
		vsData->SetFloat4("colorTint", ent->GetMaterial()->GetColorTint());
		vsData->SetMatrix4x4("world", ent->GetTransform()->GetWorldMatrix());
		vsData->SetMatrix4x4("view", camera->GetView());
		vsData->SetMatrix4x4("projection", camera->GetProjection());

		vsData->CopyAllBufferData();*/

		// Set buffers in the input assembler
		//  - Do this ONCE PER OBJECT you're drawing, since each object might
		//    have different geometry.
		//  - for this demo, this step *could* simply be done once during Init(),
		//    but I'm doing it here because it's often done multiple times per frame
		//    in a larger application/game


		//UINT stride = sizeof(Vertex);
		//UINT offset = 0;


		//context->IASetVertexBuffers(0, 1, ent->GetMesh()->GetVertexBuffer().GetAddressOf(), &stride, &offset);
		//context->IASetIndexBuffer(ent->GetMesh()->GetIndexBuffer().Get(), DXGI_FORMAT_R32_UINT, 0);
		////  - Do this ONCE PER OBJECT you intend to draw
		////  - This will use all of the currently set DirectX "stuff" (shaders, buffers, etc)
		////  - DrawIndexed() uses the currently set INDEX BUFFER to look up corresponding
		////     vertices in the currently set VERTEX BUFFER
		//context->DrawIndexed(
		//	ent->GetMesh()->GetIndexCount(),     // The number of indices to use (we could draw a subset if we wanted)
		//	0,     // Offset to the first index we want to use
		//	0);    // Offset to add to each index when looking up vertices
	}

	//loop through bullets and draw them
	for (Entity* ent : bulletList)
	{
		currentPS = ent->GetMaterial()->GetPixelShader();
		currentVS = ent->GetMaterial()->GetVertexShader();

		// Activate the current material's shaders
		currentVS->SetShader();
		currentPS->SetShader();

		currentPS->SetFloat("specInt", ent->GetMaterial()->GetSpecularIntensity());
		currentPS->CopyAllBufferData();

		currentPS->SetShaderResourceView("diffuseTexture", ent->GetMaterial()->GetSRV().Get());
		// check for normal map
		if (ent->GetMaterial()->GetNormalMap().Get() != nullptr)
		{
			currentPS->SetShaderResourceView("normalMap", ent->GetMaterial()->GetNormalMap().Get());
		}
		currentPS->SetSamplerState("samplerOptions", ent->GetMaterial()->GetSamplerState().Get());


		ent->Draw(context, currentVS, currentPS, camera);
	}

	// Terrain Drawing
	vertexShaderNormalMap->SetShader();
	terrainPS->SetShader();

	/*
	terrainPS->SetFloat("lightIntensity", 1.0f);
	terrainPS->SetFloat3("lightColor", XMFLOAT3(0.8f, 0.8f, 0.8f));
	terrainPS->SetFloat3("lightDirection", XMFLOAT3(1, -1, 1));
	*/
	/*terrainPS->SetFloat("pointLightIntensity", 1.0f);
	terrainPS->SetFloat("pointLightRange", 10.0f);
	terrainPS->SetFloat3("pointLightColor", XMFLOAT3(1, 1, 1));
	terrainPS->SetFloat3("pointLightPos", lightEntity->GetTransform()->GetPosition());*/

	terrainPS->SetFloat3("environmentAmbientColor", XMFLOAT3(0.50f, 0.5f, 0.5f));

	terrainPS->SetFloat3("cameraPosition", camera->GetTransform()->GetPosition());

	terrainPS->SetFloat("uvScale0", 50.0f);
	terrainPS->SetFloat("uvScale1", 50.0f);
	terrainPS->SetFloat("uvScale2", 50.0f);

	terrainPS->SetFloat("specularAdjust", 0.0f);

	terrainPS->CopyAllBufferData();

	// Set texture resources for the next draw
	terrainPS->SetShaderResourceView("blendMap", terrainBlendMapSRV.Get());
	terrainPS->SetShaderResourceView("texture0", terrainTexture0SRV.Get());
	terrainPS->SetShaderResourceView("texture1", terrainTexture1SRV.Get());
	terrainPS->SetShaderResourceView("texture2", terrainTexture2SRV.Get());
	terrainPS->SetShaderResourceView("normalMap0", terrainNormals0SRV.Get());
	terrainPS->SetShaderResourceView("normalMap1", terrainNormals1SRV.Get());
	terrainPS->SetShaderResourceView("normalMap2", terrainNormals2SRV.Get());
	terrainPS->SetSamplerState("samplerOptions", samplerOptions.Get());

	terrainEntity->Draw(context, vertexShaderNormalMap, terrainPS, camera);

	// Render the sky after all the opaque geometry has been rendered
	RenderSky();

	// Present the back buffer to the user
	//  - Puts the final frame we're drawing into the window so the user can see it
	//  - Do this exactly ONCE PER FRAME (always at the very end of the frame)
	swapChain->Present(0, 0);

	// Due to the usage of a more sophisticated swap chain,
	// the render target must be re-bound after every call to Present()
	context->OMSetRenderTargets(1, backBufferRTV.GetAddressOf(), depthStencilView.Get());

	// === SpriteBatch =====================================
	// See these links for more info!
	// SpriteBatch: https://github.com/microsoft/DirectXTK/wiki/SpriteBatch
	// SpriteFont: https://github.com/microsoft/DirectXTK/wiki/SpriteFont
	{
		// Make a rectangle for each of the output images
		RECT imageRect = { 10, 10, 110, 110 };
		RECT normalMapRect = { 120, 10, 220, 110 };
		RECT fontSheetRect = { 230, 10, 320, 110 };

		// Grab the SRV of the font from the SpriteFont
		// Note: It's not great to do this every frame, but 
		// this is just a demo to show what it looks like!
		ID3D11ShaderResourceView* fontSheet;
		spriteFont->GetSpriteSheet(&fontSheet);

		// Begin the batch, draw lots of stuff, then end it
		spriteBatch->Begin();

		// Draw a few 2D textures around the screen
		spriteBatch->Draw(diffuseTexture.Get(), imageRect);
		spriteBatch->Draw(normalMap.Get(), normalMapRect);
		spriteBatch->Draw(fontSheet, fontSheetRect);

		// Draw some arbitrary text
		spriteFont->DrawString(
			spriteBatch.get(),
			L"This is some cool text, yo",
			XMFLOAT2(10, 120));

		// Draw the mouse position
		POINT mousePos = {};
		GetCursorPos(&mousePos);
		ScreenToClient(hWnd, &mousePos);
		std::wstring dynamicText = L"Mouse Pos: " + std::to_wstring(mousePos.x) + L"," + std::to_wstring(mousePos.y);
		spriteFont->DrawString(
			spriteBatch.get(),
			dynamicText.c_str(),
			XMFLOAT2(10, 150));

		// Done with the batch
		spriteBatch->End();

		// Release the extra reference to the font sheet we made above
		// when we called GetSpriteSheet()
		fontSheet->Release();

		// Reset any states that may be changed by sprite batch!
		context->OMSetBlendState(0, 0, 0xFFFFFFFF);
		context->RSSetState(0);
		context->OMSetDepthStencilState(0, 0);
	}
	// ======================================================
}