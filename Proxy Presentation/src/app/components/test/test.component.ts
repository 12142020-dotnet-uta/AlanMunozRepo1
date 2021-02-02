import { Component, OnInit } from '@angular/core';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  
  DisplayJson = '';

          //Dependency Injection in our TestComponent Class for calling the service.
  constructor(private _testService:TestService){

  }

  ngOnInit(): void {
  }

  //Display the Local API endpoint with the default WeatherForecast
  displayRequest () {
    this._testService.getWeatherTest()
      //Execute this callback only if there was no problem in the request.
      .subscribe( dataOnSuccess=> {
        this.DisplayJson = dataOnSuccess;
      },

      // Execute the error callback when there was an error on the request 
      dataOnError => {
        alert("There was an error, review the console.");
        console.log(dataOnError);
      } );
  }

  //Display the remote API using the /api/ service method, using the proxy-multi.conf.js file
  displayExchangeRequest () {
    this._testService.getExchangeTest()
      //Execute this callback only if there was no problem in the request.
      .subscribe( dataOnSuccess=> {
        this.DisplayJson = dataOnSuccess;
        console.log('Test Endpoint 1')
      },

      // Execute the error callback when there was an error on the request 
      dataOnError => {
        alert("There was an error, review the console.");
        console.log(dataOnError);
      } );
  }

  //Display the remote API using the /api/ service method, using the proxy-multi.conf.js file
  displayClockRequest () {
    this._testService.getClockTest()
      //Execute this callback only if there was no problem in the request.
      .subscribe( dataOnSuccess=> {
        this.DisplayJson = dataOnSuccess;
        console.log('Test Endpoint 2')
      },

      // Execute the error callback when there was an error on the request 
      dataOnError => {
        alert("There was an error, review the console.");
        console.log(dataOnError);
      } );
  }
  
  //Display the remote API using the /EMEX_RestApi/api/TipoCambio service method, using the proxy-multi.conf.json file
  displayClockDiferentEndPointRequest () {
    this._testService.getExchangeDiferentEndPointTest()
    //Execute this callback only if there was no problem in the request.
      .subscribe( dataOnSuccess=> {
        this.DisplayJson = dataOnSuccess;
        console.log('Test Endpoint 2')
      },

      // Execute the error callback when there was an error on the request
      dataOnError => {
        alert("There was an error, review the console.");
        console.log(dataOnError);
      } );
  }

  //Display the remote API using the /EMEX_RestApi/api/ClockServ service method, using the proxy-multi.conf.json file
  displayExchangeDiferentEndPointRequest () {
    this._testService.getClockDiferentEndPointTest()
    //Execute this callback only if there was no problem in the request.
      .subscribe( dataOnSuccess=> {
        this.DisplayJson = dataOnSuccess;
        console.log('Test Endpoint 1')
      },

      // Execute the error callback when there was an error on the request 
      dataOnError => {
        alert("There was an error, review the console.");
        console.log(dataOnError);
      } );
  }



}
