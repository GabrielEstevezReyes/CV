#pragma once
#include "Vector3.h"
#include <assert.h>
class Particula
{
public:
	double m = 0;
	double mi = 0;
	float reductor = 0.99f;
	Particula();
	Particula(double m);
	Vector3 AcumForce = { 0,0,0 };
	Vector3 Position = { 0,0,0 };
	Vector3 Velocity = { 0,0,0 };
	Vector3 Aceleration = { 0,0,0 };
	void Integrate(float deltatime);
	void AddForce(Vector3 fuerza);
	~Particula();
};
