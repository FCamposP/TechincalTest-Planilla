import Swal from "sweetalert2";
import { ResponseWrapperDTO } from "../DTO/responseWrapperDTO";

export function ManejarRespuesta(respuesta: ResponseWrapperDTO<any>) {
    var respuestaDevuelta: any
    switch (respuesta.status.responseStatus.codigo) {
        case 0:
            respuestaDevuelta = respuesta.data;
            break;
        case 1:
            //mostrar mensaje de error en un dialogo o consola
            Swal.fire({ position: 'top-end', icon: 'error', title: 'Ocurrió un error', text: respuesta.status.responseStatus.mensaje + " " + respuesta.status.responseStatus.mensajeError, showConfirmButton: false, timer: 5000, toast: true });
            respuestaDevuelta = null;
            break;
    }

    return respuestaDevuelta;
}