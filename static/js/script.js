function addEvent(elementId, eventId) {
  document.getElementById(elementId).addEventListener('click', () => {
    fathom.trackGoal(eventId, 0);
  });
}

window.addEventListener('load', () => {
  const links = [
    {
      link: 'ext-link-gh',
      event: 'G529FERJ'
    },
    {
      link: 'ext-link-email',
      event: 'UTDYEHZA'
    },
    {
      link: 'link-age',
      event: 'RFA1OFV7'
    },
    {
      link: 'link-pgp',
      event: '00DEG4LH'
    },
    {
      link: 'link-more',
      event: 'ME1WCPRU'
    },
    {
      link: 'ext-link-repo',
      event: 'HX5ZMKNJ'
    },
    {
      link: 'ext-link-commit-hash',
      event: 'XG6MZXBV'
    }
  ];
  links.forEach(link => {
    try {
      addEvent(link.link, link.event);
    } catch (error) {
      console.error(error);
    }
  });
});
