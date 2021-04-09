#pragma once

#include "DXCore.h"
#include <DirectXMath.h>
#include <wrl/client.h> // Used for ComPtr - a smart pointer for COM objects
#include "Mesh.h"
#include "Entity.h"
#include "Camera.h"
#include "Material.h"
#include "Lights.h"
#include <vector>
#include "TerrainMesh.h"
#include "SpriteBatch.h"
#include "SpriteFont.h"
#include "WICTextureLoader.h"
#include "DDSTextureLoader.h"
#include <memory>

class Game 
	: public DXCore
{

public:
	Game(HINSTANCE hInstance);
	~Game();

	// Overridden setup and game loop methods, which
	// will be called automatically
	void Init();
	void OnResize();
	void Update(float deltaTime, float totalTime);
	void Draw(float deltaTime, float totalTime);

private:

	// Initialization helper methods - feel free to customize, combine, etc.
	void LoadShaders(); 
	void CreateBasicGeometry();

	// Helper for creating a cubemap from 6 individual textures
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> CreateCubemap(
		const wchar_t* right,
		const wchar_t* left,
		const wchar_t* up,
		const wchar_t* down,
		const wchar_t* front,
		const wchar_t* back);

	// Private render methods
	void RenderSky();

	// Matrices
	DirectX::XMFLOAT4X4 worldMatrix;

	// Entities
	std::vector<Entity*> entities = std::vector<Entity*>();
	//bullets
	std::vector<Entity*> bulletList = std::vector<Entity*>();
	Mesh* bulletMesh;
	Material* bulletMaterial;
	const float GRAVITY = -0.000005f;
	float collisionRadius = 1;

	// User input and entity swapping
	int currentEntity;
	bool prevTab;
	bool prevLButton;

	Camera* camera;
	
	// Note the usage of ComPtr below
	//  - This is a smart pointer for objects that abide by the
	//    Component Object Mode, which DirectX objects do
	//  - More info here: https://github.com/Microsoft/DirectXTK/wiki/ComPtr

	// Shaders and shader-related constructs
	/*SimplePixelShader* pixelShader;
	SimpleVertexShader* vertexShader;*/

	SimplePixelShader* pixelShaderNormalMap;
	SimpleVertexShader* vertexShaderNormalMap;

	SimplePixelShader* currentPS;
	SimpleVertexShader* currentVS;

	// Materials for Assignment 5
	std::vector<Material*> materials;

	// Lights
	std::vector<DirectionalLight> dLights = std::vector<DirectionalLight>();
	std::vector<PointLight> pLights = std::vector<PointLight>();

	// Texture related resources
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> diffuseTexture1;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> diffuseTexture2;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> normalMap1;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> normalMap2;
	Microsoft::WRL::ComPtr<ID3D11SamplerState> samplerOptions;

	// Terrain additions
	Entity* terrainEntity;
	Mesh* terrainMesh;
	SimplePixelShader* terrainPS;

	// Terrain vertices
	std::vector<Vertex> vertices;

	// Blend (or "splat") map
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> terrainBlendMapSRV;

	// Textures
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> terrainTexture0SRV;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> terrainTexture1SRV;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> terrainTexture2SRV;

	// Normal maps
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> terrainNormals0SRV;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> terrainNormals1SRV;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> terrainNormals2SRV;

	// Sky resources
	Mesh* skyMesh;
	SimpleVertexShader* skyVS;
	SimplePixelShader* skyPS;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> skySRV;
	Microsoft::WRL::ComPtr<ID3D11RasterizerState> skyRasterState;
	Microsoft::WRL::ComPtr<ID3D11DepthStencilState> skyDepthState;

	// Texture related resources
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> diffuseTexture;
	Microsoft::WRL::ComPtr<ID3D11ShaderResourceView> normalMap;
	//Microsoft::WRL::ComPtr<ID3D11SamplerState> samplerOptions1;

	// Sprite batch resources
	std::unique_ptr<DirectX::SpriteBatch> spriteBatch;
	std::unique_ptr<DirectX::SpriteFont> spriteFont;
};

