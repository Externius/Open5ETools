using Open5ETools.Core.Common.Models.DM.Generator;
using Open5ETools.Core.Common.Models.DM.Services;

namespace Open5ETools.Core.Common.Interfaces.Services.DM.Generator;
public interface IDungeonNoCorridor
{
    DungeonTile[][] DungeonTiles { get; set; }
    List<RoomDescription> RoomDescription { get; set; }
    List<DungeonTile> OpenDoorList { get; set; }
    DungeonModel Generate(DungeonOptionModel model);
    void AddEntryPoint();
    void Init(DungeonOptionModel optionModel);
    void AddFirstRoom();
    void FillRoomToDoor();
    void AddDescription();
}
