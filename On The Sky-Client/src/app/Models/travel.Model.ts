import Flight from "./flight.Model";
import User from "./user.Model";

export default class Travel {
  travelId!: number;
  amountTickets!: number;
  flightId!: number;
  flight!: Flight;
  // userId!: number;
  user!: User;
  status?: string;
}

