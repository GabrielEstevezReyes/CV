#include "Particula.h"



Particula::Particula()
{
	m = 1;
	mi = 1 / m;
}

Particula::Particula(double mass)
{
	if (mass <= 0) {
		m = 1;
		mi = 1 / m;
	}
	else {
		m = mass;
		mi = 1 / mass;
	}
}


void Particula::Integrate(float dt)
{
	if (m <= 0) {
		return;
	}
	Aceleration = (AcumForce*mi);
	Velocity = (Aceleration*dt) + Velocity;
	Velocity = Velocity * reductor;
	Position = Velocity * dt + Position;
	if (Position.y > 720) {
		Position.y = 1;	
	}
	else if (Position.y < 0) {
		Position.y = 719;
	}
}

void Particula::AddForce(Vector3 force)
{
	AcumForce += force;
}

Particula::~Particula()
{
}
