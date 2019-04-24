#pragma once
#include "Vector3.h"
class Quaternion
{
public:
	float esc;
	Vector3 vec;
	Quaternion();
	Quaternion(float e, Vector3 v);
	Quaternion operator +(const Quaternion v);
	Quaternion operator -(const Quaternion v);
	Quaternion operator *(const Quaternion v);
	Quaternion operator *(const Vector3 v);
	Quaternion operator *(const float e);
	float Magnitud();
	void Normalize();
	void Conjugate();
	Quaternion VectoQuat(Vector3 vectq);
	~Quaternion();
	static Quaternion GetIdentity() { return Identity; }

private:
	static Quaternion Identity;
};

