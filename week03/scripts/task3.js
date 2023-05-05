// Function Declarations
function add(number1, number2) {
    return number1 + number2;
}

function addNumbers() {
    let addend1 = parseFloat(document.getElementById('addend1').value);
    let addend2 = parseFloat(document.getElementById('addend2').value);
    document.getElementById('sum').value = add(addend1, addend2);
}

document.getElementById('addNumbers').addEventListener('click', addNumbers);

// Function Expressions
const subtract = function(number1, number2) {
    return number1 - number2;
}

const subtractNumbers = function() {
    let minuend = parseFloat(document.getElementById('minuend').value);
    let subtrahend = parseFloat(document.getElementById('subtrahend').value);
    document.getElementById('difference').value = subtract(minuend, subtrahend);
}

document.getElementById('subtractNumbers').addEventListener('click', subtractNumbers);

// Arrow Functions
const multiply = (number1, number2) => number1 * number2;

const multiplyNumbers = () => {
    let factor1 = parseFloat(document.getElementById('factor1').value);
    let factor2 = parseFloat(document.getElementById('factor2').value);
    document.getElementById('product').value = multiply(factor1, factor2);
}

document.getElementById('multiplyNumbers').addEventListener('click', multiplyNumbers);

// Any Function Declaration Type
function divide(number1, number2) {
    return number1 / number2;
}

function divideNumbers() {
    let dividend = parseFloat(document.getElementById('dividend').value);
    let divisor = parseFloat(document.getElementById('divisor').value);
    document.getElementById('quotient').value = divide(dividend, divisor);
}

document.getElementById('divideNumbers').addEventListener('click', divideNumbers);

// Built-In Methods
const currentDate = new Date();
const currentYear = currentDate.getFullYear();
document.getElementById('year').innerHTML = currentYear;

// Array Methods
const numberArray = Array.from({length: 25}, (_, i) => i + 1);
document.getElementById('array').innerHTML = numberArray.join(', ');

document.getElementById('odds').innerHTML = numberArray.filter(x => x % 2 !== 0).join(', ');
document.getElementById('evens').innerHTML = numberArray.filter(x => x % 2 === 0).join(', ');

document.getElementById('sumOfArray').innerHTML = numberArray.reduce((a, b) => a + b, 0);

document.getElementById('multiplied').innerHTML = numberArray.map(x => x * 2).join(', ');

document.getElementById('sumOfMultiplied').innerHTML = numberArray.map(x => x * 2).reduce((a, b) => a + b, 0);