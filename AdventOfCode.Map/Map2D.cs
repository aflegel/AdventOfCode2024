using System.Text;

namespace AdventOfCode.Map;

public class Map2D<T>
{
    private readonly T[,] _map;
    public T this[Position2D index] => _map[index.X, index.Y];
    public int Width => _map.GetLength(1);
    public int Height => _map.GetLength(0);

    public bool OutOfBounds(Position2D position) => position.X < 0 || position.Y < 0 || position.X >= Width || position.Y >= Height;

    public static Map2D<char> FromString(string input)
    {
        var map = input.Split("\n").To2DArray();
        return new Map2D<char>(map);
    }

    public Map2D(T[,] map)
    {
        _map = map;
    }

    public Map2D(int width, int height)
    {
        _map = new T[height, width];
    }

    public void Set(Position2D position, T value) => _map[position.X, position.Y] = value;
    public T Get(Position2D position) => _map[position.X, position.Y];

    public void Fill(T value)
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                _map[i, j] = value;
            }
        }
    }

    public IEnumerable<Position2D> Positions()
    {
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                yield return new(i, j);
            }
        }
    }

    public IEnumerable<Position2D> SearchAll(T target)
        => Positions()
            .Where(w => _map[w.Y, w.X].Equals(target));

    public void PrintToConsole()
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                Console.Write(_map[i, j]);
            }
            Console.WriteLine();
        }
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                result.Append(_map[i, j]);
            }
            result.Append('\n');
        }

        return result.ToString();
    }
}

internal static class EnumerableExtensions
{
    /**
	Source https://gist.github.com/kekyo/2e0c456f506ec31431f33741608d5230
	*/
    public static T[,] To2DArray<T>(this IEnumerable<IEnumerable<T>> source)
    {
        var data = source
            .Select(x => x.ToArray())
            .ToArray();

        var res = new T[data.Length, data.Max(x => x.Length)];
        for (var i = 0; i < data.Length; ++i)
        {
            for (var j = 0; j < data[i].Length; ++j)
            {
                res[i, j] = data[i][j];
            }
        }

        return res;
    }
}