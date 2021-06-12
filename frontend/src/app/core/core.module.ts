import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule, Optional, SkipSelf } from '@angular/core';

import { NavbarModule } from './navbar/navbar.module';

@NgModule({
    exports: [NavbarModule],
    imports: [CommonModule, HttpClientModule]
})
export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error('CoreModule is already loaded. Import only in AppModule.');
        }
    }
}
