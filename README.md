# ROR2-Mod
Risk of rain 2 mod, written to practice unity skills in a different form as well as other security related skills

Due to the nature of how the script accociated with adding extra functionality is added into the game after runtime, I have yet to find a way to allow
the use of external scripts and classes to structure the file in a more readable and proper way. This is something I am still trying to figure out so I can 
clean up the code 

TODO:
  Rewrite the OnGUI method to make use of Unity buttons and handle those button click events to trigger the functionality instead of key presses
  
  Clean up the code by extracting functions and other code into it's respective classes
  
  Make a better written 'flying' system, instead of calling the main player game objects playerMotor (RigidBody) to force a jump to occur regardless of isGrounded,
   to allow free flight more like a free cam than just jumping rapidly to simulate flight
