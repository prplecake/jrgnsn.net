+++
date = '2025-08-19T15:25:42-05:00'
draft = false
title = 'NextJS With Google ReCaptcha Enterprise'
tags = ['NextJS', 'Google ReCaptcha', 'ReCaptcha', 'react', 'sysjob1']
+++

I haven't been able to find a guide for this yet, but there's plenty of guides
out there for Recaptcha v2 and v3. Many of the v3 packages *can work* with
ReCaptcha Enterprise, but none of them seem to support policy-based challenges.
This guide will show you how to implement ReCaptcha Enterprise with policy-based
challenges in a NextJS app without any external libraries.

This guide will not cover how to obtian a ReCaptcha Enterprise key, nor how to
validate the token on the server side.

## ReCaptcha Component

Create a new component called `ReCaptcha.tsx` in your components directory. This
component will handle the rendering of the ReCaptcha widget and the logic for
handling submissions.

```tsx
"use client";

import React, {useEffect} from "react";
import Script from "next/script";

export const RECAPTCHA_SITE_KEY = process.env.NEXT_PUBLIC_RECAPTCHA_SITE_KEY || "";

declare global {
  interface Window {
    recaptchaPolicyCallback?: (token: string) => void;
  }
}

type RecaptchaProps = {
  onToken: (token: string) => void;
};

export function Recaptcha({ onToken }: RecaptchaProps) {
  // Make the callback globally visible for `data-callback`
  useEffect(() => {
    window.recaptchaPolicyCallback = onToken;
    return () => {
      delete window.recaptchaPolicyCallback;
    };
  }, [onToken]);

  return (
  <Script
    src={`https://www.google.com/recaptcha/enterprise.js?render=${RECAPTCHA_SITE_KEY}`}
    onLoad={() => {
      console.log("Recaptcha script loaded");
    }}
    strategy="afterInteractive"
  />
  );
}
```

## Using the ReCaptcha Component

Now you can use the `Recaptcha` component in your forms. Here's an example:

```tsx
"use client";
import {RECAPTCHA_SITE_KEY, Recaptcha} from "@/components/recaptcha";

export default function Home() {

  function onSubmit(token: string) {
    console.log("Recaptcha token received:", token);
    // Here you would typically send the token to your server for validation
  }

  return (
    <div>
      <form id="test-form">
        <Recaptcha onToken={onSubmit}/>
        <button
          className="g-recaptcha"
          data-action="test_page"
          data-callback="recaptchaPolicyCallback"
          data-sitekey={RECAPTCHA_SITE_KEY}
        >Submit
        </button>
      </form>
    </div>
  );
}
```

The recaptcha won't trigger until the button is clicked, and Google's script
pretty much handles the rest. I had trouble figuring out the `data-callback` part,
and it also seems like Google's script requires the button have the `g-recaptcha`
class. Hopefully this saves someone else some time.

A full example is available here: 
[prplecake/recaptcha-enterprise-nextjs](https://github.com/prplecake/recaptcha-enterprise-nextjs)

