#include "Quaternion.h"
Quaternion Quaternion::Identity = { 0,{ 1,0,0 } };
Quaternion::Quaternion()
{
	esc = 1;
	vec = { 0,0,0 };
}

Quaternion::Quaternion(float e, Vector3 v)
{
	esc = e;
	vec = v;
}

Quaternion Quaternion::operator+(const Quaternion v)
{
	return Quaternion(esc + v.esc, vec + v.vec);
}

Quaternion Quaternion::operator-(const Quaternion v)
{
	return Quaternion(esc - v.esc, vec - v.vec);
}

Quaternion Quaternion::operator*(const Quaternion v)
{
	float nfloat1 = esc * v.esc;
	float nfloat2 = vec.productopunto(vec, v.vec);
	float nfloat3 = nfloat1 - nfloat2;
	Vector3 cross = vec * v.vec;
	Vector3 v1 = v.vec;
	v1 *= esc;
	Vector3 v2 = vec;
	v2 *= v.esc;
	Vector3 sum = v1 + v2 + cross;
	return Quaternion(nfloat3, sum);
}

Quaternion Quaternion::operator*(const Vector3 v)
{
	Quaternion me(esc, vec);
	Quaternion nQ(0, v);
	return me * nQ;
}

Quaternion Quaternion::operator*(const float e)
{
	return Quaternion(esc*e, vec*e);
}

float Quaternion::Magnitud()
{
	return std::sqrt((esc*esc) + (vec.x*vec.x) + (vec.y*vec.y) + (vec.z*vec.z));
}

void Quaternion::Normalize()
{
	esc = esc / this->Magnitud();
	vec = { vec.x / this->Magnitud() ,vec.y / this->Magnitud() ,vec.z / this->Magnitud() };
}

void Quaternion::Conjugate()
{
	vec = vec * -1;
}

Quaternion Quaternion::VectoQuat(Vector3 vectq)
{
	return Quaternion(0, vectq);
}

Quaternion::~Quaternion()
{
}
