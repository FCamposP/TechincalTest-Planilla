export class EmpleadoDTO {
    empleadoId:number;
    codigo:string;
    puestoId:number;
    nombrePuesto?: string;
    nombre: string;
    primerNombre?: string;
    segundoNombre?: string;
    primerApellido?: string;
    segundoApellido?: string;
    email: string;
    telefono:string;
    activo?: boolean;
    creado?: Date;
}