#pragma once
#include "Primitiva.h"
class Tetraedro : public Primitiva
{
public:
	vector<Vector3*> verticestetraedro = { new Vector3(-0.5f, -0.2887f, -0.2041f), new Vector3(0.5f, -0.2887f, -0.2041f), new Vector3(0, 0.5774, -0.2041f), new Vector3(0, 0, 0.6124f) };
	Tetraedro(Vector3 centro, float lado);
	Tetraedro();
	~Tetraedro();
};
