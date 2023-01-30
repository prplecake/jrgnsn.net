---
date: "2019-05-07T00:00:00Z"
tags:
- Stories
title: There Once Was a Sysadmin...
---

That sysadmin really enjoyed working on silly programs in his free time,
he even runs a few websites! Oftentimes, this sysadmin is fairly
surprised he has a real job as a Systems Engineer/Web Developer.

This is one of those times.

---

I recently decided to switch my domains' name servers from CloudFlare to
[FastMail][fastmail-ref]. There were a few reasons for this, all of
which turned out to be the wrong answers to the original questions.

It all started when I had `mail.jrgnsn.net` set to redirect to
`fastmail.com`. I kept getting *Invalid Certificate* errors since the
domain was `jrgnsn.net` but the server is (and the certificate was for)
`fastmail.com`. I spent a good few months perplexed why there would be
an issue, *"mail dot" all my other domains* worked just fine, after all.

So I went ahead and switched name servers on all of my domains to
FastMail's domain servers. This accomplishes several things:

1. I don't have to worry about configuring all the MX, TXT, and other
records related to mail systems.
2. FastMail is much more convenient for me to access than CloudFlare.
3. I don't necessarily require DDoS protection (which I wasn't paying
for anyhow).
4. FastMail makes it incredibly easy to serve a file or directory from
my file storage at FastMail.

Unfortunately, all the existing issues remained and I even added a
few issues to the list! `mail.jrgnsn.net` was still returning
certificate errors. Additionally, `https://veganmsp.com` wasn't being
resolved at all. dig-ing at `1.1.1.1`, `1.0.0.1`, `8.8.8.8`, and
`8.8.4.4` were all returning SERVFAIL for `veganmsp.com`.

This was frustrating to say the least, but I learned a valuable lesson
throughout this process that I have yet to put into practice.
**Documentation is important.**

I'll say it again, **DOCUMENTATION IS IMPORTANT!**

It turns out there was an HTTP Strict Transport Security (HSTS)
directive tucked neatly away in an Nginx snippet. I don't remember
putting it there, perhaps Ghost did that automatically when I upgraded.
After removing the HSTS directive and forcing Firefox to "Forget About
This Site", my `jrgnsn.net` domain was behaving as expected!

`veganmsp.com` was still giving me trouble, though, and it was worse
than `jrgnsn.net`. Certain DNS resolvers would return a `SERVFAIL` while
others would return `NOERROR` with the correct values. I was baffled!
*Why in the hell are some resolvers working while others, especially the
"big name" resolvers weren't?!* Turns out the answer was simple and if I
weren't so curious, perhaps I could have avoided this issue altogether.

There was a DNSSEC record for veganmsp.com. 🙄

After some quick research, I learned one of the better methods to
changing name servers with a DNSSEC record is to delete the record at
the Registrar, wait 24 hours, delete the record at the name server, and
then change name servers.

I changed name servers, was looking into this for a few days, and then
deleted the record at the registrar.

---

At the time of this writing `1.1.1.1`, `1.0.0.1`, `8.8.8.8`, and
`8.8.4.4` still return `SERVFAIL`. I'll give it 24-48 hours until I
continue the investigation.

And maybe I'll start a local wiki to keep domain, DNS, and security
configurations in a central location.

How am I employed in this industry, again?

[fastmail-ref]: https://www.fastmail.com/?STKI=17835792
