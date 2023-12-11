export interface ResponseWrapperDTO<T> {
    data: T;
    status: GlobalStatusDTO;
}

export interface GlobalStatusDTO {
    requestStatus?: RequestStatusDTO;
    responseStatus?: ResponseStatusDTO;
}

export interface RequestStatusDTO {
    codigo?: number;
    message?: string;
}

export interface ResponseStatusDTO {
    codigo?: number;
    mensaje?: string;
    mensajeError?: string;
}