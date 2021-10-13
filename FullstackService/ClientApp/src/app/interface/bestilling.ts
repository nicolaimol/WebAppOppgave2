import { Lugar } from "./lugar";

export interface BestillingInfo {
    utreiseDato: string;
    hjemreiseDato?: string;
    pris: number;
    registreringsnummer?: string;
    antallLugarer: number;
    reiseId: number;
    lugar?: Lugar;
    antall_barn: number;
    antall_voksen: number;
}