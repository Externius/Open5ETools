using Open5ETools.Core.Common.Enums.DM;
using Open5ETools.Core.Common.Models.DM.Generator;

namespace Open5ETools.Core.Services.DM.Generator;

public static class RoomPosition
{
    public static bool Up { get; private set; }
    public static bool Down { get; private set; }
    public static bool Left { get; private set; }
    public static bool Right { get; private set; }

    internal static void CheckRoomPosition(DungeonTile[][] dungeonTiles, int x, int y)
    {
        Up = false;
        Down = false;
        Left = false;
        Right = false;
        if (dungeonTiles[x][y - 1].Texture == Texture.Room) // left
            Left = true;
        if (dungeonTiles[x][y + 1].Texture == Texture.Room) // right
            Right = true;
        if (dungeonTiles[x + 1][y].Texture == Texture.Room) // bottom
            Down = true;
        if (dungeonTiles[x - 1][y].Texture == Texture.Room) // top
            Up = true;
    }
}