#include "Primitiva.h"

Primitiva::Primitiva()
{
}

Primitiva::Primitiva(Vector3 centro, float lado)
{
	PuntoCentral = centro;
	Lado = lado;	
}

void Primitiva::ConnectVertex(SDL_Renderer *renderer)
{
	for (int i = 0; i < vertices.size() - 1; ++i) {
		for (int j = i + 1; j < vertices.size(); ++j) {
			Vector3 dist = vertices[i] - vertices[j];
			float distancia = dist.mag();
			if (distancia >= Lado/5) {
				SDL_RenderDrawLine(renderer, vertices[i].x, vertices[i].y, vertices[j].x, vertices[j].y);
			}
		}
	}
}

void Primitiva::Draw(SDL_Renderer * renderer)
{
	for (int i = 0; i < vertices.size() - 1; ++i) {
		SDL_RenderDrawPoint(renderer, vertices[i].x, vertices[i].y);
	}
}

void Primitiva::Translate(Vector3 trans)
{
	for (int i = 0; i < vertices.size(); ++i) {
		vertices[i] = vertices[i] + trans;
	}
}

void Primitiva::QRotate(Quaternion rot)
{
	rot.Normalize();
	Quaternion qc = rot;
	qc.Conjugate();
	for (int i = 0; i < vertices.size(); i++) {
		vertices[i] = vertices[i] - PuntoCentral;
		Quaternion a(0, vertices[i]);
		Quaternion b = a * qc;
		Quaternion c = rot * b;
		vertices[i] = c.vec;
		vertices[i] = vertices[i] + PuntoCentral;
	}
}


Primitiva::~Primitiva()
{
}
