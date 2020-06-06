import { Injectable } from '@angular/core';

import config from './app.config';
import { OktaAuthService } from '@okta/okta-angular';
import { HttpClient } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { flatMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(public oktaAuth: OktaAuthService, private http: HttpClient) { }

  public getProfile(): Observable<any> {
    return from(this.oktaAuth.getAccessToken())
      .pipe(
        flatMap(accessToken =>
            this.http.get<Array<any>>(`${config.resourceServer.baseApiUrl}/profile`, {
              headers: {'authorization': `Bearer ${accessToken}`}
            })));
  }
}
