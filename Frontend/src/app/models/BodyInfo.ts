export interface BodyInfo {
  height: number;
  weightMeasurements: Array<BodyWeight>;
}

export interface BodyWeight {
  weight: number;
  measuredOn: Date;
}
