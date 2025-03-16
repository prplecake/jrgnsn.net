---
date: "2024-04-10T00:00:00Z"
type: "post"
title: Regetwitter Fail
---

The website formerly known as Twitter recently made some changes in the way they
handled domains. This change has since been reverted or corrected, but the fact
it happened at all should be a lesson to developers everywhere.

I won't speculate on what problem they were trying to solve over at X Corp.
Essentially, they were doing a replacement on "x.com", changing it to
"twitter.com". Normal stuff when you're trying to decide which domain you
actually want to use, which they seem to be struggling with a little.

Except this was a substring replacement, or a regex gone wrong, that results in
**any URL ending in "x.com" to be changed** resulting in replacements like:

* `goodrx.com -> goodrtwitter.com`
* `square-enix.com -> square-enitwitter.com`
* `carfax.com -> carfatwitter.com`

among many, many others. I own those three domains (the ones ending in
"twitter.com") as well as a few others. Those sites display a message like this:

![goodrtwitter.com screenshot]({{< slug >}}/ruseriousx.png)

I saw the [original page by Nanashi][nanashi-proj] and [converted it to a
Next.JS app][my-proj] I could host once with Vercel and point many domains at
the one deployment.

[nanashi-proj]: https://github.com/sevenc-nanashi/roblotwitter
[my-proj]: https://github.com/prplecake/x-no-twitter.com

## See also

* [@seraph@wetdry.world's mastodon post](https://wetdry.world/@seraph/112241754503585255)
* [Twitter's Clumsy Pivot to X.com Is a Gift to
  Phishers](https://krebsonsecurity.com/2024/04/twitters-clumsy-pivot-to-x-com-is-a-gift-to-phishers/) - Krebs on Security[^1]

[^1]: Supposedly they tried reaching out to me on Mastodon, but I never heard
    from anyone.
