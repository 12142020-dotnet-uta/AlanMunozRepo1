import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  constructor( private _http: HttpClient ) { }

  //Angular Project endpoint: localhost:4200

  //Url for our local application with diferent endpoint
  // GET https://localhost:44328/WeatherForecast
  getWeatherTest():Observable<any> {
    return this._http.get<any>("/WeatherForecast")
  }
  
  //Url for the remote application with the /EMEX_RestApi/api/TipoCambio endpoint
  //GET http://192.168.1.2/EMEX_RestApi/api/TipoCambio
  getExchangeDiferentEndPointTest() :Observable<any> {
    return this._http.get<any>("/EMEX_RestApi/api/TipoCambio");
  }

  //Url for the remote application with the /EMEX_RestApi/api/TipoCambio endpoint
  //GET http://192.168.1.2/EMEX_RestApi/api/TipoCambio
  getClockDiferentEndPointTest() :Observable<any> {
    return this._http.get<any>("/EMEX_RestApi/api/ClockServ");
  }
  
  //Url for the remote application with the /api/ endpoint
  //GET http://192.168.1.2/EMEX_RestApi/api
  getExchangeTest() :Observable<any> {
    return this._http.get<any>("/api/TipoCambio");
  }

  //Url for the remote application with the /api/ endpoint
  //GET http://192.168.1.2/EMEX_RestApi/api
  getClockTest() :Observable<any> {
    return this._http.get<any>("/api/ClockServ");
  }

}
