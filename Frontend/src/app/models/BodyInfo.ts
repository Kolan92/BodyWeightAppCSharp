export interface BodyInfo {
  height: number;
  weightMeasurements: Array<BodyWeight>;
}

export interface BodyWeight {
  bmi: number;
  weight: number;
  measuredOn: Date;
}
