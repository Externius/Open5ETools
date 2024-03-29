﻿using Open5ETools.Core.Common.Models.DM.Generator;
using Open5ETools.Core.Common.Models.DM.Services;

namespace Open5ETools.Core.Common.Interfaces.Services.DM.Generator;

public interface IDungeonHelper
{
    void AddRoomDescription(DungeonTile[][] dungeonTiles, int x, int y, ICollection<RoomDescription> roomDescription, ICollection<DungeonTile> currentDoors);
    void AddTrapDescription(DungeonTile dungeonTile, ICollection<TrapDescription> trapDescription);
    void AddRoamingMonsterDescription(DungeonTile dungeonTile, ICollection<RoamingMonsterDescription> roamingMonsterDescription);
    int Manhattan(int dx, int dy);
    void AddNcRoomDescription(DungeonTile dungeonTile, ICollection<RoomDescription> roomDescription, string doors);
    void Init(DungeonOptionModel model);
    bool CheckNcDoor(DungeonTile dungeonTile);
    string GetNcDoorDescription(DungeonTile[][] dungeonTiles, IEnumerable<DungeonTile> closedList);
    string GetNcDoor(DungeonTile door);
    string GetTreasure();
    string GetMonsterDescription();
    string GetRandomTrapDescription(bool door);
    string GetRoamingMonsterDescription();
    DungeonTile[][] GenerateDungeonTiles(int dungeonSize, int imgSizeX, int imgSizeY);
}