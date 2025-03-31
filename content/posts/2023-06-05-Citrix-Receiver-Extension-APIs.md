---
date: "2023-06-05T00:00:00Z"
type: "post"
tags: ["Citrix","Citrix StoreFront","sysjob1"]
title: Citrix Receiver Extension APIs
---

This document lists CSS and Javascript extension points for Citrix Receiver.
Unless otherwise specified, these extensions apply to both web and native
clients. It used to be hosted on Citrix's website, but is currently 404'ing.
It's also available on the [Wayback
Machine][wbm-receiver-extension-apis].

[wbm-receiver-extension-apis]:https://web.archive.org/web/20220906174817/https://www.citrix.com/content/dam/citrix/en_us/citrix-developer/documents/receiver-apis.html

# CSS Classes for Customization

# Areas Commonly Customized

`.logo-container`
`.logon-logo-container`

: Company logo to replace the default logo in the main UI and authentication
screens. Should specify a background image and background size.

`.theme-header-color`
`.theme-highlight-color`

: Primary colors used for header and highlight text. Should specify a 'color'
attribute.

`.theme-header-bgcolor`

: Primary color (or fill) used for header region.

`.button / .button.default`

: Primary color (or fill) used for popup buttons, and the popup button selected
by default.

## Marker Classes

`.large` / `.small`
`.largeTiles` / `.smallTiles`
`.highdpi`
`.myapps-view` / `.store-view` / `.desktops-view` / `.appinfo-view` / `.search-view`

: Marker classes indicating current state of UI that can be used in CSS to
selectively change the display of custom content.

## Areas for Custom Content

These are areas within parts of the UI intended for custom content to be added
using script.

For example:

```javascript
$('#customTop').html('Hello World');
```

`#customTop`
`#customScrollTop`
`#customBottom`

: These are all on the home screen.

`#customInfoTop`
`#customInfo`

: These are on the app details (info) page.

`#customExplicitAuthHeader`
`#customExplicitAuthFooter`
`#customExplicitAuthTop`
`#customExplicitAuthBottom`

: These are all on the explicit authentication screen.

`#customAuthHeader`
`#customAuthFooter`
`#customAuthTop`
`#customAuthBottom`

: These are all on the general authentication screens (when shown).

`.customAuthHeader`
`.customAuthFooter`
`.customAuthTop`
`.customAuthBottom`

: These relate to all authentication screens.

# Script Objects

The various script APIs pass, or use a number of JavaScript objects. An
understanding of the intent of these objects, and how they may evolve over time,
is key if you wish any customization to work across releases. We considered
hiding all Receiver internals, and creating shadow objects purely for API use.
This would have given us a very stable API - but we felt this would be too
restrictive, as inevitably it would limit what an extension could do. Instead we
choose to reveal the "real" objects and caution users that the futher they stray
from the core APIs, they more likely it is that they will have work to do on
server upgrade. We recommend sticking to the key object properties to ensure
smooth upgrade.

## app

```
{  
    name: Name of app.  
    description: Description.  
    iconurl: Icon for app (once loaded)  
    subscriptionstatus: Current status, if subscription is supported  
    mandatory: True if app is required  
    store: Store object for this app  
}
```

## category

```
{  
    fname: Name of the leaf folder.  
    pname: Full path to folder.  
}
```

## bundle (featured app group)

```
{  
    title: Name of the bundle.  
    description: Description of the bundle.  
    tileid: Tile id.  
}
```

## native client properties

```
{  
    apiversion: currently always 1.1  
    platform: { id: Platform id, eg windows }  
    preferredLanguages[Array of locales]  
}
```

## jsondata

This is the low level data format from the server. See wire API specifications.

## message box params object

```
{  
    localize: If true, then any string matching a key in the localization dictionary will be localized.  
    messageText: Text to show  
    messageTitle: Title for message box  
    okButtonText: Optional button text (defaults to ok)  
    cancelButtonText: Optional button text for a second button (defaults to cancel)  
    okAction: Function to call if OK is clicked  
    cancelAction: Function to call if Cancel is clicked. (If omitted, only a single button is shown)  
}
```

### ajax options

```
{  
    data: Optional data to send (string)  
    headers: Optional header dictionary  
    type: Request type (GET or POST)  
    url: URL to call (relative to page)  
    dataType: Type of returned data text/XML/json (optional)  
    success: Function to call on success  
    error: Function to call on error  
}
```

# Script Extension Points

These are called at various times during execution and can either pause or
modify the behavior of Citrix Receiver. To use an extension point, define a
function on CTXS.Extensions.

Where a function defines a callback, the callback should be called to continue
normal operation. For example:

```javascript
CTXS.Extensions.preInitialize = function(callback) {
    alert("just starting");
    callback();  
};
```

## Notifications of progress

`preInitialize(callback)`
`postInitialize(callback)`
`postConfigurationLoaded()`
`postAppListLoaded()`

: Note that during these calls, the UI may be hidden by native Citrix Receiver,
so it is not safe to show UI.

  For APIs passing a callback, you MUST call the callback function, though you
  may delay calling it until code of your own has run.

`beforeLogon(callback)`

: Web browsers only. Called prior to displaying any logon dialogs. You may call
"showMessage" here, or add your own UI.

`beforeDisplayHomeScreen(callback)`

: All clients, called prior to displaying the main UI. This is the ideal place
to add custom startup UI.

  Note that for native clients, the user may not have logged in at this stage,
  as some clients allow offline access to the UI.

`afterDisplayHomeScreen()`

: All clients, called once the UI is loaded and displayed. The ideal place to
call APIs to adjust the initial UI, for example to start in a different tab.

# APIs used to monitor Receiver environment

`noteNativeClient(properties)`

: This passes back information about the native client environment. Not called
for web browser access.

`noteNativeStore(store)`

: This passes information about the store being accessed by the native client -
as the page URI does not necessarily give any clue.

# APIs used to modify the applications shown

`preProcessAppData(store,jsondata)`

: A low level, but extremely useful function to modify application data prior to
any processing.

`noteStartLoadApps(store)`
`noteApp(app)`

: Note an app object exists. Useful if you want to remember specific apps (for
example to launch them yourself). This is called during initialization, so you
should not immediately attempt to use the app object until initialization
completes.

`excludeApp(app)`

: Exclude an application completely from all UI, even if it would normally be
included.

`includeApp(app)`

: Include an application, even if it would normally be excluded. For example, a
platform might exclude applications intended for a different platform.

`includeInMyApps(app)`

: Force an app to be included in the "Favorites" view

`excludeFromMyApps(app)`

: Force an app to be excluded from the "Favorites" view

# APIs to arrange icons

`sortCategoryAppList(app_array, category, defaultSortFn)`
`sortBundleAppList(app_array, bundle, defaultSortFn)`
`sortMyAppList(app_array,defaultSortFn)`
`filterAllAppsDisplay(app_array,defaultSortFn)`
`filterDesktops(app_array,defaultSortFn)`

: These APIs all allow the apps shown in a particular view to be sorted or
filtered. You should modify the array of apps in place.

  Note that the "Favorites" view supports user rearrangement by default, so
  sorting the list here may be confusing unless you also disable the user
  rearrangement.

# APIs related to UI updates

`preRedraw()`
`postRedraw()`

: These are low-level catch-all functions intended to help tweak a UI that
misbehaves. Strictly redraw is under the whim of the browser, so this really
means before/after significant UI events, such as change of view. Generally,
there are more specific callbacks on UI change.

`beforeShowAppInfo(app)`

: Called prior to displaying the details page for an app. Useful to tweak the
buttons/information to be displayed.

`onFolderChange(folder path)`

: Called on the Store view if the folder is changed, passing the path. Also
called passing "" if a non-folder view is selected on the store view.

`onViewChange(view name)`

: Called if the view changes.

`onAppHTMLGeneration(element)`

: Called when HTML is generated for one or more app tiles, passing the parent
container. Intended for deep customization. (Warning: this sort of change is
likely to be version specific)

`useNativeMessageBox(message id title or text)`

: Called whenever the UI is considering showing a native dialog, rather than a
web dialog for a message. You can return false to force the web dialog to be
used. Note that some error messages originate in native code and cannot be
redirected to the web UI.

`showMessageBox(params,defaultMessageBoxFn)`

: Allow override of the (web) dialog used for all error and similar messages, to
enable a replacement function to be used.

`getAppTileMarkup(app, appDisplayName, defaultMarkupFn)`

: Overrides the HTML markup for app tiles. You can call the default function and
then modify the markup, or return custom markup. The return value must be a
string. (StoreFront 3.5 and later)

`getBundleMarkup(bundle, index, isFirst)`

: Provide custom HTML markup for featured app group tiles. The return value must
be a string. (StoreFront 3.5 and later)

`getFolderMarkup(folder, folderDisplayName, defaultMarkupFn)`

: Overrides the HTML markup for folder tiles. You can call the default function
and then modify the markup, or return custom markup. The return value must be a
string. (StoreFront 3.5 and later)

# APIs related to user initiated operations

Calling the supplied function will allow the operation to proceed. Not calling
it will cancel the action.

`doLaunch(app,launchFn)`<br/>
`postLaunch(app,status)`

`doSubscribe(app,subscribeFn)`<br/>
`postSubscribe(app)`

`doRemove(app, removeFn)`<br/>
`postRemove(app)`

`doInstall(app,installFn)`<br/>
`postInstall(app)`

# APIs that only apply to web browsers

`includeAuthenticationMethod(authenticationMethod)`

: Force inclusion of an authentication method, by returning true

`excludeAuthenticationMethod(authenticationMethod)`

: Force exclusion of an authentication method, by returning true

`showWebReconnectMenu(bool_showByDefault)`

: Return true/false to enable/disable the menu item

`showWebDisconnectMenu(bool_showByDefault)`

: Return true/false to enable/disable the menu item

`webReconnectAtStartup(bool_ReconnectByDefault)`

: Return true/false to enable/disable reconnection at startup. Note that
reconnection at startup is not supported on all platforms.

`webLogoffIcaAction(string_defaultAction)`

: Return the action to be taken with regard to ICA sessions during logoff.

  Return one of "disconnect", "terminate" or "none"

**Detailed hooking of logoff steps**

`beforeWebLogoffIca(string_defaultAction)`

: The first phase of logoff is to handle ICA sessions.

  Return one of "disconnect", "terminate" or "none"

`beforeWebLogoffWebSession()`

: The second phase of logoff is to exit the web session from the server.

  Return false to prevent web session termination.

`beforeWebLogoffGateway`

: The third and final phase of logoff is to terminate any gateway sesson.

  Return false to prevent gateway session termination

`afterWebLogoffComplete`

: Called once logoff has completed.

# Action APIs

These are functions that can be called to make something happen, for example to
launch an app or show a message box. These APIs are all defined on the
CTXS.ExtensionAPI object.

`showMessage(data)`

: Show a message box. Data contains messageText, messageTitle, okButtonText,
cancelButtonText, okButtonAction, cancelButtonAction

`resize()`

: Inform Receiver that custom elements have changed size, and that it may need
to update the UI layout.

`trace(message)`

: Send a trace message to the underlying trace system.

`refresh()`

: Refresh application lists from server. May have side effects, such as
reconnection and/or causing authentication prompts.

`changeView(view,view title)`

: Change the current view being displayed. For views added by extensions, also
supply a title to be displayed.

  Note that changing to the "appinfo" view will show the app info of the last
  app for which info was displayed - or return to the home screen if this is no
  longer valid. Changing to the "search" view is not supported.

`navigateToFolder(path)`

: Change the current folder being displayed. View should be "store".

`addToolbarButton(text,button_class,optional_htmlContent,optional_onClickFn)`

: Toolbar buttons are only shown on "apps" view by default.

`addViewButton(view class,button text,view title)`

: Add an additional view button alongside Favorites, Desktops and Apps.

`addInfoButton(text label, button class,onClickFn)`

: Add a button to the app info view.

`addHelpButton(onClickFn)`

: Add a help button to the UI header.

`localStorageGetItem(name,callback fn)`

: Get an item from web local storage or the native equivalent. The callback function will be called passing the value, and
this may return asynchronously.

`localStorageSetItem(name,value,optional callback fn)`

: Set an item in web local storage or the native equivalent. The name and value
should be strings. The callback is called once the value has been stored.

`openUrl(url)`

: Open a URL in a new web browser tab, or using the default web browser
application.

`proxyRequest(options,bool_loginFirst)`

: Proxy a request to a web service. 'options' is a subset of the jQuery ajax()
request options.

  Note that this is recommended _only_ for access to the server hosting the
  Receiver UI. Access to other servers may be limited by browser and/or native
  security policies.

`enableSubscriptionProperties()`

: Enable subscription (and extended app) properties when communicating with the
server.

`updateSubscriptionProperties(app,callback)`

: Update the subscription properties stored on the server for an app to match
the in memory object. The callback function is called on completion, passing a
status string.

`launch(app)`

: Launch an app or desktop.

`showSelectedApps(apps)`

: Given an array of app objects, displays the corresponding app icons in the UI.
(StoreFront 3.5 and later)
