import { HttpHeaders } from "@angular/common/http";

export const environment = {
  production: true,
  apiUrl:'https://planillatest.somee.com/Planilla/api/',
  httOptionsDefault: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + localStorage.getItem('access_token'),
})
};
