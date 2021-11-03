import { Bruker } from "./bruker";

export interface Log {
    id: number;
    bruker: Bruker;
    beskrivelse: string;
    datoEndret: Date;
}