#include "Matrix3.h"



Matrix3::Matrix3()
{
	i = *new Vector3(0, 0, 0);
	j = *new Vector3(0, 0, 0);
	k = *new Vector3(0, 0, 0);
}

Matrix3::Matrix3(Vector3 I, Vector3 J, Vector3 K)
{
	i = I;
	j = J;
	k = K;
}

Matrix3::Matrix3(Vector3 I, Vector3 J)
{
	i = I;
	j = J;
	k = *new Vector3(0, 0, 0);
}

Matrix3::Matrix3(Vector3 I)
{
	i = I;
	j = *new Vector3(0, 0, 0);
	k = *new Vector3(0, 0, 0);
}

// i  j  k
// 0  0  0
// 1  1  1
// 2  2  2
void Matrix3::Transpose()
{
	Vector3 aux1(i.x, j.x, k.x);
	Vector3 aux2(i.y, j.y, k.y);
	Vector3 aux3(i.z, j.z, k.z);
	i = aux1;
	j = aux2;
	k = aux3;
}

Vector3 Matrix3::operator*(const Vector3 v)
{
	Vector3 resultado(0, 0, 0);
	resultado.x = (i.x*v.x) + (j.x*v.y) + (k.x*v.z);
	resultado.y = (i.y*v.x) + (j.y*v.y) + (k.y*v.z);
	resultado.z = (i.z*v.x) + (j.z*v.y) + (k.z*v.z);
	return resultado;
}

Matrix3 Matrix3::operator*(const Matrix3 v)
{
	Vector3 aux1(i.x, j.x, k.x);
	Vector3 aux2(i.y, j.y, k.y);
	Vector3 aux3(i.z, j.z, k.z);
	Vector3 a(0, 0, 0);
	Vector3 ni(a.productopunto(aux1, i), a.productopunto(aux1, j), a.productopunto(aux1, k));
	Vector3 nj(a.productopunto(aux2, i), a.productopunto(aux2, j), a.productopunto(aux2, k));
	Vector3 nk(a.productopunto(aux3, i), a.productopunto(aux3, j), a.productopunto(aux3, k));
	return Matrix3(ni, nj, nk);
}


Matrix3::~Matrix3()
{
}
