import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable, catchError, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseWrapperDTO } from '../DTO/responseWrapperDTO';
import { ManejarRespuesta } from '../utilities/ManejarRespuesta';

@Injectable({
    providedIn: 'root',
})

export class appServices<T> {

    private urlEndpoint: string
    constructor(private http: HttpClient) {
        this.urlEndpoint = environment.apiUrl;
    }

    private httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
        timeout: 1000000,
    };

    private httOptionsDefault = new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + localStorage.getItem('access_token'),
    });

    private httOptionsExport = new HttpHeaders({
        'Content-Type': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
        'Content-Disposition': 'attachment; filename=p_establecimientos.xlsx',
        'Response-Type': 'blob',
        Authorization: 'Bearer ' + localStorage.getItem('access_token'),
    });

    OtroGet(controller: string, method: string, queryParams?: HttpParams): Observable<T> {
        let ruta = controller + "/" + (method != "" ? method + "/" : "");
        return this.http.get<ResponseWrapperDTO<T[]>>(this.urlEndpoint + ruta, { params: queryParams, headers: this.httOptionsDefault }).pipe(
            map(response => {
                return ManejarRespuesta(response);
            }),
            catchError((error: any) => {
                //agregar método reutilizable para mostrar errores ocurridos en el request
                return null
            })
        )
    }

    OtroPost(controller: string, method: string, body:any, queryParams=null): Observable<T> {
        if(!queryParams){
            queryParams = new HttpParams();
        }
        queryParams = queryParams.append("userId", localStorage.getItem('userId'));
        let ruta = controller + "/" + (method != "" ? method + "/" : "");
        return this.http.post<ResponseWrapperDTO<T[]>>(this.urlEndpoint + ruta, body, { params: queryParams, headers: this.httOptionsDefault }).pipe(
            map(response => {
                return ManejarRespuesta(response);
            }),
            catchError((error: any) => {
                return null
            })
        )
    }

    
    CargarArchivos(controller: string, method: string, body:any, queryParams=null): Observable<T> {
        if(!queryParams){
            queryParams = new HttpParams();
        }
        queryParams = queryParams.append("userId", localStorage.getItem('userId'));
        let ruta = controller + "/" + (method != "" ? method + "/" : "");
        return this.http.post<ResponseWrapperDTO<T[]>>(this.urlEndpoint + ruta, body, { params: queryParams }).pipe(
            map(response => {
                return ManejarRespuesta(response);
            }),
            catchError((error: any) => {
                return null
            })
        )
    }

    OtroPut(controller: string, method: string, body: any, queryParams = null): Observable<T> {
        if (!queryParams) {
            queryParams = new HttpParams();
        }
        queryParams = queryParams.append("userId", localStorage.getItem('userId'));
        let ruta = controller + "/" + (method != "" ? method + "/" : "");
        return this.http.put<ResponseWrapperDTO<T[]>>(this.urlEndpoint + ruta, body, { params: queryParams, headers: this.httOptionsDefault }).pipe(
            map(response => {
                return ManejarRespuesta(response);
            }),
            catchError((error: any) => {
                //agregar método reutilizable para mostrar errores ocurridos en el request
                return null
            })
        )
    }

    OtroDelete(controller: string, method: string, queryParams?: HttpParams): Observable<T> {
        if (!queryParams) {
            queryParams = new HttpParams();
        }
        queryParams = queryParams.append("userId", localStorage.getItem('userId'));
        let ruta = controller + "/" + (method != "" ? method + "/" : "");
        return this.http.delete<ResponseWrapperDTO<T[]>>(this.urlEndpoint + ruta, { params: queryParams, headers: this.httOptionsDefault }).pipe(
            map(response => {
                return ManejarRespuesta(response);
            }),
            catchError((error: any) => {
                //agregar método reutilizable para mostrar errores ocurridos en el request
                return null
            })
        )
    }

    DownloadFile(controller: string, method: string): Observable<any> {
        let ruta = controller + "/" + (method != "" ? method + "/" : "");
        return this.http.get(this.urlEndpoint + ruta, {responseType: 'blob' }).pipe(
            map(response => {
                return response;
            }),
            catchError((error: any) => {
                return null
            })
        )
    }

    getExcel(controller: string, method: string[]): Observable<T> {

      responseType: 'blob'
      let ruta = controller + '/' + method.join('/');
      if (localStorage['access_token']) {
          this.httpOptions = {
              headers: new HttpHeaders({
                  'Content-Type': 'application/json',
                  Authorization: 'Bearer ' + localStorage.getItem('access_token'),
              }),
              timeout: 10000
          };
      }


      return new Observable<T>((observer) => {
          let subs = this.http.get(`${environment.apiUrl}${ruta}`, { responseType: 'blob' }).subscribe(
              (response: any) => {
                  observer.next(response);
                  subs.unsubscribe();
              },
              async (err) => {
                  // this.ErrorHandler(err);
                  observer.error(err);
              }
          );
      });
  }

}
