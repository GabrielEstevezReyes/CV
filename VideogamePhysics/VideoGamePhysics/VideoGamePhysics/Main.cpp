#include <iostream>
#include <SDL.h>
#include <stdio.h>
#include <math.h>
#include "Timer.h"
#include "Input.h"
#include "Vector3.h"
#include "Matrix3.h"
#include "Primitiva.h"
#include "Cuadrado.h"
#include "Cubo.h"
#include "Triangulo.h"
#include "Tetraedro.h"
#include "Particula.h"
#include <assert.h>
#include "ResorteFijo.h"
#include "ResorteAncla.h"
#include "Quaternion.h"

using namespace std;

const int WIDTH = 1080, HEIGHT = 720;

float PX = 540;
float PY = 0;
float PZ = 0;

Vector3 CentroPantalla(WIDTH / 2, HEIGHT / 2, 0);

Particula part(1);
Primitiva cubito = Cubo(CentroPantalla, 200);

Vector3 vec11(cubito.vertices[0], cubito.vertices[1]);
Vector3 vec12(cubito.vertices[0], cubito.vertices[2]);
Vector3 vec13 = vec11 * vec12;

float angulo = 0.0001745329;
float angulo2;


void drawVector(SDL_Renderer *renderer, Vector3 centro, Vector3 v) {
	SDL_RenderDrawLine(renderer, centro.x, centro.y, centro.x + v.x, centro.y + v.y);
}

void drawVector(SDL_Renderer *renderer, Vector3 v) {
	SDL_RenderDrawLine(renderer, CentroPantalla.x, CentroPantalla.y, v.x, v.y);
}

void drawPlaneFrom2Vectors(SDL_Renderer *renderer, Vector3 centro, Vector3 v1, Vector3 v2) {
	drawVector(renderer, centro, v1);
	drawVector(renderer, centro, v2);	
	drawVector(renderer, centro + v1, v2);
	drawVector(renderer, centro + v2, v1);
}

bool HitColision(SDL_Renderer *renderer, Vector3 centroPlano, Vector3 vecPlanoA, Vector3 vecPlanoB, Particula particula) {
	Vector3 acruzb = vecPlanoA * vecPlanoB;
	Vector3 posicionPart = particula.Position - centroPlano;
	Vector3 P0(0, 0, 0);
	Vector3 P0sN = P0.proyeccionVectorial(posicionPart, acruzb);
	Vector3 P0sU = P0.proyeccionVectorial(posicionPart, vecPlanoA);
	Vector3 P0sV = P0.proyeccionVectorial(posicionPart, vecPlanoB);
	Vector3 vecanorm = vecPlanoA;
	Vector3 P0sUn = P0sU;
	Vector3 vecbnorm = vecPlanoB;
	Vector3 P0sVn = P0sV;
	vecanorm.norm();
	P0sUn.norm();
	float Ppunto1 = P0sU.productopunto(P0sUn, vecanorm);
	vecbnorm.norm();
	P0sVn.norm();
	float Ppunto2 = P0sU.productopunto(P0sVn, vecbnorm);

	SDL_SetRenderDrawColor(renderer, 0, 255, 0, 0);
	drawVector(renderer, centroPlano, acruzb);
	SDL_SetRenderDrawColor(renderer, 255, 255, 0, 0);
	drawVector(renderer, centroPlano, P0sN * -1);
	SDL_SetRenderDrawColor(renderer, 0, 255, 255, 0);
	drawVector(renderer, centroPlano, P0sU);
	SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);
	drawVector(renderer, centroPlano, P0sV);

	if (P0sN.mag() >= -1 && P0sN.mag() <= 1 && Ppunto2 <= 1 && Ppunto2 >= 0 && P0sV.mag() <= vecPlanoB.mag() && Ppunto1 <= 1 && Ppunto1 >= 0 && P0sU.mag() <= vecPlanoA.mag()) {
		return true;
	}
	return false;
}

bool HitColision(Vector3 centroPlano, Vector3 vecPlanoA, Vector3 vecPlanoB, Particula particula) {
	Vector3 acruzb = vecPlanoA * vecPlanoB;
	Vector3 posicionPart = particula.Position - centroPlano;
	Vector3 P0(0, 0, 0);
	Vector3 P0sN = P0.proyeccionVectorial(posicionPart, acruzb);
	Vector3 P0sU = P0.proyeccionVectorial(posicionPart, vecPlanoA);
	Vector3 P0sV = P0.proyeccionVectorial(posicionPart, vecPlanoB);
	Vector3 vecanorm = vecPlanoA;
	Vector3 P0sUn = P0sU;
	Vector3 vecbnorm = vecPlanoB;
	Vector3 P0sVn = P0sV;
	vecanorm.norm();
	P0sUn.norm();
	float Ppunto1 = P0sU.productopunto(P0sUn, vecanorm);
	vecbnorm.norm();
	P0sVn.norm();
	float Ppunto2 = P0sU.productopunto(P0sVn, vecbnorm);
	if (P0sN.mag() >= -1 && P0sN.mag() <= 1 && Ppunto2 <= 1 && Ppunto2 >= 0 && P0sV.mag() <= vecPlanoB.mag() && Ppunto1 <= 1 && Ppunto1 >= 0 && P0sU.mag() <= vecPlanoA.mag()) {
		return true;
	}
	return false;
}

Vector3 getRotationAxis(Primitiva a, Particula b) {
	Vector3 centroPrim = a.PuntoCentral;
	Vector3 centroaPunto = b.Position - centroPrim;
	Vector3 eje = centroaPunto * b.Velocity;
	return eje;
}

int main(int argc, char *argv[])
{
	if (SDL_Init(SDL_INIT_EVERYTHING) < 0)
	{
		cout << "SDL could not initialize! SDL Error: " << SDL_GetError() << endl;
	}

	SDL_Window *window = SDL_CreateWindow("Physics Engine", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, WIDTH, HEIGHT, SDL_WINDOW_ALLOW_HIGHDPI);

	if (window == NULL)
	{
		cout << "Could not create window: " << SDL_GetError() << endl;
		return EXIT_FAILURE;
	}

	SDL_Event windowEvent;
	SDL_Renderer* renderer = SDL_CreateRenderer(window, -1, 0);	

	cout << "A,W,S,D to move the particle on the 2 dimensions of the camera, T,G to move the particle on the z axis, this will cause a rotation to the cube depending on the point of collition, only one face of the cube is prone to hits (upper face)" << endl;

	part.Position = { PX, PY, PZ };

	Vector3 gravedad(0, 9.81, 0);
	Vector3 ejerot;

	int position = 0;
	
	while (true)
	{
		if (SDL_PollEvent(&windowEvent))
		{
			if (windowEvent.type == SDL_QUIT)
			{
				break;
			}
			Input::Update(windowEvent);
		}

		if (Input::GetKey(SDLK_q)) {
			part.AcumForce = { 0,0,0 };
		}
		if (Input::GetKey(SDLK_a)) {
			part.Position.x--;
		}
		if (Input::GetKey(SDLK_d)) {
			part.Position.x++;
		}
		if (Input::GetKey(SDLK_w)) {
			part.Position.y++;
		}
		if (Input::GetKey(SDLK_s)) {
			part.Position.y--;
		}
		if (Input::GetKey(SDLK_t)) {
			part.Position.z++;
		}
		if (Input::GetKey(SDLK_g)) {
			part.Position.z--;
		}

		if (position % 100 == 0) {
			SDL_SetRenderDrawColor(renderer, 64, 64, 64, 0);
			SDL_RenderClear(renderer);
		}

		Timer::Update();

		part.AddForce(gravedad);
		part.Integrate(Timer::GetDeltaTime()/5);
		
		SDL_SetRenderDrawColor(renderer, 255, 255, 255, 0);
		cubito.ConnectVertex(renderer);

		SDL_SetRenderDrawColor(renderer, 255, 0, 0, 0);
		SDL_RenderDrawPoint(renderer, part.Position.x, part.Position.y);

		if (HitColision(cubito.vertices[0], vec11, vec12, part)) {
			ejerot = getRotationAxis(cubito, part);
			ejerot.norm();
			angulo2 = angulo;
		}

		Quaternion rotacion(cos(angulo2 / 2), ejerot*sin(angulo2 / 2));
		cubito.QRotate(rotacion);

		position++;

		SDL_RenderPresent(renderer);
	}

	SDL_DestroyRenderer(renderer);
	SDL_DestroyWindow(window);
	SDL_Quit();

	return EXIT_SUCCESS;
}
