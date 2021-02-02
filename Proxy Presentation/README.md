# ProxyTest

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 11.0.7.
In this project, we are using the proxy configurations for access to our local endpoints while developing our application.

We have 3 configuration files, the `src/proxy.conf.json`, `src/proxy.conf.js`, and `src/proxy-multi.conf.json`, in the json files.

When you want to load a specific Proxy configuration to Angular, there are 2 ways:
    
    1.- Open the `./angular.json` file, in the "serve" configuration, we add the proxy config to the serve Options: 
```
"serve": {
          "builder": "@angular-devkit/build-angular:dev-server",
          "options": {
            "browserTarget": "ProxyTest:build",
            "proxyConfig": "src/proxyFileWeWantToImplement"
          },
          "configurations": {
            "production": {
              "browserTarget": "ProxyTest:build:production"
            }
          }
        },
```
    2.- Update the package.json files, in the scripts, we update the start to the following: 
```
"start": "ng serve --proxy-config src/proxyFileWeWantToImplement",
```

Now you can do either `ng serve`, or `npm start` and it will enable the proxy server with the configurations provided. For more information, review WebKit development server for more details.


## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
