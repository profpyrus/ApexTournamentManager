# Apex Legends Tournament Manager
This is a tool that's supposed to help you manage and integrate your Apex Legends Tournaments into your streams - in the most customizable, powerful way possible!

Currently, the functions that are possible with this program look like the following:

1. [Point Management](#point)
2. [Team Management](#team)
3. [Match Management](#match)
4. [Leaderboard View](#leaderboard)
5. [Settings / Home View](#settings)

## <a name="point"></a>Point Management
You can assign different points to different "achievements" in the tournament, which will be automatically calculated for viewing on the leaderboard. This works by having each element in the point assignment list represent it's assigned value and everything above or below.

See this example, which are the default values:
![image](https://github.com/profpyrus/ApexTournamentManager/assets/116492135/0b367c64-92b2-41f9-9882-45f7c1545be0)
Here, each Team would be assigned 10 points for 1st, and 5 points for 2nd place. Next, the 3rd through 5th placed would be getting 3 points each, as place 6 then gets a different value assigned. So all the placements that are on or below an assigned placement, the get assigned these points, until a lower place is assigned a different value. The full list here would be:
- 1: 10 Points
- 2: 5 Points
- 3-5: 3 Points
- 6-10: 1 Point
- 11 and anything below: 0 Points

The Kills work the same, just the other way around. So 1 Kill or more, than 2 kills or more, etc. Since most people will want to assign one more point per kill though, we made that the default, up to 15 kills. Should a single player get more than 15 Kills (which is very impressive), you can add more Kill-point-assignments by pressing the '+' button.

## <a name="team"></a>Team Management
This page is used to set up all the teams taking part in your tournament, including the players.
![image](https://github.com/profpyrus/ApexTournamentManager/assets/116492135/6c3dafe8-50e8-4028-b59a-083736340894)
At the very left, you have a list of all the teams, where you can add or delete teams by pressing '+' or '-'. Next to that list, you have the option to change the name of the team, and a list of all players, which one can also rename and add or remove.

## <a name="match"></a>Match Management
Here you add all the matches that take place in your tournament.
![image](https://github.com/profpyrus/ApexTournamentManager/assets/116492135/40cbb160-e21b-4bd2-987a-96cc6979da87)
You can add a match using the '+' button, or remove the selected one using the '-' button. For each match, you have a list of all the Teams, for whitch you can each assign a placement, and register how many kills and deaths each player of the selected team got in the match. 

## <a name="leaderboard"></a>Leaderboard View
Finally, the page where all the setup comes together. The Leaderboard View looks like this:
![image](https://github.com/profpyrus/ApexTournamentManager/assets/116492135/574d51ff-f134-4ac1-8967-7893e54e87ee)
On the left are three dropdown menus:
- The topmost one being for selecting whether to take the data for the leaderboard from a specific match, or all of them.
- The second one is for selecting whether to rank the teams, or all of the players seperately.
- Finally the third one is for selecting what to rank by. Depending on your choices before, the options will change. But it will mostly be something like Kills, Points, etc. Additionally, if you selected All Matches, you can choose between sorting by something in Total, meaning all the matches added together, or by the Average of all the matches.

The points for teams means each player gets assigned their points for the kills they got, those get added together for the team, and the points for the placement get added. Similarly, the points for a player result from adding their kill points on top of their teams placement points. 

Lastly there is a Button to send the 'Leaderboard to OBS'. This can only be used when connected to OBS via the Option on the [Home View](#settings)

## <a name="settings"></a>Settings / Home View
Besides the Info text on the right side of this page, there are 2 important parts to this page: The Session Options, and the OBS section.
![image](https://github.com/profpyrus/ApexTournamentManager/assets/116492135/71e444ca-05be-4b5f-ac11-4ee49112bc4a)
### Session
Here you can either Save the current session, save the current session in another file, Open an existing one, or create a new one. FOr each of them (excpet saving) this will open a file dialog to select a location and give a file name. This filename will also be the name of the session displayed in the bottom left corner of the window.
### OBS
Set up your connection to OBS to allow the manager to display your leaderboards on your stream. The options you have here are the following:
- The Connecting-section: Setup your connection by entering the IP address and port number (which in 99% of cases will be the default values here) of your OBS Websocket. (To enter those options, in OBS see Tools > OBS-Websocket).
- The Maintainance-section: Send some test values to all the rank sources in obs or clear them, to see if everything is working as intended.
- The Source setup-section: This section will take away a lot of work from you. Set up a single Text(GDI+) source in an obs scene somewhere (it doesn't matter which), and give it a name (for example, like seen here, 'RANKTEMPLATE'). Now Setup all settings in the way you want them to, most importantly the font. This is because the manager will set up 180 (!!!) sources in your obs in the next step, and you do not want to set the font for each of those seperately. Enter the name
