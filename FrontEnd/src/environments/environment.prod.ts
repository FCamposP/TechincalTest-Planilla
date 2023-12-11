import { HttpHeaders } from "@angular/common/http";

export const environment = {
  production: true,
  apiUrl:'http://localhost:9095/api/',
  httOptionsDefault: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + localStorage.getItem('access_token'),
})
};
