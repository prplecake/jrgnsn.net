SHELL := /bin/bash
.PHONY: all clean install update

all: serve

build:
	HUGO_ENV=production hugo --gc --minify

serve:
ifeq ($(CODESPACES),true)
	@echo "In a codespace";\
	HUGO_ENV=production hugo server -D --appendPort=false --baseURL https://${CODESPACE_NAME}-1313.${GITHUB_CODESPACES_PORT_FORWARDING_DOMAIN}
else
	HUGO_ENV=production hugo server
endif

serve-with-drafts:
	HUGO_ENV=production hugo server --buildDrafts

clean:
	rm -r public
