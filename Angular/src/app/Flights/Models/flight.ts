export class Flight {

    FlightId: number;
    FlightName: string;
    Source: string;
    Destination: string;
    DepartureTime: Date;
    ArrivalTime: Date;
    NoOfSeats: number;
    BaggageLimit: number;
    AvailableSeats: number;


    constructor(
        FlightId: number,
        FlightName: string,
        Source: string,
        Destination: string,
        DepartureTime: Date,
        ArrivalTime: Date,
        NoOfSeats: number,
        BaggageLimit: number,
        AvailableSeats: number) {
        this.FlightId = FlightId;
        this.FlightName = FlightName;
        this.Source = Source;
        this.Destination = Destination;
        this.DepartureTime = DepartureTime;
        this.ArrivalTime = ArrivalTime;
        this.BaggageLimit = BaggageLimit;
        this.AvailableSeats = AvailableSeats;
    }
}
