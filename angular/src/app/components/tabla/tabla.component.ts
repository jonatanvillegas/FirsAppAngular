import { Component,OnInit } from '@angular/core';
import { Usuario } from 'src/app/models/usuario.model';
import { UsuarioService } from 'src/services/usuario.service';

@Component({
  selector: 'app-tabla',
  templateUrl: './tabla.component.html',
  styleUrls: ['./tabla.component.css']
})
export class TablaComponent {

  usuario : Usuario[] = [];
  constructor(private usuariosService: UsuarioService){

  }
  ngOnInit(): void {
    this.usuariosService.GetAllCliente()
    .subscribe({
      next: (usuarios) => {
        this.usuario = usuarios;

        console.log(usuarios)
      },
      error:(response) => {
        console.log(response);
      }
    })
  }
}
