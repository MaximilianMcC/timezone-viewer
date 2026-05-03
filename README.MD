# Timezone viewer thing
Shows what time it is for other people, and also has a world ascii map that tracks the suns position.

## Preview thing
This is an example of the output at 9:18am nzst with two people added. You can see the orange sections represent the people tracked, and daylight is represented on the map (nighttime in the uk, day in nz)
![map](https://i.imgur.com/cUqfoqH.png)

---

# Using it
You'll probably wanna rename the file when you download it to something simple like `timezones.exe`. You can download it from the releases tab [here](https://github.com/MaximilianMcC/timezone-viewer/releases/).

To view the clock then just launch it in your terminal:
```cmd
timezones
```
To add or remove someone, then use the add and remove args. These have a little cli wizard thingy
```cmd
timezones add
timezones remove
```
If you'd like to manually edit the JSON file which contains the users, you can run this to print the file location, but I'd rather you just use `add` and `remove` ngl
```cmd
timezones json
```
You can edit/upload a custom map file, but it is kinda fiddly to get looking good since you've gotta get the size right and stuff yk. I recommend using the braille characters like I did but you can do whatever
```cmd
timezones map
```

## Uninstalling
To get rid of it then you've just gotta delete the exe file. But before you do that then run either `map` or `json` to get the path of the json and map files so you can get rid of that directory. They take up like maybe 10 kb max so its chill if you forget though
---

> [!WARNING]  
> Running on Windows Terminal won't update the map, but will instead redraw it underneath (windows terminal issue, not me) To use it with windows terminal you need to clear the screen first: `cls && timezones` not my fault trust