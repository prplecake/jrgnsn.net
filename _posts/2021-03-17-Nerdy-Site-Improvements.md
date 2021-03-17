---
title: Nerdy Site Improvements
layout: post
---

Let's skip right over how long it's been since I've written a blog post
and dive right into some recent changes I've made on this site. A lot of
it is behind the scenes in the scripts I use to manage creating new
posts, publishing drafts, deploying the site to my server and just today
I wrote a quick ruby script that adds the current commit hash to
`_config.yml`.

In case you aren't already aware, I use Jekyll as my static-site
generator.

## Creating New Posts

I was getting tired of having to manually write the YAML front-matter
for each new post, so I wrote a bash script that asks for the title and
creates it in `_posts/` with the date prepended before a slugified
title. I could pass `-d` which will create it as a draft instead, of
course placing it in `_drafts/` with no date.

## Publishing Drafts

I've written another script to assist with publishing drafts. I'm rather
proud of this script since it shows me a list of each draft,  and
adding "today's" date to the title, then moving it to `_posts/`. 

Today's improvements to this script include adding the ability to
automatically create a commit with the new post. If I pass `-D`, this
would also run my deploy script.

## Deployment

Another simple bash script to actually deploy to my server. It's main
job is to build the site, and update `site.git_hash`, and `scp` it to my
server. It runs my ruby script to get the current hash of HEAD, then
updates `_config.yml`. Finally it creates another commit to track we've
updated `site.git_hash`.

## `update_version.rb`

This script was written because it seems easiest to work with YAML files
using a scripting langauge that isn't bash, and since Jekyll is written
in ruby I just decided to use it.

There's probably some massive improvements that could be made since, if
I run all three scripts, that's at least two new commits for each
deployment. However, I think I'll be okay with this process since it
doesn't make sense to add the commit hash to the site footer and have it
go to the ["updating hash" commit](https://git.sr.ht/~mjorgensen/jrgnsn.net/commit/ddad15559f5336068111ba932a626d9bf4418634).

---

You can find these scripts in the [jrgnsn.net][git.sr.ht] repo incase
you're bored and want to read some bad code.

[git.sr.ht]:https://git.sr.ht/~mjorgensen/jrgnsn.net/tree/master/item/contrib
