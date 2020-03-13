var path = require("path");
 
module.exports = {
    mode: "development",
    entry: path.join(__dirname, "./src/UI/App.fsproj"),
    output: {
        path: path.join(__dirname, "./public"),
        filename: "bundle.js",
    },
    devServer: {
        contentBase: "public",
        port: 8080,
    },
    module: {
        rules: [{
            test: /\.fs(x|proj)?$/,
            use: "fable-loader"
        }]
    }
}