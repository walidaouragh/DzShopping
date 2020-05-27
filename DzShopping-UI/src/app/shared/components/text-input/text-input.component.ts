import { Component, OnInit, ViewChild, ElementRef, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
    selector: 'app-text-input',
    templateUrl: './text-input.component.html',
    styleUrls: ['./text-input.component.scss'],
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
    // @Self : Parameter decorator to be used on constructor parameters,
    // which tells the DI framework to start dependency resolution from the local injector
    // Resolution works upward through the injector hierarchy,
    // so the children of this class must configure their own providers or be prepared for a null result.
    // check (https://angular.io/api/core/Self#description) for more info
    constructor(@Self() public controlDir: NgControl) {
        this.controlDir.valueAccessor = this;
    }

    @ViewChild('input', { static: true }) input: ElementRef;
    @Input() type = 'text';
    @Input() label: string;

    ngOnInit() {
        const control = this.controlDir.control;
        const validators = control.validator ? [control.validator] : [];
        // For API validators
        const asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];

        control.setValidators(validators);
        control.setAsyncValidators(asyncValidators);
        control.updateValueAndValidity();
    }

    onChange(event) {}
    onTouched() {}

    writeValue(obj: any): void {
        this.input.nativeElement.value = obj || '';
    }

    registerOnChange(fn: any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }
}
