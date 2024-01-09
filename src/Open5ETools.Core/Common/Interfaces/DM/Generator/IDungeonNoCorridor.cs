using Open5ETools.Core.Common.Models.DM.Generator;
using Open5ETools.Core.Common.Models.DM.Services;

namespace Open5ETools.Core.Common.Interfaces.DM.Generator;
public interface IDungeonNoCorridor
{
    DungeonTile[][] DungeonTiles { get; set; }
    ICollection<RoomDescription> RoomDescription { get; set; }
    IList<DungeonTile> OpenDoorList { get; set; }
    DungeonModel Generate(DungeonOptionModel model);
    void AddEntryPoint();
    void Init(DungeonOptionModel optionModel);
    void AddFirstRoom();
    void FillRoomToDoor();
    void AddDescription();
}
