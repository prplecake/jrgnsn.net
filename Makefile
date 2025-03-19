SHELL := /bin/bash
.PHONY: all clean install update

all: serve

build:
	HUGO_ENV=production hugo --gc --minify

serve:
	HUGO_ENV=production hugo server

serve-with-drafts:
	HUGO_ENV=production hugo server --buildDrafts

clean:
	rm -r public
