import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LivroComponent } from './livro/livro.component';
import { AutorComponent } from './autor/autor.component';
import { AssuntoComponent } from './assunto/assunto.component';
import { RelatorioComponent } from './relatorio/relatorio.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'livros', component: LivroComponent },
  { path: 'autores', component: AutorComponent },
  { path: 'assuntos', component: AssuntoComponent },
  { path: 'relatorio', component: RelatorioComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
