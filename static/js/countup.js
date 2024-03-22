/*jshint multistr: true */
const counterInnterHtmlBuilder = (diff) =>
  "<div class=\"months\"> \
  <div class=\"c-number\">" + diff.months + "</div>months</div> \
  <div class=\"weeks\"> \
  <div class=\"c-number\">" + diff.weeks + "</div>weeks</div> \
	<div class=\"days\"> \
  <div class=\"c-number\">" + diff.days + "</div>days</div> \
  <div class=\"hours\"> \
  <div class=\"c-number\">" + diff.hours + "</div>hours</div> \
  <div class=\"minutes\"> \
  <div class=\"c-number\">" + diff.minutes + "</div>minutes</div> \
  <div class=\"seconds\"> \
  <div class=\"c-number\">" + Math.floor(diff.seconds) + "</div>seconds</div> \
  </div>";



counters.forEach((counterTime, i) => {
  let divId = `counter-${i}`;
  let div = document.createElement("div");
  div.setAttribute("id", divId);
  div.classList.add("counter");
  document.getElementsByClassName("main")[0].append(div);
  setInterval(() => {
    const start = luxon.DateTime.fromMillis(counterTime);
    const end = luxon.DateTime.now();

    const diff = end.diff(start, ["months", "weeks", "days", "hours", "minutes", "seconds"]).toObject();

    div.innerHTML = counterInnterHtmlBuilder(diff);


  }, 1000);
});
