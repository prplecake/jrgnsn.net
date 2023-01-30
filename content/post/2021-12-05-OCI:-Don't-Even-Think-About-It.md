---
date: "2021-12-05T00:00:00Z"
title: 'OCI: Don''t Even Think About It'
---

Back when Oracle announced their cloud offering, Oracle Cloud
Infrastructure (OCI), I was originally intrigued. I figure I'd give it a
try, after all I'm not a big fan of AWS and I'm even less a fan of
Azure. At the beginning I decided to forget about Oracle's reputation,
and, damn was that a mistake.

I stuck to the free tier, using only two low-powered instances to run a
test application as well as a wiki. Everything was going great until
this morning. My last time browsing my wiki was last night at about
21:00, and it worked great! This morning I went to look something up and
I was greeting with a nice Connection Timeout error. "Great, the gollum
service probably died again," I thought. It should be an easy fix! I'd
just SSH in and do a quick service restart.

Then my SSH connections were timing out. Uh-oh, this isn't good.

I should clarify I hadn't touched this server in months. Nothing *should
have* changed. Fortunately, OCI provides you with a cloud shell to
access your instances. I was able to log in to poke around, read some
logs, check on services, in an attempt to see what might be going wrong.
I couldn't find anything out of the ordinary. My virtual network rules
were all still in place, and I didn't run ufw or iptables on the server
at all. My other instance was working just fine.

After about an hour of troubleshooting and coming up empty, I decided
I'll just cut my losses and close my OCI account. I have resources with
other providers anyway. Any important workloads would be migrated to
those servers and I'd be done with OCI.

Oh, how naive I was. After searching for abut another 45 minutes for the
"Close Account" button, I did some quick searching and came across [this
reddit post][reddit-post]:

![It took me the better part of two weeks to finally get the account
closed. You have to open a ticket with Oracle Support - and then wait
for them to escalate to the team that can actually close the account.
THEN, you need to wait another 30+ days before the account is
permanently deleted. Start with opening a support ticket in the Oracle console.
](/content/2021-12-05/reddit-post.png)

[reddit-post]:https://old.reddit.com/r/oraclecloud/comments/nu7gr6/how_to_delete_my_oracle_cloud_account/h0wo4xt/

Alright, no problem. I found the Support portal but there was a
noticable lack of an "Open Support Ticket" button:

![Screen shot of OCI's support
center](/content/2021-12-05/oracle-support-center.png)

Weird. Let's do some more research, maybe I'm missing something. A few
searches later and I found a document titled ["Getting Help and
Contacting Support"][oracle-kb], which as you read you'll
find that [**only paid accounts can open support
tickets**][oracle-kb-4].

![Oracle Support Center says only paid accounts can open support
tickets](/content/2021-12-05/oracle-kb.png)

[oracle-kb]:https://docs.oracle.com/en-us/iaas/Content/GSG/Tasks/contactingsupport.htm
[oracle-kb-4]:https://docs.oracle.com/en-us/iaas/Content/GSG/Tasks/contactingsupport.htm#Contacti

Are you fucking kidding me? That's absolutely laughable. They allow you
to test out their services for free with their free tier, but you can't
test out their support! Who in their right mind would pay for such a
(dis)service?[^1]

I wouldn't, and couldn't in good conscious, recommend OCI to anyone for
anything at all. Don't even think about it.

---

I'd like to point out now how incredibly unnavigable I find the OCI
interface, it might be my opinion, but it makes a big difference when
you can't find anything you're looking for.

Sure, I could just delete all my resources and forget about the account,
but I'd rather close it for good. So here begins my apparently
multi-week long process of closing the account.

Fuck you, Oracle.

[^1]: AWS has a basic support plan!
