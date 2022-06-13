import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './infraestructure/driven-adapter/auth-guard/auth.guard';
import { IsAuthGuard } from './infraestructure/driven-adapter/is-auth-guard/is-auth.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [IsAuthGuard],
    loadChildren: () =>
      import('./ui/pages/home/home.module').then((m) => m.HomeModule),
  },
  {
    path: 'categories',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./ui/pages/categories/categories.module').then((m) => m.CategoriesModule),
  },
  { path: '**', redirectTo: 'error/404' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
