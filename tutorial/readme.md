# Little Bot

## Starting the Project

 1. Open Unity
 2. File > New Project ...
 3. Select the 2D option
 4. Name the project "little-robot"
 5. Click Create

## Setting up the Scene

### Creating the Background
 1. Assets > Import New Asset …
 2. Find saved background image, click “Import”

Hierarchy Area > Right Click > 2D Object > Sprite

Inspector Area > Click on the **New Sprite** GameObject
1.  Rename to `Background`
2.  On the **Sprite** field, click on the circle with the dot in the middle
3.  Double click on the image for the background
    
### Creating the Player
 1. Assets > Import New Asset …
 2. Find the saved robot image, and click “Import”

Hierarchy Area > Right Click > 2D Object > Sprite

Inspector Area > Click on the **New Sprite** GameObject
1.  Rename to `Player`
2.  On the Sprite field, click on the circle with the dot in the middle
3.  Double click on the image for the robot

The robot may appear behind the background. 
In the Inspector Area for the **Player** GameObject, set the **Order in Layer** to 1.

It’s also a good habit to make sure everything is positioned at 0, 0, 0 to begin with - for both the GameObjects **Player** and **Background**, go to their **Transform** component in the Inspector Area and click the three dots at the top > Reset.

### Moving the Player

To move the Player, we'll need to do some programming!
We will be making a script and checking for button presses.

Assets Area > Create > C# Script
Name the script `Player`
Double click the file to open it in a text editor

There are two empty functions already prepared:
`Start()` Is automatically called by Unity when the script is loaded
`Update()` is automatically called by Unity once per frame - this means about 30 times a second or more, which is a lot!

To make the Playermove, the script should read in what horizontal or vertical controls the player presses.
This can be read in by using `Input.GetAxis()`.
`Input.GetAxis()` reads in either the WASD keys, or a controller’s joystick button.

![enter image description here](test)

We will store `Input.GetAxis()` values in two variables, `horizontal` and `vertical`.

Inside the `Update()` function- inside the curly brackets `{}`, type in these two lines:

    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    
![enter image description here](test)

To find out what these variables will have in them during the game, we'll print them out in the Unity console.
After the two lines we wrote above, write in these two lines:

    Debug.Log(horizontal);
    Debug.Log(vertical);

The code should now look like this:

![enter image description here](test)

This means every frame (~30 times a second), it will read in the horizontal and vertical inputs, and print them out.

Save the Script and go back to Unity.

We’ll now need to attach the Player Script to the robot, because the robotis what we want to move.

Hierarchy Area > Click the **Player** GameObject
Inspector Area > Click the **Add Component** button
In the search bar that comes up, type `Player` to find our Player Script, and click it
You will see a **Player (Script)** component has been added.

![enter image description here](test)

Now we can start the game that we have so far
To *start* the game, click on the **Play** button at the top
To *stop* the game, click on the **Play** button at the top again

Start the game. You will see the **Game** tab.
In the bottom area is the **Console**, where you should see a lot of `0`s being printed out. This is from the `Debug.Log()` messages, and it's printing out a `0` for every frame because we're not pressing the movement keys.

Try pressing any of the `W`, `A`, `S` or `D` keys on the keyboard, and you will see the numbers that print change.

![enter image description here](test)
  
Stop the game.

To make the robot move with these values, we’ll use some `Vector3` code.

Open the **Player** Script again
Remove the two `Debug.Log()` lines
To get the current position of the Player, we will use `transform.position`.
To get the target position for the Player, we will us addition, adding our current position with the direction the player presses.

Under where we set the `horizontal` and `vertical` variables, set the current target:

    Vector3 target = transform.position + new Vector3(horizontal, vertical);

We will then move the Player's position by using `MoveTowards()` below that line.

    transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);

Save the script, go back to Unity, and start the game.

The robot should now move when you press one of the `W`, `A`, `S` or `D` keys on the keyboard.
 
He’s a bit slow though, so we can speed him up by multiplying the last value by our desired speed:

Above where we called `MoveTowards()`, set a `speed` variable:

    float speed = 3f;

Now apply the speed to the movement by editing our `MoveTowards()` line:

    transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

Save the script, go back to Unity, and restart the game.
The robot should now be faster when you move.

![enter image description here](test)

You’ll notice that even when you walk to the right, the robot still faces to the left.
To make the robot turn when walking right, we’ll use `Scale`.

If the horizontal input is more than `0`, it means the player is moving right and it should keep facing right.
If the horizontal input is less than `0`, it means the player is moving left and it should flip the robot.

    if (horizontal > 0)
    {
		transform.localScale = new Vector3(1, 1);
    }
    
    if (horizontal < 0)
    {
	    transform.localScale = new Vector3(-1, 1);
    }


Save the script, go back to Unity, and restart the game.
The robot will now change directions when he needs to.

![enter image description here](test)

### Making the Player Stay Within the Walls

It’s great that the robot can move, but right now the player is able to move him off the board.
We want him to stay in the square.
To do this, we will use **Colliders**.

Colliders are very useful in being walls, as well as detecting if things like enemies are near us or have hit us.

To add the Collider component to the robot:
Hierarchy Area > Click on the **Player** GameObject
Inspector Area > **Add Component** button > Search for and click “Box Collider 2D”.

Make sure it’s **Box Collider 2D**, and not **Box Collider**.

Once it’s added, click on the button next to **Edit Collider**.

Change the size of the collider in the scene window so that it’s about the same size as the robot picture.

![enter image description here](test)

If the camera icon is covering the robot, you can click the **Gizmo** button at the top to hide the icon.

Now that we have a collider on the robot, we’ll need colliders to act as walls on our board.
Hierarchy Area > Click on the **Background** GameObject
Inspector Area > **Add Component** button > Search for and click “Box Collider 2D”.

Once it’s added, click on the button next to **Edit Collider**.

Change the size of the collider in the scene window so that it’s about the same size as the top black wall.

![enter image description here](test)

Repeat this three more times to make four walls.

![enter image description here](test)
  
**Colliders** need another component called **RigidBody2D** in order to work correctly.

Click on the **Player** GameObject
**Add Component** > **Rigidbody2D**

On the component settings:
Set **Gravity Scale** to `0`, because we don’t want gravity in our game.
Under **Constraints**, check the **Freeze Rotation** box because we don’t want our Robot falling over.

Save the Scene, and restart the game
The robot will no longer be able to go through walls.

![enter image description here](test)

### Creating the Enemy

Now that we have our Player movement set up, we’ll introduce some enemie.
Download one enemy image from here
Project Area > Right Click > **Import New Asset …** > Find the enemy image that you saved
Hierarchy Area > Right Click > Create Empty

Inspector Area:
 1. Rename to `Enemy`
 2. Click on the three dots on the **Transform** component > **Reset**
 3. **Add Component** > **Sprite Renderer**
 4. Click the circle next to **None (Sprite)**, and select the enemy image
 5. **Add Component** > **Box Collider 2D**
 6. **Edit Collider** to adjust the collider box to fit the enemy image in the scene
 
![enter image description here](test)

### Moving the Enemy

We’ll get the enemy to follow the Player next, again using programming in a Script.
1. Project Area > Right Click > Create > **C# Script**
2. Name the new script `Enemy`

To attach the script to the Enemy GameObject:
Hierarchy > Click **Enemy** > Inspector > **Add Component** > Search `Enemy` and click it

![enter image description here](test)

The Enemy GameObject should now have the script attached.

Open the enemy script.

The movement code for the enemy will be very similar to the Player's.
We want the Enemy to see where the Player is, and move towards it.

At the top of the Enemy class, create a player variable with a GameObject type:

    GameObject player;

In the `Start()` function, we’ll get Unity to find the Player object and store it in the Enemy’s player variable.

    player = GameObject.Find("Player");

Now in the `Update()` function, we’ll set the target to the player’s position,
and then set the Enemy’s position to move towards the target.

    Vector3 target = player.transform.position;
    transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);

Save the script, go back to Unity, and restart the game.

When the Player moves, the Enemy will follow.

![enter image description here](test)

### Throwing Balls
  
Now we’ll get the Playerto throw balls at the enemy if you left click on the mouse.
To do this, we’ll make a ball, and each time the Player throws, we’ll make a copy of the ball and throw it towards where the Player clicked.

Hierarchy Area > Right Click > Create New > 2D Object > **Sprite**
Inspector Area of the new GameObject:
1. Rename to `Ball`
2. **Transform** component’s three dots at the top > **Reset**
3. Click the circle on the right of **Sprite**
4. Select **Knob** - it’s a circle that we’ll use as the ball
5. Set **Order in Layer** to `5` so that it appears at the front

![enter image description here](test)

You can change the color of the ball by clicking on the **Color** field and picking a color inside the wheel and the square.

Now to make the Player throw the ball:
Open the Player script
Prepare to store the Ball GameObject by declaring the ball at the top of the Player class

    GameObject ball;

When the script starts in the `Start()` function, locate the Ball GameObject and store it. We also want to deactivate the copy of the ball so it doesn’t appear on the board.

    ball = GameObject.Find("Ball");
    ball.SetActive(false);

In the empty `Update()` method, put in a check to see if the mouse has been clicked -

    if (Input.GetMouseButtonDown(0))
	{
	}

For the ball-throwing, we’re going to need to know the location of the click:

    Vector3 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

Then we need to know the direction we’re going to throw the ball, which is the difference between the location of the Player and the location of the click.

    Vector3 direction = target - transform.position;

With those in mind, we can create a copy of the Ball GameObject, and then activate it again

    var clone = Instantiate(ball, transform.position, transform.rotation);
    clone.SetActive(true);

Now go back to the game,  restart it, and click while moving.

The ball doesn’t move yet - but the game does create copies of the ball where the Player is when you click! 

To give the Ball a push so it looks like it’s throwing, we’ll need to add a **Rigidbody2D** to the Ball.

Hierarchy Area > Click **Ball** 
Inspector Area > **Add New Component** > **RigidBody 2D**
Set **Gravity Scale** to `0`

Go to the Player Script.

Under the code we wrote before, we’ll put in the code to give the Ball a push using the RigidBody2D on the ball.

    clone.GetComponent<Rigidbody2D>().AddForce(direction * 100);

Your script should now look like this

![enter image description here](test)

Save the script and restart the game.
Now when you click, it should throw the ball in the direction you clicked.

### Enemy Taking Damage

Now we want the Enemy to take damage if the Ball hits it.
We already added a collider to the Enemy, but we are yet to do that for the Ball.

Hierarchy Area > Click the **Ball** GameObject > **Add Component** > **Box Collider 2D**
Check the **Is Trigger** box since we don’t want the ball bouncing off the Enemy.

Open the Enemy script
When something first collides with the Enemy, we’ll print out the name of what collided.

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	Debug.Log(collision.name);
    }

Save, go back to the game, and throw Balls at the Enemy.

You should see two kinds of messages, one that prints out **Ball (Clone)** when the Ball hits the Enemy, and **Player** if the Player touches the Enemy.

Inside the `OnTriggerEnter2D()` function, make sure the collision is with the Ball.

    Debug.Log(collision.name);
    if (collision.name == "Ball(Clone)")
    {
    
    }

Inside this check, we’ll **Destroy** both the Ball and the Enemy when they collide.

    Destroy(collision.gameObject);
    Destroy(gameObject);

![enter image description here](test)

Save the script, go back to the game, and Play. 
Walk away from the Enemy a bit, and then throw a Ball at the Enemy. 
Both the Ball and the Enemy should disappear.

The game might be too easy like this, so we’ll make the enemies die after 3 hits.

To do this, in the Enemy script create a health value at the top of the `Enemy` class:

    int health = 3;

Now inside the OnTrigger function that we have, every time it collides with the ball, it should subtract 1 from Enemy’s health

    health = health - 1;

After that if the health is 0 or below, it should Destroy the Enemy:

    if (health >= 0)
    {
    	Destroy(gameObject);
    }

Your code should now look like this:

![enter image description here](test)

Save the script and restart the game. 
Now it should take 3 throws for the enemy to disappear.

### Player Taking Damage

Now that the enemy has health and collision with danger, we should do that for the Player too.
The Player should have health, and take damage when the enemy touches the Player.

Open the Player script and add the health at the top of the Player class

    int health = 5;

Similar to the Enemy and the Ball, add a `OnTriggerEnter2D()` function to the Player script.
The Script should check if the collided object is an Enemy:

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.name == "Enemy")
    	{
    		health = health - 1;
    	}
    }

When the Player loses all health, we’ll restart the game. To do this, we’ll use Unity’s **SceneManager** to reset the Scene.

To tell Unity that we want to use SceneManager, put this at the very top of our Player script:

    using UnityEngine.SceneManagement;

Now back in the collision script after we subtract a health on hit, put in a check for 0 health and reload the scene

    if (health <= 0)
    {
    	SceneManager.LoadScene("SampleScene");
    }

Now we have health data in the code, but we want to show the Player what their health is in the game. We’ll show some text on the screen for this.

Hierarchy Area > Right Click > UI > **Text**

This creates a few objects in the Scene, including one named **Canvas** and **Text**.

Try *double-clicking* on the **Canvas** GameObject- it will zoom way out, very far away from your game, and show a rectangle.
Now try double-clicking on the **Player** GameObject - it will zoom all the way in.

You might think it’s weird how the Canvas is so big, but don’t worry - it’s just how Unity does UI (User Interface - things like Text and Buttons). It will all end up looking normal in the end.

Now we’ll change the Text:
Hierarchy Area > Click on the Text GameObject
1. Name the Object “HealthText”
2. Change **Color** to white
3. Set the **Text** to “Health:”
4. Set the **Font Size** to 24
5. In the **RectTransform** component, click on the grid with red and black lines.
This will pop up some **Anchor Presets**.
We want the text to appear in the bottom left, so hold the **Alt** key, and click on the Anchor Preset that would set it to the bottom right:

![enter image description here](test)

Save the Scene and start the game.
The text for “Health:” should now be showing in the bottom left.
Stop the game.

Now we will make the Player script tell the health text what number it should show.
In the Player script, let Unity know we want to use the UI code at the very top:

    using UnityEngine.UI;

At the top of the Player class, add a variable for Text for our health text.

    Text healthText;

Now in the Start function we want to put the HealthText into this variable.
Do this by finding the HealthText GameObject, and getting the **Text** component from it

    healthText = GameObject.Find("HealthText").GetComponent<Text>();

Right after that, we’ll set the text to the current health:

    healthText.text = "Health :" + health;

Now each time we subtract the Player’s health, we want to update the text again:

    healthText.text = "Health :" + health;
    
Your code should now look like this:
![enter image description here](test)

## Lots of Enemies

At the moment the Enemy starts in the same place as the Player, which isn’t very fair because it makes the Player take damage at the beginning of the game!

Move the **Enemy** GameObject to the top left of the board.

Now when you Play the game, the Enemy should move towards the Player, and each time the Enemy touches the Player, the Player should lose 1 health. When the health reaches 0, it should reload the Scene and start again.

We’re nearly there!

Now to make the game more difficult, let’s add lots of enemies. We’ll do this by copying and pasting the Enemy object.

In the Scene Area, click on the Enemy and copy it by pressing [Copy shortcut]. 
Paste the copied enemy by pressing [Paste SHortcut]. 
This will create a copy of the Enemy. 
You can move it around where you wish on or off the board.

To make the game more difficult over time, you can put lots of enemies far away, like so:
![enter image description here](test)

Last Step!

In the Hierarchy Area, you’ll see the Enemy copies have numbers on the end of their names.

![enter image description here](test)

This is an issue because the Player script only checks for the name “Enemy”, and not something like “Enemy(2)”.

We can get around this by editing the Player script so that the trigger script checks for colliders with names that have the word “Enemy” in it, instead of just “Enemy”.

In the Player Script where the name check is name, change

    if (collision.name == "Enemy")

To:

    if (collision.name.Contains("Enemy"))

Now the game is complete!

## Building the Game Program

Now to make it so that you can put the game on a USB or on the internet, we’ll need to **Build** the game.

All game start in full screen, so we’ll set it to windowed mode.

1. File > Build Settings … > Player Settings button >
2. Expand **Resolution and Presentation** section by clicking on the triangle next to it
3. Set **Fullscreen Mode** to `Windowed`
4. Set **Default Screen Width** to `800`
5. Set **Default Screen Height** to `800`
6. Tick the box for **Resizable Window**

![enter image description here](test)

Close Player Settings by clicking the `x` at the top of that window

Back in the Build Settings window, click on the **Build** button.

It will pop up a file explorer window - go to the Desktop and create a new folder called `game`. Click on the folder to select it, and click Select.

When it finishes building, it should automatically open the folder that it built to.

Inside the folder that you built in, double click on the game, little-bot.exe























