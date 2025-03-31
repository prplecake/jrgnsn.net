+++
date = '2025-03-30T19:53:05Z'
title = 'Windows Update Error 0x80070780'
tags = [ "Microsoft Windows", "Windows Update", "sysjob1" ]
+++

These last two months a couple of the servers I maintain at work were failing to
install the monthly update rollup. Usually when updates start failing, we tend
to reach for a server rebuild instead of troubleshooting the black box that can
be Windows Updates -- after all the error messages and codes they display to end
users aren't exactly... descriptive. This particular error code doesn't make
[Microsoft's list of common errors and
mitigation](https://learn.microsoft.com/en-us/troubleshoot/windows-client/installing-updates-features-roles/common-windows-update-errors).

Our first occurrence of this error started with our February maintenance,
performed in early March. Time got away from me and I didn't find the time to
troubleshoot or rebuild the affected servers in March... until March
maintenance.

The Windows Update troubleshooter found nothing wrong.

Running the following took about an hour, and found nothing amiss (or, if it
did, it was unrelated to the update error).

```powershell
DISM.exe /Online /Cleanup-image /Scanhealth
DISM.exe /Online /Cleanup-image /Restorehealth
DISM.exe /Online /Cleanup-image /startcomponentcleanup
sfc /scannow
```

A quick search suggests `0x80070780` indicates a permissions error. Sounds
simple enough, I might be able to handle this myself. A quick look at
`CBS.log`[^1] showed the error has something to do with a `desktop.ini` in the
"Administrative Tools" folder of the Start Menu.

The actual text in the log was a bit more cryptic (formatted for readability):

```
2025-03-30 04:37:07, Error
CSI    00001676 (F) c0000279 [Error,Facility=(system),Code=633 (0x0279)] #3914314#
from Windows::Rtl::SystemImplementation::DirectFileSystemProvider::SysCreateFile(
    flags = (AllowFileNotFound), 
    handle = {provider=NULL, handle=0, name= ("null")}, 
    da = (FILE_GENERIC_READ|FILE_EXECUTE|0x00000040), 
    oa = @0x75b79fbb28->OBJECT_ATTRIBUTES {
        s:48; rd:NULL; 
        on:[89]'\??\C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\desktop.ini';
        a:0; sd:@0x1dd81dc34e0
    },
    iosb = @0x75b79fbb80, as = (null), fa = 0, s[gle=0xd0000279]
```
I'm not entirely sure what most of that means, but I am able to investigate
permissions issues, so I'll start with that. I checked the permissions of
`C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative
Tools\desktop.ini`[^2] and compared them to the same file from a working server.
There were some discrepancies.

Without going into too much detail here, on the working server the following
users or groups had some sort of permissions on `desktop.ini`:

* Everyone
* ALL APPLICATION PACKAGES
* ALL RESTRICTED APPLICATION PACKAGES
* SYSTEM
* Administrators
* Users

On the trouble servers, ALL APPLICATION PACKAGES and ALL RESTRICTED APPLICATION
PACKAGES were missing. I tried to add them, but that didn't result in a
successful update. My next trick was to rename `desktop.ini` to
`desktop.ini.old` and retry the update. Success!

I'll be curious to see if that resolves some of the issues I've been having with
Citrix Profile Management.

[^1]:`C:\Windows\Logs\CBS\CBS.log`, or one of the recent logs from a previous
    update run.
[^2]: Possibly closer to `C:\ProgramData\Microsoft\Windows\Start
    Menu\Programs\Windows Tools\` on Windows 11. I've also seen
    `C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Windows Administrative Tools\`.
