import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { HttpClient } from '@angular/common/http';
import { flatMap } from 'rxjs/operators';
import { Observable, from} from 'rxjs';

import config from './app.config';
import { BodyInfo, BodyWeight } from './models/BodyInfo';

@Injectable({
  providedIn: 'root'
})
export class BodyInfoService {

  constructor(public oktaAuth: OktaAuthService,
    private http: HttpClient) { }

  public getBodyInfo(): Observable<BodyInfo> {
    return from(this.oktaAuth.getAccessToken())
      .pipe(
        flatMap(accessToken =>
            this.http.get<BodyInfo>(`${config.resourceServer.baseApiUrl}/bodyinfo`, {
              headers: {'authorization': `Bearer ${accessToken}`}
            })));
  }

  public addMeasurement(measurement: BodyWeight) {

    console.warn('adding new mes')
    return from(this.oktaAuth.getAccessToken())
      .pipe(
        flatMap(accessToken =>
            this.http.post<BodyWeight>(`${config.resourceServer.baseApiUrl}/bodyinfo`,
            measurement, {
              headers: {'authorization': `Bearer ${accessToken}`}
            })));
  }
}
