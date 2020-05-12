import { Component, OnInit } from '@angular/core';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-section-header',
    templateUrl: './section-header.component.html',
    styleUrls: ['./section-header.component.css'],
})
export class SectionHeaderComponent implements OnInit {
    constructor(private breadcrumbService: BreadcrumbService) {}
    public breadcrumb$: Observable<any[]>;

    ngOnInit() {
        this.breadcrumb$ = this.breadcrumbService.breadcrumbs$;
    }
}
