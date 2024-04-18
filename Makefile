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
	$(BUNDLE) config set --local path 'vendor/bundle'
	$(BUNDLE) install

update: $(PROJECT_DEPS)
	$(BUNDLE) update

git_hash=`git rev-parse --short HEAD`

update-version:
	echo -n $(git_hash) > $(PWD)/_includes/version

build: update-version install
	JEKYLL_ENV=production $(JEKYLL) build

serve: update-version install
	JEKYLL_ENV=production $(JEKYLL) serve

HASHMARK := \#
grep_cmd := grep -v '^$(HASHMARK)' | sed '/^$$/d'
proofer_ignore_files := `awk '{print}' .proofer_ignore_files | $(grep_cmd) | paste -s -d, -`
proofer_opts := --ignore-files $(proofer_ignore_files)

html-proof: install build
	$(HTMLPROOFER) $(proofer_opts) _site

clean:
	rm -r _site
