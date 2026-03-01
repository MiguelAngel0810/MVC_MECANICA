import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { VehiculoService } from '../../../services/vehiculo/vehiculo.service';
import { VehiculoDto } from '../../../models/vehiculo/VehiculoDto.model';
import { MantenimientoVehiculoEditarComponent } from '../mantenimiento-vehiculo-editar/mantenimiento-vehiculo-editar.component';

@Component({
  selector: 'app-mantenimiento-vehiculo-list',
  imports: [MantenimientoVehiculoEditarComponent],
  templateUrl: './mantenimiento-vehiculo-list.component.html',
  styleUrls: ['./mantenimiento-vehiculo-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoVehiculoListComponent implements OnInit {
  private readonly _vehiculoService = inject(VehiculoService);

  vehiculos = signal<VehiculoDto[]>([]);
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  vehiculoSeleccionado: VehiculoDto | null = null;

  ngOnInit() {
    this.getAllVehiculos();
  }

  getAllVehiculos() {
    this._vehiculoService.getAll().subscribe({
      next: (data) => {
        this.vehiculos.set(data);
      },
      error: (err) => { console.log("Ocurrió un error", err); }
    });
  }

  abrirAgregar(): void {
    this.modoEdicion = 'crear';
    this.vehiculoSeleccionado = null;
    this.mostrarModal = true;
  }

  abrirEditar(vehiculo: VehiculoDto): void {
    this.modoEdicion = 'editar';
    this.vehiculoSeleccionado = { ...vehiculo };
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.vehiculoSeleccionado = null;
  }

  onGuardado(vehiculoActualizado: VehiculoDto): void {
    this.cerrarModal();

    if (this.modoEdicion === 'crear') {
      this.vehiculos.set([...this.vehiculos(), vehiculoActualizado]);
    } else {
      const lista = this.vehiculos().map(v =>
        v.vehiculoId === vehiculoActualizado.vehiculoId ? vehiculoActualizado : v
      );
      this.vehiculos.set(lista);
    }
  }

  eliminarVehiculo(vehiculo: VehiculoDto): void {
    const confirmado = window.confirm(
      `¿Está seguro de eliminar el vehículo con placa ${vehiculo.placa}?`
    );
    if (!confirmado) return;

    this._vehiculoService.delete(vehiculo.vehiculoId).subscribe({
      next: () => {
        this.getAllVehiculos();
      },
      error: (err) => {
        console.log('Ocurrió un error al eliminar', err);
      },
    });
  }
}
