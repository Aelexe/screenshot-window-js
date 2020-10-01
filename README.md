# screenshot-window-js

Node.js package using .NET and Edge.js to take screenshot of a given window.

# Installation

```
npm install screenshot-window-js
```

# Example

Take a screenshot of a specific window and save it to a file:

```javascript
const fs = require("fs");
const screenshotWindow = require("screenshot-window-js");

screenshotWindow("Example Window").then((image) => {
    fs.writeFileSync("example-window.png", image);
});
```

Take a screenshot of the foreground window and save it to a file:

```javascript
const fs = require("fs");
const screenshotWindow = require("screenshot-window-js");

screenshotWindow("").then((image) => {
    fs.writeFileSync("foreground.png", image);
});
```

# TODO

- Separate the C# code into a separate project and add more options to how windows are searched for, and compile into a dll to improve use with Edge.js.
