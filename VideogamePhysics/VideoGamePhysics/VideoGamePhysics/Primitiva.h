#pragma once
//#include "Vector3.h"
#include <iostream>
#include <vector>
#include <SDL.h>
#include "Quaternion.h"
using namespace std;

class Primitiva
{
public:
	vector<Vector3> vertices;
	Vector3 PuntoCentral;
	float Lado;
	Primitiva();
	Primitiva(Vector3 centro, float lado);
	void ConnectVertex(SDL_Renderer *renderer);
	void Draw(SDL_Renderer *renderer);
	void Translate(Vector3 trans);
	void QRotate(Quaternion rot);
	~Primitiva();
};
