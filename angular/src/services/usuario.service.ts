import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http"
import { environment } from 'src/enviroment/environment';
import { Usuario } from 'src/app/models/usuario.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  GetAllCliente(): Observable<Usuario[]>{
    return this.http.get<Usuario[]>(this.baseApiUrl + '/api/Cliente');
  }
  addUsuario(agregarUsuario: Usuario):  Observable<Usuario>{
    return this.http.post<Usuario>(this.baseApiUrl + '/api/Cliente', agregarUsuario);
  }
}
