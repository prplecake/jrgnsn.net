---
title: Optional Parameters Required to Close WebSockets in Safari Without Errors
layout: post
---

At work, I wrote some JavaScript to do a quick test to make sure a WebSocket
connection can be made from our users' computers. We're a Citrix shop, and most
of our users use the HTML5 client, which requires WebSockets to work.

If a WebSocket can't connect, or an error is thrown, a message is shown to
encourage the user to reach out to *their* IT department to resolve whatever
firewalls or security software might be in the way.

```javascript
// Create WebSocket connection
_ws = new WebSocket(`wss://${window.location.host}`);
console.debug("_ws.readyState: " + _ws.readyState);
// Set up callbacks
_ws.onopen = (e) => {
    console.debug(e);
    // Make sure connection is open
    if (_ws.readyState !== WebSocket.OPEN) {
        websocketErrorMessage();
    }
    _ws.close();
}
_ws.onerror = (e) => {
    console.debug(e);
    websocketErrorMessage();
}
_ws.onclose = (e) => {
    console.debug(e);
}
window.onbeforeunload = () => {
    _ws.close();
}
```

Essentially, we create a WebSocket, set up some event callbacks, and make sure
the socket gets closed.

[According to the WebKit JS
docs](https://developer.apple.com/documentation/webkitjs/websocket/1632860-close),
the `code` and `reason` are optional. However, if you omit them, the socket's
`onerror` event will always fire. You'd also see an error in the console:
"WebSocket connection to 'wss://example.com' failed: The network connection was
lost.

![Safari WebSocket Error](/content/2024-12-16/safari_websocket_error.jpeg)

Updating the `_ws.close();` lines to `_ws.close(1000, "test successful");` would
resolve the issue.

![Safari WebSocket Success](/content/2024-12-16/safari_websocket_success.jpeg)
