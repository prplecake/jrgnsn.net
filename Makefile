SHELL := /bin/bash
BUNDLE := bundle
JEKYLL := $(BUNDLE) exec jekyll
HTMLPROOFER := $(BUNDLE) exec htmlproofer

PROJECT_DEPS := Gemfile

.PHONY: all clean install update

all: serve

check:
	$(JEKYLL) doctor

install: $(PROJECT_DEPS)
	$(BUNDLE) install --path vendor/bundle

update: $(PROJECT_DEPS)
	$(BUNDLE) update

build: install
	$(JEKYLL) build

serve: install
	JEKYLL_ENV=production $(JEKYLL) serve

html-proof: install build
	$(HTMLPROOFER) _site

clean:
	rm -r _site