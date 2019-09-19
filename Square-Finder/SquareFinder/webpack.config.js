"use strict";

module.exports = {
    entry: "~/Scripts/jsx/App.jsx",
    output: {
        filename: "./dist/bundle.js"
    },
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                loader: "babel-loader"
            }
        ]
    },
    externals: {
        // Don't bundle the 'react' npm package with the component.
        'react': 'React'
    },
    resolve: {
        // Include empty string '' to resolve files by their explicit extension
        // (e.g. require('./somefile.ext')).
        // Include '.js', '.jsx' to resolve files by these implicit extensions
        // (e.g. require('underscore')).
        extensions: ['', '.js', '.jsx']
    }
    
};