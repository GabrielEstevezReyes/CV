#include "Vector3.h"



float Vector3::mag()
{
	return sqrt((x*x) + (y*y) + (z*z));
}

void Vector3::norm()
{
	if (x != 0 || y != 0 || z != 0) {
		float a;
		float b;
		float c;
		a = x / mag();
		b = y / mag();
		c = z / mag();
		x = a;
		y = b;
		z = c;
	}
}

Vector3::Vector3()
{
	x = 0;
	y = 0;
	z = 0;
}

Vector3::Vector3(float a, float b, float c)
{
	x = a;
	y = b;
	z = c;
}

Vector3::Vector3(float a, float b, float c, float a2, float b2, float c2)
{
	x = a2 - a;
	y = b2 - b;
	z = c2 - c;
}

Vector3::Vector3(Vector3 a, Vector3 a2)
{
	x = a2.x - a.x;
	y = a2.y - a.y;
	z = a2.z - a.z;
}

Vector3 Vector3::operator+(const Vector3 v)
{
	return Vector3(x + v.x, y + v.y, z + v.z);
}

void Vector3::operator+=(const Vector3 v)
{
	float a = x + v.x;
	float b = y + v.y;
	float c = z + v.z;
	x = a;
	y = b;
	z = c;
}

Vector3 Vector3::operator-(const Vector3 v)
{
	return Vector3(x - v.x, y - v.y, z - v.z);
}

void Vector3::operator-=(const Vector3 v)
{
	float a = x - v.x;
	float b = y - v.y;
	float c = z - v.z;
	x = a;
	y = b;
	z = c;
}

Vector3 Vector3::operator*(const float v)
{
	float a = x * v;
	float b = y * v;
	float c = z * v;
	return Vector3(a, b, c);
}

Vector3 Vector3::operator*=(const float v)
{
	float a = x * v;
	float b = y * v;
	float c = z * v;
	return Vector3(a, b, c);
}

Vector3 Vector3::operator*(const Vector3 v)
{
	float i = (y*v.z) - (z*v.y);
	float j = (x*v.z) - (z*v.x);
	float k = (x*v.y) - (y*v.x);
	return Vector3(i, -j, k);
}

Vector3 Vector3::operator*=(const Vector3 v)
{
	float i = (y*v.z) - (z*v.y);
	float j = (x*v.z) - (z*v.x);
	float k = (x*v.y) - (y*v.x);
	return Vector3(i, -j, k);
}

float Vector3::productopunto(Vector3 a, Vector3 b)
{
	return (a.x*b.x)+(a.y*b.y)+(a.z*b.z);
}

float Vector3::proyeccionEscalar(Vector3 a, Vector3 b)
{
	return a.productopunto(a, b) / b.mag();
}

Vector3 Vector3::proyeccionVectorial(Vector3 a, Vector3 b)
{
	float aux = (a.productopunto(a, b) / (b.mag()*b.mag()));
	return b * aux;
}

string Vector3::Print()
{
	string a = to_string(x);
	string b = to_string(y);
	string c = to_string(z);
	return a + " " + b + " " + c;
}

Vector3::~Vector3()
{
}
