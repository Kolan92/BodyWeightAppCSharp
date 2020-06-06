import { BodyInfo } from './../models/BodyInfo';
import { Component, OnInit } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { ChartDataSets, ChartOptions } from 'chart.js';

import { BodyInfoService } from '../body-info.service';


@Component({
  selector: 'app-bodyinfo',
  templateUrl: './bodyinfo.component.html',
  styleUrls: ['./bodyinfo.component.css']
})
export class BodyInfoComponent implements OnInit {
  failed: Boolean;
  public bodyInfo: BodyInfo;

  public chartOptions = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  public chartLabels = [];
  public chartType = 'line';
  public chartLegend = false;
  public chartData:Array<ChartDataSets>;

  public options: ChartOptions =  {
      responsive: true,
      scales: {
        // We use this empty structure as a placeholder for dynamic theming.
        xAxes: [{}],
        yAxes: [
          {
            id: 'weight',
            position: 'left',
          },
          {
            id: 'bmi',
            position: 'right',
            gridLines: {
              color: 'rgba(255,0,0,0.3)',
            },
            ticks: {
              fontColor: 'red',
            }
          }
        ]
      }
    };

  constructor(
    public oktaAuth: OktaAuthService,
    private bodyInfoService: BodyInfoService,
    ) {
  }

  ngOnInit() {
    this.bodyInfoService.getBodyInfo()
      .subscribe(bodyInfos => {
        this.bodyInfo = bodyInfos;
        console.log(bodyInfos);
        this.chartLabels = this.bodyInfo.weightMeasurements.map(e => new Date(e.measuredOn).toDateString());
        this.chartData = [
          {
            yAxisID: 'weight',
            data : this.bodyInfo.weightMeasurements.map(e => e.weight)},
          {
            yAxisID: 'bmi',
            data : this.bodyInfo.weightMeasurements.map(e => e.bmi)
          }
        ];
      },
      err => console.error(err));
  }
}
