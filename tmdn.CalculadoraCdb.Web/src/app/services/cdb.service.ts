import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultadoCalculoCdb } from '../models/resultado-calculo-cdb.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CdbService {
  constructor(private httpClient: HttpClient) {}

  public Calcular(
    valorInicial: number,
    qtdMeses: number
  ): Observable<ResultadoCalculoCdb> {
    const params = new HttpParams()
      .set('valorInicial', valorInicial)
      .set('qtdMeses', qtdMeses);

    return this.httpClient.get(`${environment.API_BASE_URI}/api/Cdb`, {
      params,
    }) as Observable<ResultadoCalculoCdb>;
  }
}
