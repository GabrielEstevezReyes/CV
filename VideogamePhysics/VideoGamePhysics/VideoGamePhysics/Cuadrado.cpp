#include "Cuadrado.h"

Cuadrado::Cuadrado(Vector3 centro, float lado)
{
	Lado = lado;
	PuntoCentral = centro;
	for (int i = 0; i < verticescuadrado.size(); ++i) {
		vertices.push_back((*verticescuadrado[i] * Lado) + centro);
	}
}

Cuadrado::Cuadrado()
{
}


Cuadrado::~Cuadrado()
{
}
