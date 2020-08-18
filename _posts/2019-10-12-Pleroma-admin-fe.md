---
layout: post
title: "Pleroma: admin-fe"
tags:
  - Pleroma
---

I recently switched from Mastodon to Pleroma, for no real reason in
specific other than: Pleroma is far less resource intensive.
Setting up Pleroma was a breeze compared to setting up Mastodon--I
remember having a great deal of trouble. I enjoy the simpler
interface as well. While Pleroma doesn't have *all* of the flashy
features[^1] mastodon does, I don't need or use them anyway.

Tonight I came across [admin-fe][admin-fe] for Pleroma, and thought I'd
give it a shot even though I'm the only use on my instance. Installation
documentation is lacking for the project, so I thought I'd offer my
notes in case they could be useful to anyone else.

---

I downloaded the latest version of admin-fe[^2] and unarchived the
tarball.

```
curl -O https://git.pleroma.social/pleroma/admin-fe/-/archive/1.2.0/admin-fe-1.2.0.tar.gz
tar xzvf admin-fe-1.2.0.tar.gz
```

Entered the directory and attempted to run `yarn build:prod` per the
README:

```
cd admin-fe-1.2.0
yarn build:prod
```

I was greeted with a somewhat unhelpful error:

> Yarn requires Node.js 4.0 or higher to be installed.

So I looked Node.js up, I remember it takes more to install than `sudo
apt install nodejs`. I saw the latest version was 12, which I found odd
considering Pleroma's fairly new on the block.

After installing Node.js, `yarn build:prod` still wasn't successful:

```
$ yarn build:prod
yarn run v1.17.3
$ cross-env NODE_ENV=production env_config=prod node build/build.js
/bin/sh: 1: cross-env: not found
error Command failed with exit code 127.
info Visit https://yarnpkg.com/en/docs/cli/run for documentation about this command.
```

I solved this by simply running `yarn` with no arguments. Finally,
`yarn build:prod` was successful and I had a built distribution of
admin-fe. 

Serving admin-fe was a bit of a hassle, mostly due to my limited
knowledge of nginx configuration. At first, my config looked like this:

```
location /_admin-fe/ {
	root /var/www/pleroma/admin-fe;
	index index.html
}
```

Which was failing with an HTTP 404, requests to example.com/_afe were
trying to be served from `/var/www/pleroma/admin-fe/_afe/`. I needed to
switch the `root` directive for the `alias` directive, add a trailing
slash, and I was in business. 

**Note:** I tried to pick a URL that *probably* wouldn't become a
username. There was no hint towards what to call it in the limited
documentation--not that it really matters on an instance for 1 anyway.

The ending result is my `location` block ended up reading this:

```
location /_admin-fe/ {
	alias /var/www/pleroma/admin-fe/;
	index index.html;
}
```

Now the login page loads, is on the right domain[^3], *and* it
successfully authenticates against my Pleroma database.

Unfortunately, admin-fe seems to have a lot of trouble on my instance
and I'm sure it's due to the way I configured Pleroma. I can't find any
documentation on it, but I vaguely remember choosing something along the
lines of "Don't allow site configuration changes from the frontend."
Thus, my adventures setting up admin-fe have come to an end.

After reaching out on #pleroma on freenode, it's a big possibility that
admin-fe doesn't work for me because the necessary pieces aren't
implemented on the backend. 

If you think you can help, please don't hesitate to find me on the
fediverse or write to my [public-inbox][lists].


[admin-fe]: https://git.pleroma.social/pleroma/admin-fe
[lists]: https://lists.sr.ht/~mjorgensen/public-inbox

[^1]: Mastodon 3.0 came out today, some of their new features include:
	moving accounts, audio in toots, and hashtag auto-suggestions.

[^2]: 1.2.0 at the time of writing

[^3]: At first I thought about hosting admin-fe at something like
	`admin.example.com`, but that didn't work because now admin-fe would
	try to load the Pleroma API at `admin.example.com/api/...` instead
	of `example.com/api/...`.