# IdentityExperiment
## Overview
This solution has:
1. An Identity Server (Duende)
1. A Weather Forecasting API
1. An MVC Client (App)

The Api is designed with onion architechture in mind: `( ( ( domain ) application ) api )`

So the client can access the api, it needs to ask Duende for a Token (using auth. code).  
After that it can reach the protected resource by informing the token in the Authorization header (Bearer)

## Unit Tests

Both the Application layer and Domain layer has UnitTests.  
They can be found under tests/UnitTests in the file explorer.  
Those are the only test projects I created to demonstrate the testability of the api.

## Integration Tests
* Integration tests were written for httpyac.
* I could also use a C# project with `WebApplicationFactory`, but I wanted to use a black-box approach (and write less :D).
* The Integration Tests are written only for the protected API to test its protection.
* The tests are far away from being exhaustive. This is only to demonstrate a possible approach.

### Install httpyac

`npm install -g httpyac` or `yarn global add httpyac`

Other options on [httpyac's website](https://httpyac.github.io/guide/)

### Running the tests

1. Run the IdentityServer and the API
3. Go to `tests/IntegrationTests` using `cd` on terminal and run `httpyac ./**/*.http --all -e dev`

If you want to run the tests on VSCode instead of CLI, install the httpyac extension:

`code --install-extension anweber.vscode-httpyac`

## Using the client
1. Run the Identity Server, the API and the Client at the same time.
1. Login with usr: bob pwd: bob and click on `Weather Forecast` in the top menu.
1. It is configured to refresh token when needed. You can see the `.Token.refresh_token` in the Home Page.