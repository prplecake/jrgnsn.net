---
date: "2020-08-17T00:00:00Z"
type: "post"
title: Status update, August 2020
---

August has been a month of revival. Six months of staying at home during
the coronavirus pandemic has taken its toll on most aspects of my life.
I'm fortunate enough to remain employed. Working from home had
introduced its unique set of challenges. Things like staying motivated
and focused were especially hard during the earlier months.

I moved, at the end of April, out of my brother's and into an apartment
with a friend of mine. After three and a half months living here, I've
learned some lessons for the next time I move in with someone.
Hopefully, I won't have to worry about it and in about 5 years I'm
thinking about being a homeowner. I like Minneapolis enough to stay
here.

Things that have been revived this month include my desire to write code
and play video games. I [reworked my dotfiles][0] after finding [this
project by Charles Gould][1]. Having a modular install script is
something I've been meaning to write for a while now and Charles'
solution was perfect for me. I've started making my way through the book
[Mazes for Programmers][2], which introduces programmatically
maze-making using Ruby. This happens to be my first real foray into the
Ruby language.

[0]:https://github.com/prplecake/dotfiles
[1]:https://git.sr.ht/~crg/config
[2]:http://www.mazesforprogrammers.com/

I've also reignited the sysadmin in me, both at work and for hobby
stuff. In work terms, I've been much more motivated and excited to solve
issues and resolve problems for others. I'd like to write more code at
work, but I've got a decent backlog of stuff to work through before
getting to some ideas I have. I spun up a [Minio][3] server for object
storage for both my Pleroma instance and my Nextcloud instance. This let
me migrate my Nextcloud installation to the same server Pleroma is
running on, slightly reducing costs. I think I might be able to reduce
the size of the VPS Pleroma and Nextcloud run on, but I won't make that
decision until I have a months worth of performance data. On that note,
I spun up another tiny VPS for Grafana and Prometheus.

[3]:https://min.io

While keeping in mind all the projects I've started that are *nowhere
near* complete, I'm been thinking about future projects. It's been a few
years since I've been wanting to create a recipe manager for myself.
I've tried using [Paprika 3][4], [Cookbook for Nextcloud][5], among
others and none of the options I've tried fit the particular use-case
I've convinced myself I have.

[4]:https://www.paprikaapp.com/
[5]:https://apps.nextcloud.com/apps/cookbook

My brother made a weather dashboard which has inspired me to create
a more personalized one. He includes a Minneapolis dashboard, but
there's a real possibility I'd create a dashboard to run on my local
network that could show my Pi-hole metrics among other things.

That's it for this month. Enjoy the rest of the summer!
