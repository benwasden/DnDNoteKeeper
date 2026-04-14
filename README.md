# Dungeons and Dragons Notekeeper
Welcome to the DnD Note Keeper! This project allows users to create an account, sign in, and have access to all sorts of useful DnD features!
These include:
- Making and advertising campaigns to other users
- Uploading campaign images to enhance campaign advertising
- Creating characters while tracking their class, level, stats, and much more
- In-app multi-dice roll - six different types of dice in one

## Hosted Link
If you'd like to get a feel for how the app looks and functions, here's a link to a hosted instance.
[Hosted Site][https://dnd-note-taker-ffeyc8gxdzc4gfa7.eastus-01.azurewebsites.net/]

## Using the app
- First time you open the app, you'll be shown a logged-out view. To get anywhere, you'll want to make an account. Click "Create Account" on the sidebar.
- Enter a username and password. The app will check if the username is already taken and let you know.
- Once registered, you'll be redirected to the login page. Sign in and you'll be taken to the main dashboard.
- From here, you can access all the functions of the app!
    - "Search for Campaigns" shows you all posted campaigns. Click on one to see details about it.
    - Click "My Campaigns" to see ones you've made, or make one yourself.
    - "My Characters" lists all characters you've created.
        - You can create a new character here as well! Set its name, stats, class, and more!
    - The "Dice Roller" lets you select a die to use and then get rolls from it! You can even see the history of past rolls down below.
- Click "Logout" to log yourself out of the app and keep your data secure!


## Setup Guide
- For local instances, ensure appsettings.json is present
- Set up a database for storing images as well as a sql database
    - Our hosted instance uses Azure for web hosting as well as Azure SQL and Azure Blob.
- Put the connection strings in your appsettings.json (or in the connection string settings of your web host)


## Contributors
- Ben Wasden
- Johnathan Babb
