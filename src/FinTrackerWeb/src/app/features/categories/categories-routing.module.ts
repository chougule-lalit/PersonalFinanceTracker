// src/app/features/categories/categories-routing.module.ts (Replace entire file)
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './pages/category-list/category-list.component';
import { CategoryFormComponent } from './pages/category-form/category-form.component';

const routes: Routes = [
    {
        path: '',
        component: CategoryListComponent
    },
    {
        path: 'new',
        component: CategoryFormComponent
    },
    {
        path: 'edit/:id',
        component: CategoryFormComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoriesRoutingModule { }