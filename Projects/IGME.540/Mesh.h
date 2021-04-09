#pragma once
#include "Vertex.h"
#include <d3d11.h>
#include <wrl/client.h>
#include <fstream>
#include <vector>

class Mesh
{
private:
	// Buffers to hold geometry data
	Microsoft::WRL::ComPtr<ID3D11Buffer> vertexBuffer;
	Microsoft::WRL::ComPtr<ID3D11Buffer> indexBuffer;
	// int to hold the number of indices in the index buffer
	int numberOfIndices;
	int numberOfVertices;
	

public:
	Mesh();

	Mesh(
		Vertex* vertices,
		int numberOfVertices,
		unsigned int* indices,
		int numberOfIndices,
		Microsoft::WRL::ComPtr<ID3D11Device> device);
	Mesh(
		const char* filename,
		Microsoft::WRL::ComPtr<ID3D11Device> device);
	~Mesh();

	void CalculateTangents(Vertex* verts, int numVerts, unsigned int* indices, int numIndices);
	void CreateBuffers(Vertex* vertexes, unsigned int* indexes, int indicesNum, int vertexsNum, Microsoft::WRL::ComPtr<ID3D11Device> deviceTwo2);
	void SetBuffersAndDraw(Microsoft::WRL::ComPtr<ID3D11DeviceContext> context);


	Microsoft::WRL::ComPtr<ID3D11Buffer> GetVertexBuffer();
	Microsoft::WRL::ComPtr<ID3D11Buffer> GetIndexBuffer();
	int GetIndexCount();

	std::vector<Vertex> GetVertices();
	int GetNumberOfVertices();
	std::vector<Vertex> vertices;
};

