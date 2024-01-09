using Open5ETools.Core.Common.Enums.DM;
using Open5ETools.Core.Common.Models.DM.Generator;
using Open5ETools.Core.Common.Models.DM.Services;

namespace Open5ETools.Core.Common.Interfaces.DM.Generator;
public interface IDungeon
{
    DungeonTile[][] DungeonTiles { get; set; }
    ICollection<RoomDescription> RoomDescription { get; set; }
    DungeonModel Generate(DungeonOptionModel model);
    void AddEntryPoint();
    void Init(DungeonOptionModel optionModel);
    void GenerateCorridors();
    void GenerateRoom();
    void AddDeadEnds();
    void AddCorridorItem(int inCount, Item item);
}
