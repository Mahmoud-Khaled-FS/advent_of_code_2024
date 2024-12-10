import Foundation

struct Vec2 {
  var x = 0
  var y = 0
}
if CommandLine.argc < 2 {
  print("ERROR: Invalid file path")
  exit(1)
}
let filePath = CommandLine.arguments[1]
let content: String = try! String(contentsOfFile: filePath)
let lines = content.split(whereSeparator: \.isNewline)
func solvePart1() -> Int {
  var placedPos: Set<String> = []
  var points: [Character: [Vec2]] = [:]
  var result: Int = 0
  for (y, line) in lines.enumerated() {
    for (x, char) in line.enumerated() {
      if char == "." || char.isWhitespace {
        continue
      }
      if points[char] == nil {
        points[char] = []
      }
      var newPoints: [Vec2] = []
      for point in points[char]! {
        let dx = point.x - x
        let dy = point.y - y
        newPoints.append(Vec2(x: x - dx, y: y - dy))
        newPoints.append(Vec2(x: point.x + dx, y: point.y + dy))
      }
      for point in newPoints {
        if point.y < 0 || point.y >= lines.count {
          continue
        }
        if point.x < 0 || point.x >= lines[0].count {
          continue
        }
        if !placedPos.contains("\(point.y),\(point.x)") {
          result += 1
          placedPos.insert("\(point.y),\(point.x)")
        }
      }
      points[char]!.append(Vec2(x: x, y: y))
    }
  }
  return result
}
func solvePart2() -> Int {
  var placedPos: Set<String> = []
  var points: [Character: [Vec2]] = [:]
  var result: Int = 0
  for (y, line) in lines.enumerated() {
    for (x, char) in line.enumerated() {
      if char == "." || char.isWhitespace {
        continue
      }
      if points[char] == nil {
        points[char] = []
      }
      var newPoints: [Vec2] = []
      for point in points[char]! {
        let dx = point.x - x
        let dy = point.y - y
        var i = Vec2(x: x, y: y)
        while true {
          // print("x")
          if i.y < 0 || i.y >= lines.count {
            break
          }
          if i.x < 0 || i.x >= lines[0].count {
            break
          }
          newPoints.append(Vec2(x: i.x, y: i.y))
          i.x -= dx
          i.y -= dy
        }
        i = Vec2(x: x + dx, y: y + dy)
        while true {
          if i.y < 0 || i.y >= lines.count {
            break
          }
          if i.x < 0 || i.x >= lines[0].count {
            break
          }
          newPoints.append(Vec2(x: i.x, y: i.y))
          i.x += dx
          i.y += dy
        }
      }
      for point in newPoints {
        if !placedPos.contains("\(point.y),\(point.x)") {
          result += 1
          placedPos.insert("\(point.y),\(point.x)")
        }
      }
      points[char]!.append(Vec2(x: x, y: y))
    }
  }
  return result
}
print("Result Part 1: \(solvePart1())")
print("Result Part 2: \(solvePart2())")
