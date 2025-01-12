# IdentityExperiment

## Integration Tests
* Integration tests were written for httpyac.
* I could also use a C# project with `WebApplicationFactory`, but I wanted to use a black-box approach (and write less :D).
* The Integration Tests are written only for the protected API.

### Install httpyac

`npm install -g httpyac` or `yarn global add httpyac`

Other options on [httpyac's website](https://httpyac.github.io/guide/)

### Running the tests

1. Go to the project folder
2. Run the IdentityServer and the API
3. Run `httpyac ./**/*.http --all -e dev`

If you want to run the tests on VSCode instead of CLI, install the httpyac extension:

`code --install-extension anweber.vscode-httpyac`

