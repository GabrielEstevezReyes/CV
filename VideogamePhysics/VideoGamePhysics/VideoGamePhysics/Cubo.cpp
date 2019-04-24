#include "Cubo.h"



Cubo::Cubo()
{
}

Cubo::Cubo(Vector3 centro, float lado)
{
	Lado = lado;
	PuntoCentral = centro;
	for (int i = 0; i < verticescubo.size(); ++i) {
		vertices.push_back((verticescubo[i] * Lado) + centro);
	}
}


Cubo::~Cubo()
{
}
