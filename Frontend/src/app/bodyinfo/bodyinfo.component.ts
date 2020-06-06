/*!
 * Copyright (c) 2018, Okta, Inc. and/or its affiliates. All rights reserved.
 * The Okta software accompanied by this notice is provided pursuant to the Apache License, Version 2.0 (the "License.")
 *
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *
 * See the License for the specific language governing permissions and limitations under the License.
 */

import { Component, OnInit } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { HttpClient } from '@angular/common/http';

import sampleConfig from '../app.config';
import { BodyInfoService } from '../body-info.service';


@Component({
  selector: 'app-bodyinfo',
  templateUrl: './bodyinfo.component.html',
  styleUrls: ['./bodyinfo.component.css']
})
export class BodyInfoComponent implements OnInit {
  failed: Boolean;
  public bodyInfo: any[] = [];

  constructor(
    public oktaAuth: OktaAuthService,
    private bodyInfoService: BodyInfoService) {
  }

  ngOnInit() {
    this.bodyInfoService.getBodyInfo()
      .subscribe(bodyInfos => {
        this.bodyInfo = bodyInfos;
      },
      err => console.error(err));
  }
}
