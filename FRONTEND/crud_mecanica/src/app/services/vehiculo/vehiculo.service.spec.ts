/* tslint:disable:no-unused-variable */
import { TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { VehiculoService } from './vehiculo.service';
import { VehiculoDto } from '../../models/vehiculo/VehiculoDto.model';

describe('VehiculoService', () => {
  let service: VehiculoService;
  let httpMock: HttpTestingController;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [VehiculoService]
    });
    service = TestBed.inject(VehiculoService);
    httpMock = TestBed.inject(HttpTestingController);
  }));

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call getAll', () => {
    service.getAll().subscribe();
    const req = httpMock.expectOne('https://localhost:7000/api/vehiculo');
    expect(req.request.method).toBe('GET');
    req.flush([]);
  });

  it('should call getById', () => {
    service.getById(1).subscribe();
    const req = httpMock.expectOne('https://localhost:7000/api/vehiculo/1');
    expect(req.request.method).toBe('GET');
    req.flush({});
  });

  it('should call create', () => {
    const vehiculo: VehiculoDto = { vehiculoId: 0, placa: '', marca: '', modelo: '', anio: 0 };
    service.create(vehiculo).subscribe();
    const req = httpMock.expectOne('https://localhost:7000/api/vehiculo');
    expect(req.request.method).toBe('POST');
    req.flush({});
  });

  it('should call update', () => {
    const vehiculo: VehiculoDto = { vehiculoId: 1, placa: '', marca: '', modelo: '', anio: 0 };
    service.update(1, vehiculo).subscribe();
    const req = httpMock.expectOne('https://localhost:7000/api/vehiculo/1');
    expect(req.request.method).toBe('PUT');
    req.flush({});
  });

  it('should call delete', () => {
    service.delete(1).subscribe();
    const req = httpMock.expectOne('https://localhost:7000/api/vehiculo/1');
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });
});
