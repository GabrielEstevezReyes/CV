#pragma once
#include <SDL.h>

class Input
{
public:
	static void Update(SDL_Event windowEvent)
	{
		e = windowEvent;
	}

	static bool GetKey(SDL_Keycode KeyCode)
	{
		if (e.type == SDL_KEYDOWN)
		{
			if (e.key.keysym.sym == KeyCode)
			{
				return true;
			}
			else return false;
		}
		else return false;
	}

	static SDL_Event e;
};

SDL_Event Input::e;