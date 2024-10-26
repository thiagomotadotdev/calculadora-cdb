import {
  ChangeDetectionStrategy,
  Component,
  inject,
  Input,
  LOCALE_ID,
} from '@angular/core';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { CdbService } from '../../services/cdb.service';
import { Observable } from 'rxjs';
import { ResultadoCalculoCdb } from '../../models/resultado-calculo-cdb.model';
import { CommonModule } from '@angular/common';
import {
  MatSnackBar,
  MatSnackBarAction,
  MatSnackBarActions,
  MatSnackBarLabel,
  MatSnackBarRef,
} from '@angular/material/snack-bar';

@Component({
  selector: 'tmdn-calculadora',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatChipsModule,
    MatProgressBarModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    ReactiveFormsModule,
  ],
  providers: [{ provide: LOCALE_ID, useValue: 'pt-BR' }, CdbService],
  templateUrl: './calculadora.component.html',
  styleUrl: './calculadora.component.scss',
})
export class CalculadoraComponent {
  public formGroup: FormGroup;
  public resposta?: ResultadoCalculoCdb;
  private snackBar = inject(MatSnackBar);

  constructor(public service: CdbService) {
    this.formGroup = new FormGroup({
      valorInicial: new FormControl('', [
        Validators.required,
        this.createValorInicialValidor(),
      ]),
      qtdMeses: new FormControl('', [
        Validators.required,
        this.createQtdMesesValidor(),
      ]),
    });
  }

  public onSubmit() {
    this.service
      .Calcular(
        this.formGroup.get('valorInicial')?.value,
        this.formGroup.get('qtdMeses')?.value
      )
      .subscribe(
        (resposta) => (this.resposta = resposta),
        (error) => {
          this.snackBar.open(
            error.error.ExceptionMessage
              ? error.error.ExceptionMessage
              : error.error.MessageDetail
          );
        }
      );
  }

  public printableValue(value: number): string {
    return new Intl.NumberFormat('pt-BR', { minimumFractionDigits: 2 }).format(
      value
    );
  }

  public createQtdMesesValidor(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      return Number.parseInt(control.value) > 1000 ? { qtdMeses: true } : null;
    };
  }

  public createValorInicialValidor(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      return Number.parseFloat(control.value) > 1000000000
        ? { qtdMeses: true }
        : null;
    };
  }

  public limpar() {
    this.resposta = undefined;
  }
}
