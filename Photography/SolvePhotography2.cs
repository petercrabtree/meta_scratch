namespace Meta_Scratch.Photography;
class Solution {
  
  public long getArtisticPhotographCount(int N, string C, int X, int Y) {
    const bool debug = true;
    // need to count all possible unique 'artistic' photos
    // 'artistic' photos have a photographer, then an actor, then a backdrop
    // the elements of the photos have to be at least X apart, and no more than Y
    // apart 
    
    // we can approach this with a sliding window where we compute the "artistic actor scenes"
    // then we can have a sliding window where we compute "artistic backdrops from this actor"
    // then we multiply the backdrops from the actor by the photographers from the actor
    
    // then we do the other direction
    
    // TODO need to implement this for backwards as well
    var result = new long[N];
    var last_possible_actor = N - 1 - X;
    
    // n can be == 300,000
    // and we have 5 seconds to run
    // so we need to be pretty efficient
    // let's build up the state incrementally using a window
    var window_length = X - Y + 1;

    var valid_photos_left = new long[N];
    var valid_photos_right = new long[N];
    var valid_backdrops_left = new long[N];
    var valid_backdrops_right = new long[N];

    // todo: is this clearer if there's one loop for both directions?

    // forward (window to the left)
    // initialize inspection window state
    var photographers = 0;
    var backdrops = 0;
    
    for (var i = X; i < N; i++) {
      var entering_window = i - X;
      if (C[entering_window] == 'P') {
        photographers++;
      } else if (C[entering_window] == 'B') {
        backdrops++;
      }
      var leaving_window = i - Y - 1;
      if (leaving_window >= 0) {
        // the window no longer includes:
        if (C[leaving_window] == 'P') {
        photographers--;
        } else if (C[leaving_window] == 'B') {
          backdrops--;
        }
      }
      valid_photos_left[i] = photographers;
      valid_backdrops_left[i] = backdrops;
    }

    // now backwards (compute for right side)
    photographers = 0;
    backdrops = 0;
    for (var i = N - X - 1; i >= 0; i--) { // start i at the first element at least X from the end
      var entering_window = i + X;
      if (C[entering_window] == 'P') {
        photographers++;
      } else if (C[entering_window] == 'B') {
        backdrops++;
      }
      var leaving_window = i + Y + 1;
      if (leaving_window < N) {
        // the window no longer includes:
        if (C[leaving_window] == 'P') {
          photographers--;
        } else if (C[leaving_window] == 'B') {
          backdrops--;
        }
      }
      valid_photos_right[i] = photographers;
      valid_backdrops_right[i] = backdrops;
    }

    // we now have the valid backdrop count and valid actor counts for both directions for all cells
    // find all actor cells and count the possible backdrops and actors from there

    var sum = 0L;
    for (var i = X; i < N - X; i++) {
      if (C[i] == 'A') {
        // we have an actor!
        var photographer_to_left = valid_photos_left[i] * valid_backdrops_right[i];
        var photographer_to_right = valid_photos_right[i] * valid_backdrops_left[i];
        var counts = photographer_to_left + photographer_to_right;
        sum += counts;
        result[i] = counts;
      }
    }
    
    if (debug) {
      Console.WriteLine($"X: {X}, Y: {Y}, N: {N}, C: {string.Join("", C)}");
      Console.WriteLine($"                    C: {string.Join(", ", (IEnumerable<char>)C)}");
      Console.WriteLine($"    valid_photos_left: {string.Join(", ", valid_photos_left)}");
      Console.WriteLine($"valid_backdrops_right: {string.Join(", ", valid_backdrops_right)}");
      Console.WriteLine($" valid_backdrops_left: {string.Join(", ", valid_backdrops_left)}");
      Console.WriteLine($"   valid_photos_right: {string.Join(", ", valid_photos_right)}");
      Console.WriteLine($"               result: {string.Join(", ", result)}");
    }
    
    return sum;
  }

}
