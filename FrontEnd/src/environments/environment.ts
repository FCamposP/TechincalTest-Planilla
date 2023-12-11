import { HttpHeaders } from "@angular/common/http";

export const environment = {
  production: false,
  apiUrl:'https://localhost:7120/api/',
  httOptionsDefault: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + localStorage.getItem('access_token'),
})
};
