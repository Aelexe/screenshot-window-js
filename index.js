const path = require("path");

const edge = require("edge-js");

// Create a reference to the C# function.
const screenshot = edge.func(path.join(__dirname, "screenshot.cs"));

/**
 * Returns a promise that will resolve with a byte array containing the screenshot of the requested window.
 *
 * @param {string} windowName The name of the window to screenshot.
 * @return {Promise<byte[]>} A promise for the screenshot byte array.
 */
function screenshotWindow(windowName) {
	return new Promise((resolve, reject) => {
		screenshot(windowName, (error, response) => {
			resolve(response);
		});
	});
}

module.exports = screenshotWindow;
