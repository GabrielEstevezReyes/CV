#include "Tetraedro.h"



Tetraedro::Tetraedro(Vector3 centro, float lado)
{
	Lado = lado;
	PuntoCentral = centro;
	for (int i = 0; i < verticestetraedro.size(); ++i) {
		vertices.push_back((*verticestetraedro[i] * Lado) + centro);
	}
}

Tetraedro::Tetraedro()
{
}


Tetraedro::~Tetraedro()
{
}
