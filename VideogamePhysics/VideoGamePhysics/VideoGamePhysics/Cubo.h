#pragma once
#include "Primitiva.h"
class Cubo : public Primitiva
{
public:
	vector<Vector3> verticescubo = { 
		Vector3(0.5f, -0.5f, 0.5f),
		Vector3(-0.5f, -0.5f, 0.5f),
		Vector3(0.5f, -0.5f, -0.5f), 
		Vector3(-0.5f, 0.5f, 0.5f),
		Vector3(-0.5f, -0.5f, -0.5f), 
		Vector3(0.5f, 0.5f, -0.5f), 
		Vector3(-0.5f, 0.5f, -0.5f), 
		Vector3(0.5f, 0.5f, 0.5f)
	};
	Cubo();
	Cubo(Vector3 centro, float lado);
	~Cubo();
};

