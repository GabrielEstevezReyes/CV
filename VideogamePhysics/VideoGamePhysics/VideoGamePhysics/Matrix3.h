#pragma once
#include "Vector3.h"
class Matrix3
{
public:
	// i  j  k
	// 0  0  0
	// 1  1  1
	// 2  2  2
	Vector3 i;
	Vector3 j;
	Vector3 k;
	Matrix3();
	Matrix3(Vector3 I, Vector3 J, Vector3 K);
	Matrix3(Vector3 I, Vector3 J);
	Matrix3(Vector3 I);
	void Transpose();
	Vector3 operator *(const Vector3 v);
	Matrix3 operator *(const Matrix3 v);
	~Matrix3();
};

