import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, Optional, SkipSelf } from '@angular/core';

import { ToastrModule } from 'ngx-toastr';

import { HttpErrorInterceptor } from './interceptors/http-error.interceptor';
import { NavbarModule } from './navbar/navbar.module';

@NgModule({
    exports: [NavbarModule],
    imports: [
        CommonModule, 
        HttpClientModule,
        ToastrModule.forRoot({
            positionClass: 'toast-bottom-center'
        }),
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
    ]
})
export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error('CoreModule is already loaded. Import only in AppModule.');
        }
    }
}
