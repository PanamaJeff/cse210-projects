class User {
  constructor(name) {
    this.name = name;
    this.workouts = [];
  }

  addWorkout(workout) {
    if(workout.calories <= 0) {
      console.log('Invalid workout. Calories should be greater than 0');
      return;
    }
    this.workouts.push(workout);
  }

  totalCaloriesThisWeek() {
    let oneWeekAgo = new Date();
    oneWeekAgo.setDate(oneWeekAgo.getDate() - 7);
    return this.workouts
      .filter(workout => new Date(workout.date) >= oneWeekAgo)
      .reduce((total, workout) => total + workout.calories, 0);
  }

  sendData() {
    if(this.workouts.length === 0) {
      console.log('No workout data to send');
      return;
    }
    fetch('YourURL', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this),
    }).then(response => response.json())
      .then(data => console.log('Success:', data))
      .catch((error) => console.error('Error:', error));
  }

  fetchData() {
    fetch('YourURL')
      .then(response => response.json())
      .then(data => {
        this.name = data.name;
        this.workouts = data.workouts;
        console.log('Fetched data successfully');
      })
      .catch((error) => console.error('Error:', error));
  }
}

class Workout {
  constructor(calories, date = new Date()) {
    this.calories = calories;
    this.date = date;
  }
}

let user = new User('John');
user.addWorkout(new Workout(500));
console.log(user.totalCaloriesThisWeek());
user.sendData();
user.fetchData();