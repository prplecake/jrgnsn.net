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

HASHMARK := \#
grep_cmd := grep -v '^$(HASHMARK)' | sed '/^$$/d'
proofer_ignore_files := `awk '{print}' .proofer_ignore_files | $(grep_cmd) | paste -s -d, -`
proofer_opts := --check-html --file-ignore $(proofer_ignore_files)

html-proof: install build
	$(HTMLPROOFER) $(proofer_opts) _site

clean:
	rm -r _site