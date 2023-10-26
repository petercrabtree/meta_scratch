namespace Meta_Scratch.Conveyors;
class Node
{
  public List<Vertex> vertexes = new();
  public double? expectedCost; // expected h value from this node to ground
}
class Vertex
{
  public double weight; // chance this vertex is followed
  public required Node node;
}
class Belt
{
  public int Height;
  public int Start;
  public int End;
}
class Range
{
  public int start;
  public int end;
  public Belt? target;
  public Range(int start, int end, Belt? target)
  {
    this.start = start;
    this.end = end;
    this.target = target;
  }
}
class Solution
{

  public double getMinExpectedHorizontalTravelDistance(int N, int[] H, int[] A, int[] B)
  {
    // Write your code here

    // transform H A B to graph

    // a weird bit of the requirements is that conveyor coords are EXCLUSIVE
    // however, this means a TINY slice of the space is guaranteed to have a 0
    // horiz travel distance
    // (the slice from 0-> min value more than 0 and the slice from 1,000,000 to max value below 1,000,000)
    // we'll just ignore this for now I think, because those slices probably
    // fit inside the 10^-6 error window we're allowed

    // NOTE we should be careful to consider whether we need to merge nodes that refer to the same 
    // target (probably not? consider two drops to the same belt--the drops have different expected travel distances)

    // root ->
    // nodes reachable from top
    // -> nodes below either side
    // -> ground

    // convert to more convenient data structure
    var n = H.Length;
    var belts = new List<Belt>();
    for (var i = 0; i < n; i++)
    {
      belts.Add(new Belt
      {
        Height = H[i],
        Start = A[i],
        End = B[i],
      });
    }

    // we need to process belts in height order
    var belts_from_top = belts.OrderByDescending(b => b.Height).ToList();

    // find the ranges that are represented by vertexes from root
    var ground = new Node
    {
      expectedCost = 0,
    };
    var root = new Node();
    var ground_ranges = new List<Range> {
      new(0, 1000000, null),
    };
    var belt_ranges = new List<Range>();

    foreach (var belt in belts_from_top)
    {
      if (ground_ranges.Count == 0)
      {
        // no more ground to cover!
        break;
      }
      // find all ranges that this belt overlaps with from ground_ranges
      var overlapping_ranges = ground_ranges
        .Where(r => r.start < belt.End && r.end > belt.Start)
        .ToList();

      // move all fully-contained ranges from ground_ranges to belt_ranges 
      // (and update their target to this belt's node)
      for (int i = 0; i < overlapping_ranges.Count; i++)
      {
        Range range = overlapping_ranges[i];
        var fully_contained = range.start >= belt.Start && range.end <= belt.End;
        if (fully_contained)
        {
          ground_ranges.Remove(range);
          belt_ranges.Add(new Range(range.start, range.end, belt));
        }
        else
        {
          // split the range into two
          // one stays in ground_ranges (modify the existing one)
          // one goes to belt_ranges
          if (belt.Start > range.start)
          {
            range.end = belt.Start;
            belt_ranges.Add(new Range(belt.Start, range.end, belt));
          }
          else
          {
            range.start = belt.End;
            belt_ranges.Add(new Range(range.start, belt.End, belt));
          }
        }
      }

      
    }

    // graph should start with ranges of belts/ground from the top view
    // then, each vertex draws an edge to the graphs below
    // then, our goal is to figure out which single edge removed from the graph
    // will reduce the overall traversal cost the most (brute force this?)

    // X = GetTopDownView(H, A, B)
    // graph = GetGraph(X, H, A, B)
    // mutate each graph
    // find min GetExpectedCost(graph)


    // range -> node
    return 0.0;
  }

}
