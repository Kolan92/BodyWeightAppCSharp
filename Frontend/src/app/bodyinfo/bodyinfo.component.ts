import { BodyInfo } from './../models/BodyInfo';
import { Component, OnInit } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { ChartDataSets, ChartOptions } from 'chart.js';

import { BodyInfoService } from '../body-info.service';
import { FormBuilder, FormControl } from '@angular/forms';
import * as moment from 'moment';

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
  public chartLegend = true;
  public chartData: Array<ChartDataSets>;
  public measurmentForm;

  public options: ChartOptions =  {

      responsive: true,
      legend: {
        display: true,
      },
      scales: {
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
              color: '#42A5F5',
            },
            ticks: {
              fontColor: '#0D47A1',
            }
          }
        ]
      }
    };

  constructor(
    public oktaAuth: OktaAuthService,
    private bodyInfoService: BodyInfoService,
    private formBuilder: FormBuilder
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
            label: 'Weight',
            yAxisID: 'weight',
            data : this.bodyInfo.weightMeasurements.map(e => e.weight)},
          {
            label: 'Body Weight Index',
            yAxisID: 'bmi',
            data : this.bodyInfo.weightMeasurements.map(e => e.bmi)
          }
        ];

        const lastMeasurment = this.bodyInfo.weightMeasurements[this.bodyInfo.weightMeasurements.length - 1];
        this.measurmentForm.get('weight').patchValue(lastMeasurment.weight);

      },
      err => console.error(err));

      this.measurmentForm = this.formBuilder.group({
        weight: new FormControl(180, []),
        measuredOn: new FormControl(moment(new Date()).format('YYYY-MM-DD'), []),
      });
  }

  onSubmit(newMesurment) {
    this.chartLabels.push(newMesurment.measuredOn);
    this.chartData[0].data.push(newMesurment.weight);
    const heightInMeters = this.bodyInfo.height / 100;
    const currnetBmi = newMesurment.weight / (heightInMeters * heightInMeters);
    this.chartData[1].data.push(currnetBmi);
    console.warn('Your measuerment has been saved', newMesurment);
  }
}
