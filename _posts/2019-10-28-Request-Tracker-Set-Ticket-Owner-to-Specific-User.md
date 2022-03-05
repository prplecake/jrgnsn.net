---
layout: post
title: "Request Tracker: Automatically Set Ticket Owner to Specific
User"
tags:
  - Request Tracker
---

Request Tracker is an insanely powerful ticketing system. I started
using RT about 6 months ago, and I've been absolutely enjoying every
hour of it. About 2 months ago, I used a guide on the RT Wiki titled
[AutoSetOwner](https://rt-wiki.bestpractical.com/wiki/AutoSetOwner) to
automatically set *every new ticket's* owner to myself, on my personal
hosted instance of RT. It works great!

Now, since I'm more or less the only IT person at this company, it's
safe to assume that any ticket in the IT Projects, or certain other
queues should belong to me. So I adapted the AutoSetOwner scrip to
automatically set the owner of a new ticket to myself, on certain
queues. I have to admit, since I don't know Perl hardly at all, it took
a little trial and error to get this right, fortunately RT's logs came
in useful and I now have a working scrip!

The process is very similar to *AutoSetOwner* by Nicolas C. I've simply
added a few lines to get the ID of the user whom I want to own these new
tickets.

| **Description** | Set IT tickets to IT manager |
| **Condition**   | On Create                    |
| **Action**      | User Defined                 |
| **Template**    | Blank                        |

**Custom action preparation code:**

```perl
return 1;
```

**Custom action commit code:**

```perl
# get actor ID
my $Actor = $self->TransactionObj->Creator;

# get mjorgensen's id
# create a new user object
my $NewOwner = RT::User->new($RT::SystemUser);

# and load the user into that object
$NewOwner->Load('mjorgensen');

# if actor is RT_SystemUser then get out of here
return 1 if $Actor == $RT::SystemUser->id;

# prevents a ticket being assigned to an unprivileged user,
# comment out if you want this
return 1 unless $self->TransactionObj->CreatorObj->Privileged;

# get out unless ticket owner is nobody
return 1 unless $self->TicketObj->Owner == $RT::Nobody->id;

# ok, try to change owner
$RT::Logger->info("Auto assign ticket #". $self->TicketObj->id ." to user #". $NewOwner->id );
my ($status, $msg) = $self->TicketObj->_Set(Field => 'Owner', Value => $NewOwner->id, RecordTransaction => 0);
unless( $status ) {
$RT::Logger->error( "Impossible to assign the ticket to 'mjorgensen': $msg" );
return undef;
}
return 1;
```

Be sure the change `mjorgensen` to what your user's name is. Save
changes, assign this scrip to each desired queue, and you're all
set!
