+++
date = '2025-03-30T19:53:05Z'
draft = true
title = 'Windows Update Error 0x80070780'
tags = [ "Microsoft Windows", "Windows Update", "sysjob1" ]
+++

These last two months a couple of the servers I maintain at work were failing to
install the monthly update rollup. Usually when updates start failing, we tend
to reach for a server rebuild instead of troubleshooting the black box that can
be Windows Updates - after all the error messages and codes they display to end
users aren't exactly... descriptive. This particular error code doesn't make
[Microsoft's list of common errors and
mitigation](https://learn.microsoft.com/en-us/troubleshoot/windows-client/installing-updates-features-roles/common-windows-update-errors).

Our first occurance of this error started with our February maintenance,
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


[^1]:`C:\Windows\Logs\CBS\CBS.log`, or one of the recent logs from a previous
    update run.
