---
layout: post
title: Base64 Encoding in Python 3
tags: 
  - Python 3
  - Base64
---

Recently I've been working on a backup script to backup a folder to a
Backblaze B2 bucket. If only I had known I was about to spend an hour
working on four silly lines of python code...

Backblaze's documentation has absolutely *no mention* that this only
works with Python 2. [I'm not the only one][0][^1]. Pronoy had this
exact
issue with a Google API. Again, *no mention* it only works in Python 2.
In fact, the moment I stumbled across Pronoy's post is the same moment I
stopped ripping my hair out.

[0]:https://web.archive.org/web/20181211020351/https://www.pronoy.in/2016/10/20/python-3-5-x-base64-encoding-3/

Let's get into the code. According to Backblaze's API docs, they require
the Account ID and Key to be base64 encoded in the following format: 
`'accoundId:accountKey'`. Seems easy enough.

Try this in a Python 2 shell:

```python
base64.b64encode('A string')
```
    
You get an output like:

```
'QSBzdHJpbmc='
```
    
Try running the same thing in a Python 3 shell and you see something
closer to this:

      File "<stdin>", line 1, in <module>
      File "/usr/local/Cellar/python/3.6.5/Frameworks/Python.framework/ Versions/3.6/lib/python3.6/base64.py", line 58, in b64encode 
        encoded = binascii.b2a_base64(s, newline=False)
    TypeError: a bytes-like object is required, not 'str'
    
The solution? There's a few. Pronoy's works: which is to encode the
string in `UTF-8`. Then, once it's base64 encoded, you decode that as
`ASCII`.

This works:

```python
base64.b64encode('A string'.encode('UTF-8')).decode('ascii')
```

After doing some more scouring, I found [this Stackoverflow thread][1].

```python
# The following works, too, and looks a little neater.
base64.b64encode(bytes('A string', 'utf-8')).decode('ascii')
```

Here's [another Stackoverflow thread][2] explaining why.

[1]: https://stackoverflow.com/questions/40454177/how-to-encode-a-string-with-base64-in-python-3-and-remove-the-new-lines
[2]: https://stackoverflow.com/questions/8908287/why-do-i-need-b-to-encode-a-python-string-with-base64

[^1]:The [original link](https://www.pronoy.in/2016/10/20/python-3-5-x-base64-encoding-3/)
is dead. So here's a web archive URL.
