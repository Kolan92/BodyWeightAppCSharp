export interface BodyInfo {
  height: number;
  weightMeasurements: Array<BodyWeight>;
}

export interface BodyWeight {
  id: number;
  bmi: number;
  weight: number;
  measuredOn: Date;
}
