import java.io.BufferedReader;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

class Main {
  static public void main(String[] args) {
    if (args.length < 1) {
      System.err.println("ERROR: Invalid file path!");
      System.exit(1);
    }
    var lines = readFile(args[0]);
    var result1 = solvePart1(lines);
    System.out.println("Result Part 1: " + result1.size());
    System.out.println("Result Part 2: " + solvePart2(lines, result1));
  }

  static private List<String> readFile(String filePath) {
    List<String> lines = new ArrayList<>();
    try {
      FileReader fd = new FileReader(filePath);
      BufferedReader br = new BufferedReader(fd);
      String line = br.readLine();
      while (line != null) {
        lines.add(line);
        line = br.readLine();
      }
      br.close();
    } catch (Exception err) {
      System.err.println(err.getMessage());
      System.exit(1);
    }
    return lines;
  }

  static private Map<String, Vec2> solvePart1(List<String> lines) {
    Vec2 p = getPointPos(lines);
    Vec2 dir = new Vec2(0, -1);
    Vec2[] dirs = { new Vec2(0, -1), new Vec2(1, 0), new Vec2(0, 1), new Vec2(-1, 0) };
    var swap = 0;
    Map<String, Vec2> visited = new HashMap<>();
    var k = String.format("%d,%d", p.y, p.x);
    visited.put(k, dir);
    while (true) {
      dir = dirs[swap % dirs.length];
      if (p.y + dir.y < 0 || p.y + dir.y >= lines.size()) {
        break;
      }
      if (p.x + dir.x < 0 || p.x + dir.x >= lines.get(0).length()) {
        break;
      }
      if (lines.get(p.y + dir.y).charAt(p.x + dir.x) == '#') {
        swap++;
        continue;
      }
      p.x += dir.x;
      p.y += dir.y;

      k = String.format("%d,%d", p.y, p.x);
      if (visited.containsKey(k)) {
        var visitedDir = visited.get(k);
        if (visitedDir.x == dir.x && visitedDir.y == dir.y) {
          return null;
        }
      }
      visited.put(k, dir);
    }
    return visited;
  }

  static private int solvePart2(List<String> lines, Map<String, Vec2> visited) {
    Vec2 p = getPointPos(lines);
    int result = 0;
    for (var key : visited.keySet()) {
      // System.out.println(key);
      var s = key.split(",");
      var y = Integer.parseInt(s[0]);
      var x = Integer.parseInt(s[1]);
      if (p.x == x && p.y == y) {
        continue;
      }
      var newLines = new ArrayList<>(lines);
      newLines.set(y, lines.get(y).substring(0, x) + '#' + lines.get(y).substring(x
          + 1));
      if (solvePart1(newLines) == null) {
        result++;
      }
    }
    return result;
  }

  static private Vec2 getPointPos(List<String> lines) {
    for (int y = 0; y < lines.size(); y++) {
      for (int x = 0; x < lines.get(0).length(); x++) {
        if (lines.get(y).charAt(x) == '^') {
          return new Vec2(x, y);
        }
      }
    }

    return new Vec2(0, 0);
  }
}

class Vec2 {
  public int x;
  public int y;

  public Vec2(int x, int y) {
    this.x = x;
    this.y = y;
  }

  @Override
  public String toString() {
    return String.format("(%d,%d)", this.y, this.x);
  }
}