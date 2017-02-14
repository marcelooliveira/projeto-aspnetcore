var path = require('path');

module.exports = {
    entry: 
    {
        javascript: [
            './wwwroot/lib/jquery/dist/jquery.min.js',
            './wwwroot/lib/react/react.min.js',
            './wwwroot/lib/react/react-dom.min.js',
            './wwwroot/lib/react/react-with-addons.min.js',
            './wwwroot/js/react-bootstrap.js',
            './wwwroot/js/Components.jsx',
            './wwwroot/js/Cart.jsx',
            './wwwroot/js/CheckoutSuccess.jsx',
            './wwwroot/js/showdown.js'
        ]
    },
    output: {
        path: path.join(__dirname, 'wwwroot/build'),
        filename: 'bundle.js'
    },
    devtool: 'source-map',
    module: {
        loaders: [
            {
                test: /.jsx?$/,
                loader: 'babel-loader',
                exclude: /node_modules/,
                query: {
                    presets: ['es2015', 'react']
                }
            }
        ]
    }
};