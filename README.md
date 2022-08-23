# ROR2-Mod

#Hopoo Game's stance on the writing, sharing and development of mods for their game.
https://store.steampowered.com/news/app/632360/view/5318144626921363501

>It's no secret that Risk of Rain 2 has fostered an absolutely awesome modding community. While we have no direct mod support, our intent is to avoid breaking mods with >our updates, and to not do any changes that would hamper the community. Maybe you'll see us popping into the modding community in the future!

>If you're interested in Risk of Rain 2 modding, check out the Thunderstore to see the awesome content people have already been making! As always when modding ANY game, >please download and install mods at your own risk! We can't check every mod that gets uploaded or guarantee that they are safe and won't break your save files.

>Check it out here! https://thunderstore.io/


Risk of rain 2 mod, written to practice unity skills in a different form as well as other security related skills

Due to the nature of how the script accociated with adding extra functionality is added into the game after runtime, I have yet to find a way to allow
the use of external scripts and classes to structure the file in a more readable and proper way. This is something I am still trying to figure out so I can 
clean up the code 

TODO:
  Rewrite the OnGUI method to make use of Unity buttons and handle those button click events to trigger the functionality instead of key presses
  
  Clean up the code by extracting functions and other code into it's respective classes
  
  Make a better written 'flying' system, instead of calling the main player game objects playerMotor (RigidBody) to force a jump to occur regardless of isGrounded,
   to allow free flight more like a free cam than just jumping rapidly to simulate flight
