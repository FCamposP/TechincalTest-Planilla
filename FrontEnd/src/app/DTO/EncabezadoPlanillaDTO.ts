export class EncabezadoPlanillaDTO {
    encabezadoPlanillaId:number;
    periodoId:number;
    descripcion?: string;
    periodoDescripcion?:string;
    aprobado?:boolean;
    enviarCorreo?:boolean;
    correoEnviado?:boolean;
    activo?: boolean;
    creado?: Date;
}