using Open5ETools.Core.Common.Enums.DM;
using Open5ETools.Core.Common.Interfaces.Services.DM.Generator;
using Open5ETools.Core.Common.Models.DM.Generator;
using System.Text;
using Xunit.Abstractions;

namespace Open5ETools.Core.Tests.GeneratorTests;

public abstract class DungeonTestBase
{
    private readonly ITestOutputHelper _output;
    private readonly StringBuilder _stringBuilder = new();
    protected readonly IDungeon Dungeon;
    protected readonly IDungeonNoCorridor DungeonNoCorridor;
    protected DungeonTestBase(ITestOutputHelper output)
    {
        var env = new TestEnvironment();
        Dungeon = env.GetDungeon();
        DungeonNoCorridor = env.GetNcDungeon();
        _output = output;
    }

    protected void Draw(IEnumerable<DungeonTile[]> dungeonTiles)
    {
        foreach (var row in dungeonTiles)
        {
            PrintRow(row);
        }
        _output.WriteLine(" ");
    }

    private void PrintRow(IEnumerable<DungeonTile> row)
    {
        foreach (var i in row)
        {
            switch (i.Texture)
            {
                case Texture.Edge:
                    _stringBuilder.Append('X');
                    break;
                case Texture.RoomEdge:
                    _stringBuilder.Append('#');
                    break;
                case Texture.Marble:
                    _stringBuilder.Append(' ');
                    break;
                case Texture.Room:
                    _stringBuilder.Append('.');
                    break;
                case Texture.NoCorridorDoor:
                case Texture.NoCorridorDoorLocked:
                case Texture.NoCorridorDoorTrapped:
                case Texture.Door:
                case Texture.DoorLocked:
                case Texture.DoorTrapped:
                    _stringBuilder.Append('D');
                    break;
                case Texture.Corridor:
                    _stringBuilder.Append('-');
                    break;
                case Texture.Entry:
                    _stringBuilder.Append('E');
                    break;
                case Texture.Trap:
                    _stringBuilder.Append('T');
                    break;
                case Texture.RoamingMonster:
                    _stringBuilder.Append('M');
                    break;
                default:
                    _stringBuilder.Append(' ');
                    break;
            }
        }
        _output.WriteLine(_stringBuilder.ToString());
        _stringBuilder.Clear();
    }
}