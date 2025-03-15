---
date: "2018-05-12T00:00:00Z"
tags:
- Pi-hole
- Nextdoor
type: "post"
title: Block Sponsored Posts on Nextdoor with Pi-hole
---

I occasionally use Nextdoor to list items for sale, or to see what's
going on in my neighborhood and surrounding area. I have to say, I like
Nextdoor since it allows me to connect with my neighbors in a relatively
locked-down place, even though I'm sure most of them are in local
Facebook groups already.

However, I don't like seeing sponsored content. **Period.**

Here's how I managed to hide sponsored posts on Nextdoor.

## Using Pi-hole

If you have a [Pi-hole][pi-hole] set up on your network, go ahead and
add the following to your Blacklist:

    flask.nextdoor.com

I added it as an "exact" filter and it seems to work fine.

[pi-hole]: https://pi-hole.net

## Using uBlock Origin

You can create a custom filter in uBlock Origin that blocks the
following domain:

    ||flask.nextdoor.com/events/*$xmlhttprequest,first-party

Add the above to the end of your filters on "My filters" in uBlock
settings.

## Disclaimer

I expect this to break in the future as Nextdoor catches on (or finds
this post!) and as ad-serving becomes more sophisticated.
