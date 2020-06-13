export interface BodyInfo {
  height: number;
  birthDate: Date;
  weightMeasurements: Array<BodyWeight>;
}

export interface BodyWeight {
  id: number;
  bmi: number;
  weight: number;
  measuredOn: Date;
}
