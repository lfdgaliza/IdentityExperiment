module.exports = {
    // Disable SSL verification for requests on CLI
    "request": {
      "https":  {
        "rejectUnauthorized": false
       }
    },
    // Disable SSL verification for requests on VSCode extension
    "httpyac.requestGotOptions": {
        "https": {
            "rejectUnauthorized": false
        }
    },
  }