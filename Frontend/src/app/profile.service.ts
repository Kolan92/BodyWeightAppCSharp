import { Injectable } from '@angular/core';

import config from './app.config';
import { OktaAuthService } from '@okta/okta-angular';
import { HttpClient } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { flatMap, map, tap } from 'rxjs/operators';
import { UserProfile } from './models/UserProfile';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(
    private oktaAuth: OktaAuthService,
    private http: HttpClient) { }

  public getUserProfile(): Observable<UserProfile> {
    return from(this.oktaAuth.getAccessToken())
      .pipe(
        flatMap(accessToken =>
            this.http.get<UserProfile>(`${config.resourceServer.baseApiUrl}/profile`, {
              headers: {'authorization': `Bearer ${accessToken}`}
            })));
  }

  public updateUserProfile(userProfile: UserProfile) {
    return from(this.oktaAuth.getAccessToken())
      .pipe(
        tap(x => console.log(`inside updateuser profile ${userProfile}`)),
        flatMap(accessToken =>
            this.http.put<UserProfile>(`${config.resourceServer.baseApiUrl}/profile`,
            userProfile, {
              headers: {'authorization': `Bearer ${accessToken}`}
            })));
  }


}
