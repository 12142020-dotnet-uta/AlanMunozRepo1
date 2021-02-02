const PROXY_CONFIG = [
    {
        //EndPoints available to access througth the proxy server, when it finds any request string with the following context, it will map it to the target url.
        context: [
                "/EMEX_RestApi/api/TipoCambio",
                "/EMEX_RestApi/api/ClockServ",
        ],
        //URL for the server. 
        target: "http://192.168.1.2",
        //Configuration in false while developing applications, later on will be using other tools for deployment security.
        secure: false,
        //Log in the command line while it is debugging, resulting in generating a register when it call the url.
        logLevel: "debug"
    }
]
//Make the exports available to the module.
module.exports = PROXY_CONFIG;