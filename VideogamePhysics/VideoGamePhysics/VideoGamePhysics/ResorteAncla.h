#pragma once
#include "Vector3.h"
#include "Particula.h"
class ResorteAncla
{
public:
	float K;
	float B;
	Particula ancla;
	Vector3 puntoEquilibrio;
	Vector3 Offset;
	ResorteAncla(Particula a, Vector3 pE, float k, float b);
	void Update(Particula* ancla, Particula* afectada);
	~ResorteAncla();
};

