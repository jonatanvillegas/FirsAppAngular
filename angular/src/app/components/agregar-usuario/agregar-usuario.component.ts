import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario.model';
import { UsuarioService } from 'src/services/usuario.service';

@Component({
  selector: 'app-agregar-usuario',
  templateUrl: './agregar-usuario.component.html',
  styleUrls: ['./agregar-usuario.component.css']
})
export class AgregarUsuarioComponent implements OnInit{

  agregarUsuario : Usuario = {
    id: 0,
    nombre: '',
    direccion: '',
    telefono: 0
  }
  constructor(private usuariosService: UsuarioService, private router: Router){


  }

  ngOnInit(): void{

  }

  addUsuario() {
    this.usuariosService.addUsuario(this.agregarUsuario)
    .subscribe({
      next: (usuario) => {
        this.router.navigate(['/'])
      }
    })
  }
}
