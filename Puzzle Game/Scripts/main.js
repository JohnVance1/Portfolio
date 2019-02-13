"use strict";
const app = new PIXI.Application(600,600);
document.body.appendChild(app.view);
app.renderer.backgroundColor = 0x000000;

const sceneWidth = app.view.width;
const sceneHeight = app.view.height;

// Make Miscs
let stage;
let player;
let lvl1Player;
let target;
let border;
let borders = [];
let i = 0;
let check;
let paused = true;


// Makes Game Scene
let gameEndScene;
let levelOneScene;
let levelTwoScene;
let startScene;
let loseScene;

// Make Sounds
let gameEndSound, backgroundSound, gameLoseSound;



window.onload = start;

// Starts the game and loads everything
function start(){
    stage = app.stage;

    // Create Scenes
    gameEndScene = new PIXI.Container();
    gameEndScene.visible = false;
    stage.addChild(gameEndScene);

    levelOneScene = new PIXI.Container();
    levelOneScene.visible = false;
    stage.addChild(levelOneScene);

    levelTwoScene = new PIXI.Container();
    levelTwoScene.visible = false;
    stage.addChild(levelTwoScene);

    startScene = new PIXI.Container();
    startScene.visible = false;
    stage.addChild(startScene);

    loseScene = new PIXI.Container();
    loseScene.visible = false;
    stage.addChild(loseScene);

    // Create Sounds
    //http://soundbible.com/1003-Ta-Da.html
    gameEndSound = new Howl({
        src: ['Sounds/gameWon.mp3']
    });

    //https://www.bensound.com/royalty-free-music/track/the-lounge
    backgroundSound = new Howl({
        src: ['Sounds/backgroundSound.mp3']
    });

    //http://soundbible.com/1830-Sad-Trombone.html
    gameLoseSound = new Howl({
        src: ['Sounds/loseSound.mp3']
    });

    createLabelsAndButtons(); 
    
    //lvlOne();
    //lvlTwo();
    startScreen();
    //createLabelsAndButtons();
    //endGame();
    //loseScreen();

    
    app.ticker.add(game);
    

}

// Game Loop
// Checks all collisions and 
// alllows for the player to move
function game()
{
    if (paused) return;
    
    // Calculate "delta time"
    let dt = 1/app.ticker.FPS;
    if (dt > 1/12) dt=1/12;
    
    // Check Keys
    if(keys[keyboard.RIGHT]|| keys[keyboard.D]){
        player.dx = player.speed;
    }else if(keys[keyboard.LEFT]|| keys[keyboard.A]) {
        player.dx = -player.speed;
    }else{
        player.dx = 0;
    }
    
    if(keys[keyboard.DOWN] || keys[keyboard.S]){
        player.dy = player.speed;
    }else if(keys[keyboard.UP]|| keys[keyboard.W]) {
        player.dy = -player.speed;
    }else{
        player.dy = 0;
    }        

    if(keys[keyboard.Q]){
        //gameScene.removeChild(player);
        player.dr -= 2;
    }
    else if(keys[keyboard.E]){
        //gameScene.removeChild(player);
        player.dr += 2;
    }
    else{
        player.dr = 0;
    } 
    //check = false;
    


    // Allows for the player to change their size
    player.update(dt);
    if(player.radius >= 5)
    {
        player.changeR(dt, check);
        //gameScene.addChild(player);
    }
    else
    {
        player.radius = 5;
    }

    let w2 = player.width/2;
    let h2 = player.height/2;
    player.x = clamp(player.x,0+w2,sceneWidth-w2);
    player.y = clamp(player.y,0+h2,sceneHeight-h2);

    //console.log(i);
    //i++;

    // Checks to see if the player is interacting with
    // the target and if they have the same size
    if(rectsIntersect(player,target))
    {
        if(target.height <= player.height && target.width <= player.width)
        {
            //target.x = Math.random() * (sceneWidth);
            //target.y = Math.random() * (sceneHeight);
            
            // Load next level
            changeLevel();
        }
        
    }
    
    //console.log(borders);

    // Checks the collisions of all of the borders
    for(let b of borders)
    {
        if(rectsIntersect(b,player))
        {
            check = true;
            let dt = 1/app.ticker.FPS;
            if (dt > 1/12) dt=1/12;
            player.border();
            player.update(dt, check);  
            player.dr -= 20;            
            player.shrink(dt);   
            //i++;
            

        }   
        check = false;
        
    }

    if(player.height == 10 && player.width == 10)
    {
        changeLevel();

    }

    // For now this section doesn't work as I was 
    // trying to get the music to loop
    if(backgroundSound.isPlaying == false)
    {
        backgroundSound.play();
    }

    if(gameEndScene.visible == true)
    {
        backgroundSound.stop();
    }

}

// Allows for the level to change when 
// the player completes one
function changeLevel()
{
    if(startScene.visible == true)
    {
        paused = false;        
        lvlOne();
    }

    else if(player.height <= 10 && player.width <= 10)
    {
        levelOneScene.removeChild(player);
        levelTwoScene.removeChild(player);        
        borders.length = 0;        
        paused = false;      
        loseScreen();  
        

    }

    else if(levelOneScene.visible == true)
    {
        levelOneScene.removeChild(player);
        borders.length = 0;
        paused = false;
        lvlTwo();
    }

    else if(levelTwoScene.visible == true)
    {
        levelTwoScene.removeChild(player);        
        borders.length = 0;        
        paused = false;        
        endGame();
    }

    


}

// Makes the start screen visible and 
// plays the background music
function startScreen()
{
    levelOneScene.visible = false;
    startScene.visible = true;
    gameEndScene.visible = false;
    levelTwoScene.visible = false; 
    loseScene.visible = false;
    backgroundSound.play();
    //backgroundSound.loop(1);

}

function loseScreen()
{
    levelOneScene.visible = false;
    startScene.visible = false;
    gameEndScene.visible = false;
    levelTwoScene.visible = false; 
    loseScene.visible = true;
    gameLoseSound.play();

}

// Creates all ofo the labels that are on the screen
function createLabelsAndButtons()
{
    //app.renderer.backgroundColor = 0x555555;
    
    let buttonStyle1 = new PIXI.TextStyle(
        {
            fill: 0xFFFFFF,
            fontSize: 48,
            fontFamily: "Montserrat"
        }
    );

    let buttonStyle2 = new PIXI.TextStyle(
        {
            fill: 0xFFFF00,
            fontSize: 48,
            fontFamily: "Montserrat"
        }
    );

    let buttonStyle3 = new PIXI.TextStyle(
        {
            fill: 0xFF0000,
            fontSize: 48,
            fontFamily: "Montserrat"
        }
    );

    let startLabel1 = new PIXI.Text("Puzzle Game");
    startLabel1.style = new PIXI.TextStyle({
        fill: 0x000000,
        fontSize: 72,
        fontFamily: 'Montserrat',
        stroke: 0xFFFFFF,
        strokeThickness: 4
    });
    startLabel1.x = 70;
    startLabel1.y = 120;
    startScene.addChild(startLabel1);

    let instructionsStyle = new PIXI.TextStyle({
        fill: 0xFFFFFF,
        fontSize: 20,
        fontFamily: 'Montserrat',
        
    });

    let instructions = new PIXI.Text("Move with the\narrow keys or WASD.");
    instructions.style = instructionsStyle;
    instructions.x = 100;
    instructions.y = 70;
    levelOneScene.addChild(instructions);

    let increase = new PIXI.Text("Decrease or increase\nyour size with Q and E.");
    increase.style = instructionsStyle;
    increase.x = 300;
    increase.y = 200;
    levelOneScene.addChild(increase);

    let size = new PIXI.Text("Match your size\nwith the red\ntarget to finish the level!");
    size.style = instructionsStyle;
    size.x = 30;
    size.y = 480;
    levelOneScene.addChild(size);

    let lose = new PIXI.Text("Get too small\nand you lose!");
    lose.style = instructionsStyle;
    lose.x = 50;
    lose.y = 300;
    levelOneScene.addChild(lose);

    let startButton = new PIXI.Text("Start!");
    startButton.style = buttonStyle1;
    startButton.x = 230;
    startButton.y = sceneHeight - 200;
    startButton.interactive = true;
    startButton.buttonMode = true;
    startButton.on("pointerup",changeLevel); 
    startButton.on('pointerover',e=>e.target.alpha = 0.7); 
    startButton.on('pointerout',e=>e.currentTarget.alpha = 1.0);
    startScene.addChild(startButton);
        
    let textStyle = new PIXI.TextStyle({
        fill: 0xFFFFFF,
        fontSize: 18,
        fontFamily: "Montserrat",
        stroke: 0xFF0000,
        strokeThickness: 4

    });

    let loseScreenText = new PIXI.Text("You Lost!");
    textStyle = new PIXI.TextStyle({
        fill: 0xFF0000,
        fontSize: 64,
        fontFamily: "Montserrat",
        stroke: 0xAA0000,
        strokeThickness: 6
    });
    loseScreenText.style = textStyle;
    loseScreenText.x = 145;
    loseScreenText.y = sceneHeight/2 - 160;
    loseScene.addChild(loseScreenText);

    
    let winScreenText = new PIXI.Text("You Won!");
    textStyle = new PIXI.TextStyle({
        fill: 0xFFFF00,
        fontSize: 64,
        fontFamily: "Montserrat",
        stroke: 0xAAAA00,
        strokeThickness: 6
    });
    winScreenText.style = textStyle;
    winScreenText.x = 145;
    winScreenText.y = sceneHeight/2 - 160;
    gameEndScene.addChild(winScreenText);

    // 3B - make "play again?" button
    let playAgainButton = new PIXI.Text("Play Again?");
    playAgainButton.style = buttonStyle2;
    playAgainButton.x = 160;
    playAgainButton.y = sceneHeight - 150;
    playAgainButton.interactive = true;
    playAgainButton.buttonMode = true;
    playAgainButton.on("pointerup",startScreen); // startGame is a function reference
    playAgainButton.on('pointerover',e=>e.target.alpha = 0.7); // concise arrow function with no brackets
    playAgainButton.on('pointerout',e=>e.currentTarget.alpha = 1.0); // ditto
    gameEndScene.addChild(playAgainButton);

    let playAgainButton2 = new PIXI.Text("Play Again?");
    playAgainButton2.style = buttonStyle3;
    playAgainButton2.x = 160;
    playAgainButton2.y = sceneHeight - 150;
    playAgainButton2.interactive = true;
    playAgainButton2.buttonMode = true;
    playAgainButton2.on("pointerup",startScreen); // startGame is a function reference
    playAgainButton2.on('pointerover',e=>e.target.alpha = 0.7); // concise arrow function with no brackets
    playAgainButton2.on('pointerout',e=>e.currentTarget.alpha = 1.0); // ditto
    loseScene.addChild(playAgainButton2);

}

// Allows for the ending screen to be shown
function endGame()
{
    levelOneScene.visible = false;
    startScene.visible = false;
    gameEndScene.visible = true;
    levelTwoScene.visible = false;
    loseScene.visible = false;    
    gameEndSound.play();
    //app.renderer.backgroundColor = 0x555555;
    

}

// Creates the first level
function lvlOne(){
    levelOneScene.visible = true;
    startScene.visible = false;
    gameEndScene.visible = false;
    levelTwoScene.visible = false;
    loseScene.visible = false;    
    lvlOneMap();

}

// Creates the second level and for now the last level
function lvlTwo(){
    levelOneScene.visible = false;
    levelTwoScene.visible = true;    
    startScene.visible = false;
    gameEndScene.visible = false;
    loseScene.visible = false;    
    lvlTwoMap();
}

// Creates the first levels map
function lvlOneMap()
{   
    //app.renderer.backgroundColor = 0x000000;
    
    target = new Target(20,0xFF0000,100,500,500);
    target.x = 525;
    target.y = 475;
    levelOneScene.addChild(target);

    player = new Player(20,0x00FF00,100,100,300);
    player.x = 20;
    player.y = 20;
    player.radius = 20;
    //gameScene.addChild(player);
    levelOneScene.addChild(player);

    border = new Border(200, 2, 0xFFFFFF, 240, 500);
    borders.push(border);
    levelOneScene.addChild(border);

    border = new Border(230, 2, 0xFFFFFF, 40, 200);
    borders.push(border);
    levelOneScene.addChild(border);

    border = new Border(2, 200, 0xFFFFFF, 40, 0);
    borders.push(border);
    levelOneScene.addChild(border);

    border = new Border(240, 2, 0xFFFFFF, 0, 250);
    borders.push(border);
    levelOneScene.addChild(border);

    border = new Border(2, 250, 0xFFFFFF, 240, 250);
    borders.push(border);
    levelOneScene.addChild(border);

    border = new Border(2, 250, 0xFFFFFF, 270, 200);
    borders.push(border);
    levelOneScene.addChild(border);

    border = new Border(170, 2, 0xFFFFFF, 270, 450);
    borders.push(border);
    levelOneScene.addChild(border);

    border = new Border(2, 100, 0xFFFFFF, 440, 352);
    borders.push(border);
    levelOneScene.addChild(border);
    
    border = new Border(2, 100, 0xFFFFFF, 440, 500);
    borders.push(border);
    levelOneScene.addChild(border);

    border = new Border(160, 2, 0xFFFFFF, 440, 352);
    borders.push(border);
    levelOneScene.addChild(border);

}

// Creates the second levels map
//#region lvlTwoMap()
function lvlTwoMap()
{
    target = new Target(20,0xFF0000,100,500,500, 15, 15);
    target.height = 80;
    target.width = 80;
    target.x = 40;
    target.y = 560;
    levelTwoScene.addChild(target);

    player = new Player(20,0x00FF00,100,100,300);
    player.x = 20;
    player.y = 20;
    player.radius = 20;
    //gameScene.addChild(player);
    levelTwoScene.addChild(player);
    

    border = new Border(2, 300, 0xFFFFFF, 45, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(400, 2, 0xFFFFFF, 0, 520);
    borders.push(border);
    levelTwoScene.addChild(border);
    
    border = new Border(200, 2, 0xFFFFFF, 40, 330);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(400, 2, 0xFFFFFF, 40, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 120, 0xFFFFFF, 40, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(90, 2, 0xFFFFFF, 40, 480);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(90, 2, 0xFFFFFF, 70, 420);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 290, 0xFFFFFF, 80, 40);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 300, 0xFFFFFF, 120, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(100, 2, 0xFFFFFF, 160, 100);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 160, 0xFFFFFF, 160, 140);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 60, 0xFFFFFF, 160, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 60, 0xFFFFFF, 190, 40);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 60, 0xFFFFFF, 220, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(100, 2, 0xFFFFFF, 160, 300);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(100, 2, 0xFFFFFF, 160, 140);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 162, 0xFFFFFF, 260, 140);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 60, 0xFFFFFF, 100, 545);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(300, 2, 0xFFFFFF, 100, 545);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(300, 2, 0xFFFFFF, 130, 570);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 27, 0xFFFFFF, 250, 545);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(100, 2, 0xFFFFFF, 470, 570);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 50, 0xFFFFFF, 430, 522);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 300, 0xFFFFFF, 470, 270);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 200, 0xFFFFFF, 570, 340);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(35, 2, 0xFFFFFF, 570 , 520);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(50, 2, 0xFFFFFF, 472, 500);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(50, 2, 0xFFFFFF, 522, 540);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(50, 2, 0xFFFFFF, 522, 470);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(50, 2, 0xFFFFFF, 472, 430);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(50, 2, 0xFFFFFF, 522, 400);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(50, 2, 0xFFFFFF, 472, 370);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(50, 2, 0xFFFFFF, 522, 340);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(100, 2, 0xFFFFFF, 472, 310);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 100, 0xFFFFFF, 400, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 400, 492);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(70, 2, 0xFFFFFF, 400, 490);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 70, 0xFFFFFF, 190, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 100, 0xFFFFFF, 220, 420);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 110, 0xFFFFFF, 250, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 120, 0xFFFFFF, 280, 400);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 130, 0xFFFFFF, 310, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 130, 0xFFFFFF, 340, 390);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 130, 0xFFFFFF, 370, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 100, 0xFFFFFF, 160, 420);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 60, 0xFFFFFF, 190, 460);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 70, 390);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 100, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 130, 390);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 15, 0xFFFFFF, 160, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 15, 0xFFFFFF, 160, 405);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 60, 0xFFFFFF, 440, 360);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 40, 0xFFFFFF, 440, 450);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 128, 450);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 100, 420);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 70, 450);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 100, 0xFFFFFF, 290, 260);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 230, 0xFFFFFF, 290, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 200, 0xFFFFFF, 320, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 170, 0xFFFFFF, 350, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 140, 0xFFFFFF, 380, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 110, 0xFFFFFF, 410, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 80, 0xFFFFFF, 440, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 50, 0xFFFFFF, 470, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 130, 0xFFFFFF, 320, 230);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 160, 0xFFFFFF, 350, 200);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(200, 2, 0xFFFFFF, 380, 170);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 160, 0xFFFFFF, 380, 200);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 130, 0xFFFFFF, 410, 230);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 60, 0xFFFFFF, 410, 140);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 120, 0xFFFFFF, 440, 110);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 160, 0xFFFFFF, 470, 80);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 100, 0xFFFFFF, 440, 260);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 210, 0xFFFFFF, 500, 60);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 10, 0xFFFFFF, 500, 300);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 210, 0xFFFFFF, 530, 30);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 250, 0xFFFFFF, 560, 60);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 40, 0xFFFFFF, 530, 270);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 500, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 560, 0);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(10, 2, 0xFFFFFF, 590, 80);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(10, 2, 0xFFFFFF, 560, 110);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(10, 2, 0xFFFFFF, 590, 140);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(10, 2, 0xFFFFFF, 590, 200);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(10, 2, 0xFFFFFF, 560, 230);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(10, 2, 0xFFFFFF, 590, 260);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(10, 2, 0xFFFFFF, 560, 290);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(20, 2, 0xFFFFFF, 470, 340);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(20, 2, 0xFFFFFF, 470, 400);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(20, 2, 0xFFFFFF, 470, 470);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(20, 2, 0xFFFFFF, 470, 540);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(20, 2, 0xFFFFFF, 550, 370);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(20, 2, 0xFFFFFF, 550, 430);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(20, 2, 0xFFFFFF, 550, 500);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 72, 0xFFFFFF, 250, 30);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 40, 0xFFFFFF, 230, 100);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(2, 30, 0xFFFFFF, 200, 300);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(30, 2, 0xFFFFFF, 260, 190);
    borders.push(border);
    levelTwoScene.addChild(border);

    border = new Border(30, 2, 0xFFFFFF, 260, 280);
    borders.push(border);
    levelTwoScene.addChild(border);
}
//#endregion

// Allows for collisions to happen
function rectsIntersect(a,b){
    let ab = a.getBounds();
    let bb = b.getBounds();
    return ab.x + ab.width > bb.x && ab.x < bb.x + bb.width && ab.y + ab.height > bb.y && ab.y < bb.y + bb.height;
}

// Makes it so the player can't leave the scene
function clamp(val, min, max){
    return val < min ? min : (val > max ? max : val);
}


