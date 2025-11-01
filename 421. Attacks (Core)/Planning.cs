/*
 * 
 * What we need:
 *  - User to be able to choose their target for an attack
 *      - A list of targets to be presented, accessed via the Party class, which only Game has access to because that's where the object is created
 *      - User types ...something... and a target is chosen
 *      - Action runs, using the chosen target
 * 
 * 1. Player asked to choose an action
 *      - switch statement with list of actions
 * 2. Player chooses an action
 * 3. Given choice of targets
 *      - foreach loop that accesses Party.MonsterParty
 *      - prints each choice to the console
 * 4. Choose a target
 *      - switch statement with list of choices
 * 5. Action is executed
 * 
 */