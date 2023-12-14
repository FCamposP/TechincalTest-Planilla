export class DetallePlanillaDTO {
    detallePlanillaId:number;
    encabezadoPlanillaId:number;
    empleadoId:number;
    salario:number;
    descuentoIsss:number;
    descuentoAfp:number;
    descuentoRenta:number;
    otrosDescuentos:number;
    sueldoNeto:number;
    codigoEmpleado?: string;
    activo?: boolean;
    actualizar?: boolean;
    creado?: Date;
}