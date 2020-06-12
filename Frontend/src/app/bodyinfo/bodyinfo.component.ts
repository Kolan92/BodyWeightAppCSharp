import { BodyInfo } from './../models/BodyInfo';
import { Component, OnInit } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { ChartDataSets, ChartOptions } from 'chart.js';

import { BodyInfoService } from '../body-info.service';
import { FormBuilder, FormControl } from '@angular/forms';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';

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
  public measurementForm;

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

    private toastOptions = { positionClass: 'toast-top-right' };

  constructor(
    public oktaAuth: OktaAuthService,
    private bodyInfoService: BodyInfoService,
    private formBuilder: FormBuilder,
    private toasterService: ToastrService
    ) {
  }

  ngOnInit() {
    this.bodyInfoService.getBodyInfo()
      .subscribe(bodyInfos => {
        this.bodyInfo = bodyInfos;
        console.log(bodyInfos);
        this.chartLabels = this.bodyInfo.weightMeasurements.map(e => moment(e.measuredOn).format('YYYY-MM-DD'));
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

        if (this.bodyInfo.weightMeasurements.length) {
          const lastMeasurement = this.bodyInfo.weightMeasurements[this.bodyInfo.weightMeasurements.length - 1];
          this.measurementForm.get('weight').patchValue(lastMeasurement.weight);
        }
      },
      err => console.error(err));

      this.measurementForm = this.formBuilder.group({
        weight: new FormControl(70, []),
        measuredOn: new FormControl(moment(new Date()).format('YYYY-MM-DD'), []),
      });
  }

  onSubmit(newMeasurement) {
      this.chartLabels.push(newMeasurement.measuredOn);
      this.chartData[0].data.push(newMeasurement.weight);
      const heightInMeters = this.bodyInfo.height / 100;
      const currentBmi = newMeasurement.weight / (heightInMeters * heightInMeters);
      this.chartData[1].data.push(currentBmi);
      this.bodyInfoService.addMeasurement(newMeasurement)
        .subscribe(
          _ => this.toasterService.success('Successfully added new measurement', '', this.toastOptions)
        );
  }
}
