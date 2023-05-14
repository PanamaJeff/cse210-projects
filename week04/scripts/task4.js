// Declare a new variable to hold information about yourself
let myInfo = {
    name: "Jeffrey Castro",
    photo: "images/me.jpeg",
    favoriteFoods: ["Pizza", "Pasta", "Ice Cream"],
    hobbies: ["Reading", "Swimming", "Coding"],
    placesLived: [
        {
            place: "Arizona",
            length: "14 years"
        },
        {
            place: "Panama",
            length: "18 years"
        }
        // add more places as necessary
    ]
};

// Assign name
document.getElementById('name').textContent = myInfo.name;

// Assign photo source and alt attributes
let photoElement = document.getElementById('photo');
photoElement.src = myInfo.photo;
photoElement.alt = myInfo.name;

// Assign favorite foods
let favoriteFoodsElement = document.getElementById('favorite-foods');
myInfo.favoriteFoods.forEach(food => {
    let li = document.createElement('li');
    li.textContent = food;
    favoriteFoodsElement.appendChild(li);
});

// Assign hobbies
let hobbiesElement = document.getElementById('hobbies');
myInfo.hobbies.forEach(hobby => {
    let li = document.createElement('li');
    li.textContent = hobby;
    hobbiesElement.appendChild(li);
});

// Assign places lived
let placesLivedElement = document.getElementById('places-lived');
myInfo.placesLived.forEach(place => {
    let dt = document.createElement('dt');
    dt.textContent = place.place;

    let dd = document.createElement('dd');
    dd.textContent = place.length;

    placesLivedElement.appendChild(dt);
    placesLivedElement.appendChild(dd);
});