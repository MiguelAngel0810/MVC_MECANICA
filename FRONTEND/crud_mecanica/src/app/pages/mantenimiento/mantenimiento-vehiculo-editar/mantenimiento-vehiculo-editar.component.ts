import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
} from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { VehiculoDto } from '../../../models/vehiculo/VehiculoDto.model';
import { VehiculoService } from '../../../services/vehiculo/vehiculo.service';

@Component({
  selector: 'app-mantenimiento-vehiculo-editar',
  imports: [ReactiveFormsModule],
  templateUrl: './mantenimiento-vehiculo-editar.component.html',
  styleUrls: ['./mantenimiento-vehiculo-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoVehiculoEditarComponent {
  vehiculo = input<VehiculoDto | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  cancelado = output<void>();
  guardado = output<VehiculoDto>(); // ahora emitimos el objeto actualizado

  private readonly vehiculoService = inject(VehiculoService);
  private readonly formBuilder = inject(FormBuilder);

  readonly form = this.formBuilder.group({
    placa: ['', [Validators.required]],
    marca: ['', [Validators.required]],
    modelo: ['', [Validators.required]],
    anio: [0, [Validators.required]],
  });

  cargando = false;

  constructor() {
    effect(() => {
      const v = this.vehiculo();
      this.form.reset({
        placa: v?.placa ?? '',
        marca: v?.marca ?? '',
        modelo: v?.modelo ?? '',
        anio: v?.anio ?? 0,
      });
    });
  }

  onCancelar(): void {
    this.cancelado.emit();
  }

  onGuardar(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid || this.cargando) return;

    this.cargando = true;

    const valores = this.form.getRawValue();
    const actual = this.vehiculo();

    const payload: VehiculoDto = {
      vehiculoId: actual?.vehiculoId ?? 0,
      placa: valores.placa,
      marca: valores.marca,
      modelo: valores.modelo,
      anio: valores.anio,
    };

    const request$ = this.modo() === 'editar' && payload.vehiculoId > 0
      ? this.vehiculoService.update(payload.vehiculoId, payload)
      : this.vehiculoService.create(payload);

    request$.subscribe({
      next: (resp) => {
        this.guardado.emit(resp); // emitimos el objeto actualizado/creado
      },
      error: (error) => {
        console.error('Error al guardar vehículo', error);
        this.cargando = false;
      },
      complete: () => {
        this.cargando = false;
      },
    });
  }
}
