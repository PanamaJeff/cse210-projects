// Step 1, 2, 3
let currentDate = new Date();
let dayOfWeek = currentDate.getDay();

// Step 4
let message1;

// Step 5
if (dayOfWeek >= 1 && dayOfWeek <= 5) {
  message1 = 'Hang in there!';
} else {
  message1 = 'Woohoo! It is the weekend!';
}

// Step 6
let message2;

switch (dayOfWeek) {
  case 0:
    message2 = 'Sunday';
    break;
  case 1:
    message2 = 'Monday';
    break;
  case 2:
    message2 = 'Tuesday';
    break;
  case 3:
    message2 = 'Wednesday';
    break;
  case 4:
    message2 = 'Thursday';
    break;
  case 5:
    message2 = 'Friday';
    break;
  case 6:
    message2 = 'Saturday';
    break;
}

// Step 7
document.getElementById('message1').innerHTML = message1;
document.getElementById('message2').innerHTML = message2;

// Step 8
let templeList = [];

async function output(temples) {
  let templesDiv = document.getElementById('temples');
  templesDiv.innerHTML = '';

  temples.forEach(temple => {
    let article = document.createElement('article');

    let h3 = document.createElement('h3');
    h3.textContent = temple.templeName;

    let h4_1 = document.createElement('h4');
    h4_1.textContent = temple.location;

    let h4_2 = document.createElement('h4');
    h4_2.textContent = temple.dedicated;

    let img = document.createElement('img');
    img.src = temple.imageUrl;
    img.alt = temple.templeName;

    article.appendChild(h3);
    article.appendChild(h4_1);
    article.appendChild(h4_2);
    article.appendChild(img);

    templesDiv.appendChild(article);
  });
}

async function getTemples() {
  let response = await fetch('https://byui-cse.github.io/cse121b-course/week05/temples.json');
  templeList = await response.json();
  output(templeList);
}

getTemples();

function reset() {
  document.getElementById('temples').innerHTML = '';
}

function sortBy() {
  reset();
  
  let sortValue = document.getElementById('sortBy').value;
  let sortedList = [...templeList];

  sortedList.sort((a, b) => (a[sortValue] > b[sortValue]) ? 1 : -1);

  output(sortedList);
}

document.getElementById('sortBy').addEventListener('change', sortBy);