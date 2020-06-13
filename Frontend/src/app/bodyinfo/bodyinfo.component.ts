import { BodyInfo, BodyWeight } from "./../models/BodyInfo";
import { Component, OnInit } from "@angular/core";
import { OktaAuthService } from "@okta/okta-angular";
import { ChartDataSets, ChartOptions } from "chart.js";

import { BodyInfoService } from "../body-info.service";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import * as moment from "moment";
import { ToastrService } from "ngx-toastr";
import { CustomValidators } from "ngx-custom-validators";

@Component({
  selector: "app-bodyinfo",
  templateUrl: "./bodyinfo.component.html",
  styleUrls: ["./bodyinfo.component.css"],
})
export class BodyInfoComponent implements OnInit {
  public bodyInfo: BodyInfo;
  public chartOptions = {
    scaleShowVerticalLines: false,
    responsive: true,
  };
  public chartLabels = [];
  public chartType = "line";
  public chartLegend = true;
  public chartData: Array<ChartDataSets>;
  public options: ChartOptions = {
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
          },
        },
      ],
    },
  };

  public measurementForm: FormGroup;
  get f() {
    return this.measurementForm.controls;
  }
  public moment: any = moment;

  private toastOptions = { positionClass: 'toast-top-right' };

  constructor(
    public oktaAuth: OktaAuthService,
    private bodyInfoService: BodyInfoService,
    private formBuilder: FormBuilder,
    private toasterService: ToastrService
  ) {}

  ngOnInit() {
    this.measurementForm = this.formBuilder.group({
      weight: new FormControl(0, [Validators.min(1), Validators.max(1000)]),
      measuredOn: new FormControl(moment(new Date()).format("YYYY-MM-DD"), [
        CustomValidators.maxDate(new Date()),
      ]),
    });

    this.bodyInfoService.getBodyInfo().subscribe((bodyInfos) => {
      this.bodyInfo = bodyInfos;
      this.updateChartData();
      if (this.bodyInfo.weightMeasurements.length) {
        const lastMeasurement = this.bodyInfo.weightMeasurements[
          this.bodyInfo.weightMeasurements.length - 1
        ];
        this.measurementForm.get("weight").patchValue(lastMeasurement.weight);
      }
      const dateValidators = [
        CustomValidators.maxDate(new Date()),
        CustomValidators.minDate(this.bodyInfo.birthDate),
      ];
      this.measurementForm.get("measuredOn").setValidators(dateValidators);
    });
  }

  onSubmit(newMeasurementFormData) {
    const heightInMeters = this.bodyInfo.height / 100;
    const currentBmi =
      newMeasurementFormData.weight / (heightInMeters * heightInMeters);
    const newMeasurement = <BodyWeight>{
      bmi: currentBmi,
      weight: newMeasurementFormData.weight,
      measuredOn: newMeasurementFormData.measuredOn,
    };

    this.bodyInfoService.addMeasurement(newMeasurement).subscribe((model) => {
      this.toasterService.success("Added measurement", "", this.toastOptions);
      newMeasurement.id = model.id;
      this.bodyInfo.weightMeasurements.push(newMeasurement);
      this.updateChartData();
    });
  }

  deleteMeasurement(measurement: BodyWeight) {
    this.bodyInfoService.deleteMeasurement(measurement).subscribe((_) => {
      this.toasterService.success("Removed measurement", "", this.toastOptions);
      const index = this.bodyInfo.weightMeasurements.findIndex(
        (m) => m.id === measurement.id
      );
      this.bodyInfo.weightMeasurements.splice(index, 1);
      this.updateChartData();
    });
  }

  updateChartData() {
    this.chartLabels = this.bodyInfo.weightMeasurements.map((e) =>
      moment(e.measuredOn).format("YYYY-MM-DD")
    );
    this.chartData = [
      {
        label: "Weight",
        yAxisID: "weight",
        data: this.bodyInfo.weightMeasurements.map((e) => e.weight),
      },
      {
        label: "Body Weight Index",
        yAxisID: "bmi",
        data: this.bodyInfo.weightMeasurements.map((e) => e.bmi),
      },
    ];
  }
}
