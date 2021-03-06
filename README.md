# Wumpus Parser

There is gold in a custom dungeon, but you'll need to navigate through it based on memory, senses, and logic in order to get the gold and defeat the vicious wumpus!

## Commands

look: shows information about the current room (as when entering it)

climb, exit, leave: use the stairs to leave the dungeon and end the game

take, grab, get: obtain the gold treasure

shoot [north, south, east, west]: kills the wumpus if it is in that adjacent room

go [north, south, east, west]: moves in that direction

quit: exits the program immediately

You can also use shorthand for directions: n, s, e, w. For that matter, you can even omit 'go' when moving, and simply state the direction or its abbreviation.


### Gameplay

The dungeon is very dark, and it is easy to fall into a pit or right into the clutches of the wumpus itself!

Fortunately, you will feel a breeze in the air if there are pits nearby, and the wumpus has a terrible smell.

You also have a bow with one arrow. The arrow will not travel far, and can only be used once.


### Scoring

leaving the dungeon alive: +1000

killing the wumpus: +1000

getting the gold: +1000

dying: -1000

each action: -1


### Design

Hunt the Wumpus is a classic computer game, with many variants. It is now commonly used for AI education, as it is a pretty easy partially-observable domain to implement. Of course, there's nothing wrong with playing the game itself. In this case, it is sort of a Zork-like text adventure. Note, however, that the command set is so small that I didn't bother with a real parser (the 'parser' in the title just indicates that it's a text adventure). Other expectations of the genre, such as an inventory or complicated geography, are also missing.

Also notably absent in this release is the AI itself, despite being the entire point of the program!