namespace Meta_Scratch.Conveyors;
class Node {
  public List<Vertex> vertexes = new();
  public double? expectedCost; // expected h value from this node to ground
}
struct Vertex {
  public double weight; // chance this vertex is followed
  public Node node;
}
struct Belt {
  public int Height;
  public int Start;
  public int End;
}
struct Range {
  public int start;
  public int end;
  public Node target;
}
class Solution {
  
  public double getMinExpectedHorizontalTravelDistance(int N, int[] H, int[] A, int[] B) {
    // Write your code here
    
    // transform H A B to graph
    
    // a weird bit of the requirements is that conveyor coords are EXCLUSIVE
    // however, this means a TINY slice of the space is guaranteed to have a 0
    // horiz travel distance
    // (the slice from 0-> min value more than 0 and the slice from 1,000,000 to max value below 1,000,000)
    // we'll just ignore this for now I think, because those slices probably
    // fit inside the 10^-6 error window we're allowed
    
    // root ->
    // nodes reachable from top
    // -> nodes below either side
    // -> ground
    
    var n = H.Length;
    var belts = new List<Belt>();
    for(var i = 0; i < n; i++) {
      belts.Add(new Belt{
        Height = H[i],
        Start = A[i],
        End = B[i],
      });
    }
    
    var belts_from_top = belts.OrderByDescending(b => b.Height).ToList();
    
    var ground = new Node {
      expectedCost = 0,
    };
    var root = new Node();
    var ranges = new List<Range> {
      new Range(0, 1000000, );
    };
    foreach(var belt in belts_from_top) {
      
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
