/*jshint multistr: true */
setInterval(function () {

  const start = luxon.DateTime.fromMillis(chosenDate);
  const end = luxon.DateTime.now();

  const diff = end.diff(start, ["months", "weeks", "days", "hours", "minutes", "seconds"]).toObject();

  document.getElementById("countup").innerHTML =
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

}, 1000);
