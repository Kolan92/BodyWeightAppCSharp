import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { HttpClient } from '@angular/common/http';
import { flatMap } from 'rxjs/operators';
import { Observable, from} from 'rxjs';

import config from './app.config';

@Injectable({
  providedIn: 'root'
})
export class BodyInfoService {

  constructor(public oktaAuth: OktaAuthService,
    private http: HttpClient) { }

  public getBodyInfo(): Observable<Array<any>> {
    return from(this.oktaAuth.getAccessToken())
      .pipe(
        flatMap(accessToken =>
            this.http.get<Array<any>>(`${config.resourceServer.baseApiUrl}/bodyinfo`, {
              headers: {'authorization': `Bearer ${accessToken}`}
            })));
  }
}
