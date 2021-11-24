SHELL := /bin/bash
BUNDLE := bundle
JEKYLL := $(BUNDLE) exec jekyll

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

clean:
	rm -r _site