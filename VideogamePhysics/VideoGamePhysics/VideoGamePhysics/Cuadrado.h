#pragma once
#include "Primitiva.h"
class Cuadrado : public Primitiva
{
public:
	vector<Vector3*> verticescuadrado = { new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f) };
	Cuadrado(Vector3 centro, float lado);
	Cuadrado();
	~Cuadrado();
};

