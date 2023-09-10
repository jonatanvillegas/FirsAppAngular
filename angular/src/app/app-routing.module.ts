import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgregarUsuarioComponent } from './components/agregar-usuario/agregar-usuario.component';
import { TablaComponent } from './components/tabla/tabla.component';

const routes: Routes = [
  {
    path: '',
    component: TablaComponent
  },
  {
    path: 'usuarios',
    component: TablaComponent
  },
  {
    path: 'usuario/add',
    component: AgregarUsuarioComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
