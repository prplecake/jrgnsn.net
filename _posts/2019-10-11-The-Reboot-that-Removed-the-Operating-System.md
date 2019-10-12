---
layout: post
title: The Reboot that Removed the Operating System
---

One of the most gut-wrenching feelings I've recently experienced
occurred shortly after rebooting an internal virtual machine at work.
I've been slowly upgrading our systems to Windows Server 2016 or better.
Up until this incident, things were going fine.

---

***Note:** This happened about 7 months ago, and I'm just now
publishing. The ending is rough because I don't remember all the
details, and I'm also not a fan of removing things permanently.*

---

I was configuring virtual memory at the time which requires a reboot for
settings to take effect. Since it's a server and we hide the
Shutdown/Restart options via Policy, a quick `shutdown /t 0 /r` and I
was on my way...

Or so I thought. A few more minutes pass than usual when I notice my
`ping` shell was scrolling along: `Destination Host Unreachable`.
Usually, a reboot only produces a handful of `Request timed out`.
messages. Fortunately, since this is an internal VM, I have console
access. I hopped into vSphere and opened the console on the machine in
question.

<pre style="color:lightgray;background-color:black;">
	<code>An operating system wasn't found. Try disconnecting any drives that
don't contain an operating system.
Press Ctrl+Alt+Del to restart
</code></pre>

After staring at the blank, black screen for several minutes as I was
trying to figure out *what happened to the operating system I was just
using!*

A coworker had reconfigured the backup job a few hours prior, but that
shouldn't affect any running VMs. In addition to changing the job, he
also removed backups older than X days; still, this can't be the issue.

I phoned my coworker who hadn't seen this issue before. I ran a few
search queries online and started sifting through the results.

---

It turns out that using Windows' Disk Management to repartition a VMDK
is a *bad idea.* What ensued was two hours of my chasing this problem
down, spending at least 45 minutes of those two hours in Windows System
Recovery trying to reconfigure the Master Boot Record.

The root issue was that, since VMware didn't recognize the partitions
anymore, it was assigning drive letters to it as it pleased, somehow
never managing to give the actual C: disk the letter C. 
