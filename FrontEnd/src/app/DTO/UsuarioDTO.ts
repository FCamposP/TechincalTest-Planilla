export class UsuarioDTO {
    usuarioId:number;
    userName: string;
    password: string;
    passwordConfirmacion: string;
    actualizarPassword:boolean;
    empleadoId: number;
    empleadoNombre: string;
    intentosFallidos:number;
    activo:boolean;
    creado:Date;
}
