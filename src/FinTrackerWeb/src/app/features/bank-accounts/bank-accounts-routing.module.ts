import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountListComponent } from './pages/account-list/account-list.component';
import { AccountFormComponent } from './pages/account-form/account-form.component';

const routes: Routes = [
    {
        path: '',
        component: AccountListComponent
    },
    {
        path: 'new',
        component: AccountFormComponent
    },
    {
        path: 'edit/:id',
        component: AccountFormComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BankAccountsRoutingModule { }