export interface Bruker {
    id?: number;
    brukernavn: string;
    passord: string;
}

export interface BrukerUpdate {
    id?: number;
    brukernavn: string;
    passord: string;
    NyttPassord: string;
}
