# Custom Card Texture Loader for Slay the Spire 2

Loads custom card textures in the game Slay the Spire 2  

## Usage

Put your card textures in `path/to/Slay the Spire 2/CustomCardTextures`.  
They should be in the format of `CustomCardTextures/{CharacterName}/{CardName}.png` (currently only files ending in .png are supported)  
e.g. `Slay the Spire 2/CustomCardTextures/silent/bouncing_flask.png`  
Status cards go in CustomCardTextures/status/name.png, e,g, CustomCardTextures/status/burn.png thanks to null0x1337 for pointing it out

Images should ideally be in a 25:19 or 1.3157894737 aspect ratio. e.g. 250x190 for a small image or 1000x760 for a large image.

## Texture Pack Creators

If you wish to create and distribute a texture pack, textures will also be loaded from the mods directory in `path/to/Slay the Spire 2/mods/CustomCardTextures`.  
This allows mod managers such as Vortex to handle any conflicts between packs and allow users to decide which packs should have the highest priority.  
Textures loaded from images in the base game folder have a higher priority than those in the mods folder.

To distribute a pack, ensure the zip file contents look like the following so that Vortex can install it properly:
```
📦zip root  
 ┣ 📂mods  
 ┃ ┗ 📜CustomCardTextures  
 ┃   ┗ 📜silent  
 ┃     ┗ 🖻bouncing_flask.png  
 ```

 ## Installation

 ### Vortex

 1. Go to the [NexusMods listing for the mod](https://www.nexusmods.com/slaythespire2/mods/264?tab=files)
 2. Click "Mod manager download"
 3. Enjoy

 ### Manual

 1. Go to your Slay the Spire 2 install folder, this is the folder that contains the game executable as well as the `data_sts2_{platform}` folder.
 2. Create a folder named `mods`
 3. Download the [latest release](https://github.com/phantomgamers/CustomCardTextureLoader/releases/latest/CustomCardTextureLoader.zip)
 4. Extract it to the mods folder so it looks like the following:
 ```
📂Slay the Spire 2  
 ┣ 📂mods  
 ┃ ┗📜CustomCardTextureLoader
 ┃   ┗📦CustomCardTextureLoader.dll
 ┃   ┗🖹 CustomCardTextureLoader.json
 ```

