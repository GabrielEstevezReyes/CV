#include "ResorteAncla.h"

//se trata de recrear resortes con amortiguamiento para simular como se comportarían en la vida real
//la am

ResorteAncla::ResorteAncla(Particula a, Vector3 pE, float k, float b)
{
	K = k;
	B = b;
	ancla = a;
	Offset = pE;
}

void ResorteAncla::Update(Particula* ancla, Particula* afectada)
{
	puntoEquilibrio = ancla->Position + Offset;
	Vector3 fuerza = (afectada->Position - puntoEquilibrio)*-K - afectada->Velocity*B;
	afectada->AddForce(fuerza);
}

ResorteAncla::~ResorteAncla()
{
}
