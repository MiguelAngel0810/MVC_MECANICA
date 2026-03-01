import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { VehiculoDto } from '../../models/vehiculo/VehiculoDto.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VehiculoService {
  private readonly http = inject(HttpClient);
  private apiUrl = 'https://localhost:7000/api/vehiculo';

  getAll(): Observable<VehiculoDto[]> {
    return this.http.get<VehiculoDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<VehiculoDto> {
    return this.http.get<VehiculoDto>(`${this.apiUrl}/${id}`);
  }

  create(data: VehiculoDto): Observable<VehiculoDto> {
    return this.http.post<VehiculoDto>(this.apiUrl, data);
  }

  update(id: number, data: VehiculoDto): Observable<VehiculoDto> {
    return this.http.put<VehiculoDto>(`${this.apiUrl}/${id}`, data);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
