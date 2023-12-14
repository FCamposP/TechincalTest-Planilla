import { DetallePlanillaDTO } from "./DetallePlanillaDTO";

export class EncabezadoPlanillaDTO {
    encabezadoPlanillaId:number;
    estadoPlanillaId:number;
    periodoId:number;
    descripcion?: string;
    periodoDescripcion?:string;
    nombreEstado?:string;
    descripcionPeriodo?:string;
    aprobado?:boolean;
    enviarCorreo?:boolean;
    correoEnviado?:boolean;
    habilitado?:boolean;
    activo?: boolean;
    creado?: Date;
    detallePlanilla?: DetallePlanillaDTO[];

}