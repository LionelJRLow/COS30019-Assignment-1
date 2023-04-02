# COS30019-Assignment-1
Introduction to AI Assignment 1 - Robot Navigation (Tree Based Search)
 
Robot Navigation is a program where a robot has to transverse an NxM grid to
reach a goal located in a random node within the grid. The robot location will be located on
an empty node and has to traverse one node at a time in order to reach the goal. Within the
grid there will walls generated that the robot cannot pass through, needing it to go around the
walls. For this assignment the size of the grid, location of the agent, goals and the walls were
given beforehand.
<br>
The approach I took for this assignment was to use a search tree based approach in order to
reach the goal. Instead of generating the grid beforehand and filling it up with the walls and
goals and making the robot traverse the pre generated graph, the robot will instead generate
the nodes and the node edges as it is processing and traversing the nodes. The robot will take
in information of its location, the goal location, the width and length of the grid and the list of
walls and from there generate its environment while its traversing until it reaches the goal
