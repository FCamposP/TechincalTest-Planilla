export class LogErrorDTO {
    logErrorId:number;
    modulo: string;
    entorno?: string;
    informacionAdicional?: string;
    hresult?: number;
    message?: string;
    stackTrace?: string;
    source?: string;
    targetSite?: string;
    creado?: Date;
}