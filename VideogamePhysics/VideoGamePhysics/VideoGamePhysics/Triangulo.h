#pragma once
#include "Primitiva.h"
class Triangulo : public Primitiva
{
public:
	vector<Vector3*> verticestriangulo = { new Vector3(-0.5f, -0.2887f, -0.2041f), new Vector3(0.5f, -0.2887f, -0.2041f), new Vector3(0, 0.5774, -0.2041f) };
	Triangulo();
	Triangulo(Vector3 centro, float lado);
	~Triangulo();
};

