#pragma once
#include "Vector3.h"
#include "Particula.h"
class ResorteFijo
{
public:
	float K;
	float B;
	Vector3 puntoFijo;
	Vector3 puntoEquilibrio;
	ResorteFijo(Vector3 pF, Vector3 pE, float k, float b);
	void Update(Particula* afectada);
	~ResorteFijo();
};
