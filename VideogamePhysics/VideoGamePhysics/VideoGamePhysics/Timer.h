//Resource: https://wiki.libsdl.org/SDL_GetTicks
#pragma once
#include <SDL.h>

class Timer
{
public:
	static void Update()
	{
		currentTime = SDL_GetTicks();
		deltaTime = currentTime - lastTime;
		lastTime = currentTime;
	}

	static float GetDeltaTime() { return (float)deltaTime / 1000; }
	static float GetTotalTime() { return (float)currentTime / 1000; }

private:
	static unsigned int currentTime;
	static unsigned int lastTime;
	static unsigned int deltaTime;
};

unsigned int Timer::currentTime = 0;
unsigned int Timer::lastTime = 0;
unsigned int Timer::deltaTime = 0;