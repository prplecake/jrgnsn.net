/*jshint multistr: true */
setInterval(function () {

  const today = new Date().getTime();

  const diff = today - chosenDate;

  let months = Math.floor(diff / (1000 * 60 * 60 * 24 * 7 * 4));
  let weeks = Math.floor((diff % (1000 * 60 * 60 * 24 * 7 * 4)) / (1000 * 60 * 60 * 24 * 7));
  let days = Math.floor((diff % (1000 * 60 * 60 * 24 * 7)) / (1000 * 60 * 60 * 24));
  let hours = Math.floor((diff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
  let minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
  let seconds = Math.floor((diff % (1000 * 60)) / 1000);

  document.getElementById("countup").innerHTML =
    "<div class=\"months\"> \
			<div class=\"c-number\">" + months + "</div>months</div> \
			<div class=\"weeks\"> \
			<div class=\"c-number\">" + weeks + "</div>weeks</div> \
			<div class=\"days\"> \
			<div class=\"c-number\">" + days + "</div>days</div> \
			<div class=\"hours\"> \
			<div class=\"c-number\">" + hours + "</div>hours</div> \
			<div class=\"minutes\"> \
			<div class=\"c-number\">" + minutes + "</div>minutes</div> \
			<div class=\"seconds\"> \
			<div class=\"c-number\">" + seconds + "</div>seconds</div> \
			</div>";

}, 1000);
