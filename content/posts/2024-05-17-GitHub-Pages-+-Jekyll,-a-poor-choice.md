---
date: "2024-05-17T00:00:00Z"
type: "post"
tags: ["GitHub Pages", "Jekyll"]
title: GitHub Pages + Jekyll, a poor choice
---

Some of you will say that using GitHub in the first place is a poor choice, and
you'd be correct.

GitHub, and in fact Jekyll, would like you to believe that GitHub is a perfectly
fine place for hosting your Jekyll site, and it is, provided you want to do
absolutely **nothing fancy.** As, for reasons I'm not sure GitHub has
publicized, they lock down what plugins you can use, and if you're not using an
[approved plugin][gh-pages-plugins], you're SOL.

[gh-pages-plugins]:https://pages.github.com/versions/

If you don't, you don't get to use GitHub Actions to build and publish your
site, so you probably couldn't make small adjustments in a web editor to then
become live.

I believe all of the above is true *regardless of whether you're deploying to
GitHub Pages.* For example, if you just used GH Actins to build your site to be
deployed on your own server, you're subject to all the same GH Actions Jekyll
restrictions.

---

I've been working on adding a rudimentary tag system to this site, but I keep
running into the brick wall that is GitHub.

In an effort to minimize my dependency on potentially unapproved plugins, I
thought writing my own to be included in my local `_plugins/` directory would be
safe, and damn, was I wrong.

From what I can tell, GitHub executes the custom plugin code just fine, but
then, something, I haven't found *what* yet, but *something* is preventing the
plugin-generated-files to becoming part of the `_site/` build.

I've reached out to GitHub and the jekyll community for support, but I'm not
holding my breath.

## Alternatives?

What are some alternatives? I could not have tags on this site and it would be
business as usual as we have been for the last, 12 years. I could set up a git
remote on a server I own and set up some hooks to deploy any changes. I could
give sourcehut a try. I could rewrite the site in Next.JS to be hosted by
Vercel.

That last one seems like the nuclear option, though it could provide some
interesting ways to create in the future. The thing I am striving to avoid is
*enshittification*, because every single other platform is doing more than enough
of that to go around. This site doesn't *need* any JavaScript to work, but if I
can generate static HTML with NextJS + Vercel, then, you wouldn't really know
the difference.
