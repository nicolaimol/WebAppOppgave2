import { Bilde } from "./bilde";

export interface Reise {
    id?: number;
    strekning: string;
    prisPerGjest: number;
    prisBil: number;
    bildeLink: Bilde;
    info: string;
    maLugar: boolean;
}