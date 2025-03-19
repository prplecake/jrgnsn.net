+++
date = '2025-03-17T12:11:30-05:00'
draft = true
title = 'Migrating From Jekyll to Hugo'
+++

I've known for awhile that my days with Jekyll were numbered. The biggest pain
points with Jekyll was an inability to add tags to posts. Not for lack of
[my](https://github.com/prplecake/jrgnsn.net/pull/7) [trying](https://github.com/prplecake/jrgnsn.net/pull/74)[^1]
support on Jekyll's side, but on GihHub Pages. GitHub Pages has a real bias
against custom plugins and no matter how hard I tried, I couldn't get
reproducible builds in CI. (Though I could reproduce the issues with CI
locally.) GitHub support was of no help. An easy solution to this problem is to
stop using GitHub Pages but instead I decided to give another static-site
generator a try.

[^1]: [And to think I *almost* decided to write my own CMS in C#.](https://github.com/prplecake/jrgnsn.net/pull/74#issuecomment-2612182603)

Enter Hugo. I've always liked Go better than Ruby anyway. 

My goals for the migration were pretty minimal:

* Preserve permalinks
* Make it visually identical
* Tags!

I hit two out of three. Hugo has some opinions about URLs, and I disagree with
some of them. I don't think URLs ending with `.html` are ugly. Perhaps SEO
prefers to omit the extension, but for why? Hugo also prefers lowercase URLs,
and I don't have any strong opinions here either way. I just know that I didn't
want lowercase URLs in this case because Jekyll used uppercase characters in
their URLs. This was an easy fix:

```toml
# hugo.toml
disablePathToLower = true

[permalinks]
[permalinks.page]
posts = "/:year/:month/:day/:slug"

[uglyURLs]
posts = true
```

For the most part I was able to construct the URLs for posts identically to how
they were on the Jekyll site. *Except* for cases where the post title includes a
special character, or, more specifically, a [reserved
character](https://datatracker.ietf.org/doc/html/rfc3986#section-2.2). This is
where I start agreeing with Hugo more than Jekyll. I don't think Jekyll should
have allowed reserved characters in the URL. Fortunately this only breaks a
couple of links:

* [GitHub Pages + Jekyll, a poor
  choice](/2024/05/17/GitHub-Pages--Jekyll-a-poor-choice.html)
* [OCI: Don't Even Think About
  It](/2021/12/05/OCI-Dont-Even-Think-About-It.html)


