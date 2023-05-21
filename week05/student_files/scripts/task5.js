/* IF/ELSE IF */
let currentDate = new Date();
let dayOfWeek = currentDate.getDay();
let message = '';

if (dayOfWeek > 0 && dayOfWeek < 6) {
    message = 'Hang in there!';
} else {
    message = 'Woohoo! It is the weekend!';
}

/* SWITCH, CASE, BREAK */
let dayMessage = '';

switch (dayOfWeek) {
    case 0:
        dayMessage = 'Sunday';
        break;
    case 1:
        dayMessage = 'Monday';
        break;
    case 2:
        dayMessage = 'Tuesday';
        break;
    case 3:
        dayMessage = 'Wednesday';
        break;
    case 4:
        dayMessage = 'Thursday';
        break;
    case 5:
        dayMessage = 'Friday';
        break;
    case 6:
        dayMessage = 'Saturday';
        break;
    default:
        dayMessage = 'Invalid day';
}

/* OUTPUT */
document.getElementById('message1').textContent = message;
document.getElementById('message2').textContent = dayMessage;

/* FETCH */
let templeList = [];

function output(temples) {
    const parentElement = document.getElementById('temples');
    temples.forEach(temple => {
        let article = document.createElement('article');
        let h3 = document.createElement('h3');
        let h4_1 = document.createElement('h4');
        let h4_2 = document.createElement('h4');
        let img = document.createElement('img');

        h3.textContent = temple.templeName;
        h4_1.textContent = temple.location;
        h4_2.textContent = temple.dedicated;
        img.src = temple.imageUrl;
        img.alt = temple.templeName;

        article.append(h3, h4_1, h4_2, img);
        parentElement.append(article);
    });
}

async function getTemples() {
    const response = await fetch('https://byui-cse.github.io/cse121b-course/week05/temples.json');
    templeList = await response.json();
    output(templeList);
}

getTemples();

function reset() {
    document.getElementById('temples').innerHTML = '';
}

function sortBy() {
    reset();
    let sortByValue = document.getElementById('sortBy').value;
    let sortedTemples = templeList.sort((a, b) => a[sortByValue].localeCompare(b[sortByValue]));
    output(sortedTemples);
}

document.getElementById('sortBy').addEventListener('change', sortBy);