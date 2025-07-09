import Place from "./place.Model";

export default class Flight {
  flightid!: number;
  flighttime!: Date;
  countryID!: number;
  price!: number;
  maximum!: number;
  amount!: number;
  country!: Place;
}
