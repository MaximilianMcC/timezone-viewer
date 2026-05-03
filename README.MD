# Timezone viewer thing
Shows what time it is for other people, and also has a world ascii map that tracks the suns position.

## Preview thing
This is an example of the output at 9:18am nzst with two people added. You can see the orange sections represent the people tracked, and daylight is represented on the map (nighttime in the uk, day in nz)
![map](https://i.imgur.com/cUqfoqH.png)

---

# Using it
You'll probably wanna rename the file when you download it to something simple like `time.exe`. You can download it from the releases tab [here](https://github.com/MaximilianMcC/timezone-viewer/releases/).

To view the clock then just launch it in your terminal:
```cmd
time
```
To add or remove someone, then use the add and remove args. These have a little cli wizard thingy
```cmd
time add
time remove
```
---

> [!WARNING]  
> Running on Windows Terminal won't update the map, but will instead redraw it underneath (windows terminal issue, not me)