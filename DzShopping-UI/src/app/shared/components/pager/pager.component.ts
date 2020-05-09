import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
    selector: 'app-pager',
    templateUrl: './pager.component.html',
    styleUrls: ['./pager.component.scss'],
})
export class PagerComponent implements OnInit {
    constructor() {}

    @Output() pageChanged: EventEmitter<number> = new EventEmitter<number>();
    @Input() totalCount: number;
    @Input() pageSize: number;

    ngOnInit() {}

    onPageChanged(event: any) {
        this.pageChanged.emit(event.page);
    }
}
