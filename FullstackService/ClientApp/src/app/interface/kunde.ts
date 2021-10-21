export interface KontaktPerson {
    id?: number;
    fornavn: string;
    etternavn: string;
    foedselsdato: string;
    adresse: string;
    post: Post;
    telefon: string;
    epost: string;
}

export interface Kunde {
    id?: number;
    fornavn: string;
    etternavn: string;
    foedselsdato: string;
}

export interface Post {
    postNummer: string;
    postSted: string;
}

export interface KundeObj {
    kunde: Kunde,
    index: number;
    type: string;
}