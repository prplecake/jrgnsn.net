---
date: "2019-10-25T00:00:00Z"
tags:
- Windows Group Policy
- WMI Queries
type: "post"
title: Remove and Prevent Access to Shut Down Commands Except on my VMs
---

I would do anything in my power to prevent the accidental shutdown of a
critical server. Unfortunately, Windows makes it *really easy* to
accidentally shut down a computer.

![Windows Start Menu - With Shut Down Commands Available]({{< slug >}}/with_shutdown_commands.png)

One slip of the wrist would cause you to "Shut down" a machine instead
of "Sign out" of it. I'm not perfect, I've made that mistake before.

Originally we created a Group Policy Object to control this behavior.
It's pretty easy to set up.

I created a GPO and named it **Disable Shut Down Option on Start Menu -
Servers**, because I only want to control this behavior on servers. I
still want my users to be able to shut down their workstations!

Edit that GPO and navigate to `User Configuration -> Policies ->
Administrative Templates -> Start Menu and Taskbar` and **Enable**
the item called *Remove and prevent access to the Shut Down, Restart,
Sleep, and Hibernate commands*.

Since this policy configures items in *User Configuration*, this GPO
**must** be linked in an OU that contains users.[^1]

Now this GPO doesn't distinguish between client and server OS versions
yet. To do that, we need to create a WMI Filter. Originally I called
this WMI Filer *Non-client OSes*. I renamed it after I added the query
to check if the machine in question is one of my VMs. Since my VMs have
a standard naming convention, my **two** WMI queries looked like this:

```sql
SELECT Name FROM Win32_OperatingSystem WHERE ProductType > 1
SELECT Name FROM Win32_ComputerSystem WHERE NOT Name LIKE 'MLJ-VM%'
```

Based on my research, it's my understanding that GPO treats multiple
queries like this with an `AND` operator. So if the machine is in fact a
server, but the name starts with "`MLJ-VM`": well, `True AND False` is
False, so the GPO would not apply. The result on my server-OS VMs is
exactly what I want[^2]:

![Windows Start Menu - Without Shut Down Commands Available]({{< slug >}}/without_shutdown_commands.png)

Of course, you could remove the second query to apply this policy to all
non-Client operating systems.

[^1]: This *can be* the apex of your domain.
[^2]: No I don't use Internet Explorer for real, are you high?
