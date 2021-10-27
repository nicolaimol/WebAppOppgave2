import { KontaktPerson, Kunde } from "./kunde";
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

export interface Bestilling {
    id?: number;
    referanse?: string
    ferjestrekning?: string;
    utreiseDato: string;
    hjemreiseDato?: string;
    pris: number;
    registreringsnummer?: string;
    antallLugarer: number;
    reiseId: number;
    lugarType?: Lugar;
    kontaktPerson: KontaktPerson,
    voksne?: Kunde[],
    barn?: Kunde[]
}