---
title: Mitigating Pi-hole ISE 500 Error on the "Long term data - Query Log"
layout: post
tags:
  - Pi-hole
---

When selecting a large date range on the long term data Query Log, say
last 30 or even last 7 days, I'd be greeted with the following error
message and some more information in `Lighttpd`'s logs.

!['An unknown error occurred while loading the data.' alert dialog](/content/2020-06-07/an_unknown_error_occurred_while_loading_the_data.png)

And the relevant line in `/var/log/lighttpd/error.log`:

> 2020-06-07 19:40:24: (mod_fastcgi.c.421) FastCGI-stderr: PHP Fatal
> error:  Allowed memory size of 134217728 bytes exhausted (tried to
> allocate 10489856 bytes) in /var/www/html/admin/api_db.php on line 422

I'm running Pi-hole on an old desktop system with greater resources than
the Raspberry Pis I own. That box has 4GB RAM.

Once I increased the memory limit in the relevant `php.ini`, the
Internal Server Error 500 went away.

* I noticed when I increased the `memory_limit` to `512M`, I had better
luck loading the last 7 days worth of data.
* I was successfully able to see the last 30 days worth of data by
giving PHP a whopping limit of `2G`!
* Data from "all time" is still unavailable to me, I don't have the RAM
available to let PHP use all of it.

According to [this post][0] on the Pi-hole discourse, the next major
version of Pi-hole will have a new, more clever, API.

[0]:https://discourse.pi-hole.net/t/long-term-data-query-log-for-last-7-days-isnt-working/26073/4
