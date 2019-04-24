#pragma once
#include <math.h>
#include <iostream>
#include <string>
using namespace std;

class Vector3
{
public:
	float x;
	float y;
	float z;
	float mag();
	void norm();
	Vector3();
	Vector3(float a, float b, float c);
	Vector3(float a, float b, float c, float a2, float b2, float c2);
	Vector3(Vector3 a, Vector3 a2);
	Vector3 operator +(const Vector3 v);
	void operator +=(const Vector3 v);
	Vector3 operator -(const Vector3 v);
	void operator -=(const Vector3 v);
	Vector3 operator *(const float v);
	Vector3 operator *=(const float v);
	Vector3 operator *(const Vector3 v);
	Vector3 operator *=(const Vector3 v);
	float productopunto(Vector3 a, Vector3 b);
	float proyeccionEscalar(Vector3 a, Vector3 b);
	Vector3 proyeccionVectorial(Vector3 a, Vector3 b);
	string Print();
	~Vector3();
};
