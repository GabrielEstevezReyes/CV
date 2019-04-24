#include "ResorteFijo.h"

ResorteFijo::ResorteFijo(Vector3 pF, Vector3 pE, float k, float b)
{
	K = k;
	B = b;
	puntoFijo = pF;
	puntoEquilibrio = pE;
}

void ResorteFijo::Update(Particula* afectada)
{
	Vector3 fuerza = (afectada->Position - puntoEquilibrio)*-K - afectada->Velocity*B;
	afectada->AddForce(fuerza);
}

ResorteFijo::~ResorteFijo()
{
}
