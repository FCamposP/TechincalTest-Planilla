export class BoletaPagoDTO {
    detallePlanillaId:number;
    encabezadoPlanillaId:number;
    empleadoId?: number;
    salario?: number;
    descuentoIsss?: number;
    descuentoAfp?: number;
    descuentoRenta?: number;
    otrosDescuentos?: number;
    sueldoNeto?: number;
    descripcion?: string;
    fechaCorte?: string;
    codigoEmpleado?: string;
    nombreEmpleado?: string;
    nombrePuesto?: string;
}