#include "Triangulo.h"



Triangulo::Triangulo()
{
}

Triangulo::Triangulo(Vector3 centro, float lado)
{
	PuntoCentral = centro;
	Lado = lado;
	for (int i = 0; i < verticestriangulo.size(); ++i) {
		vertices.push_back((*verticestriangulo[i] * Lado) + centro);
	}
}


Triangulo::~Triangulo()
{
}
