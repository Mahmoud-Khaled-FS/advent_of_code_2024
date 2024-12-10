import java.io.File
import solvePart1

fun main(args: Array<String>) {
  if (args.size < 1) {
    println("ERROR: invalid file path!")
    System.exit(1)
  }
  val lines = File(args[0]).readLines()
  var result1 = 0.0
  var result2 = 0.0
  for (line in lines) {
    result1 += solvePart1(line)
    result2 += solvePart2(line)
  }
  val result1String = String.format("%.0f", result1)
  val result2String = String.format("%.0f", result2)
  println("Result Part 1: $result1String")
  println("Result Part 2: $result2String")
}

fun solvePart1(line: String): Double {
  val splitedLine = line.split(":")
  val leftSide = splitedLine[0].toDouble()
  val rightSide = splitedLine[1].split(" ").filter {it != ""}.map {it.toDouble()}
  fun solveTravel(left: Double, rightSide: List<Double>): Boolean {
    if (left == 0.0) {
      return true
    }
    if (rightSide.size <= 0) {
      return false
    }
  return solveTravel(left / rightSide.last(), rightSide.subList(0, rightSide.lastIndex)) 
      || solveTravel(left - rightSide.last(), rightSide.subList(0, rightSide.lastIndex))
  }
  return if(solveTravel(leftSide / rightSide.last(), rightSide.subList(0, rightSide.lastIndex)) 
      || solveTravel(leftSide - rightSide.last(), rightSide.subList(0, rightSide.lastIndex))) leftSide else 0.0
}

fun solvePart2(line: String): Double {
  val splitedLine = line.split(":")
  val leftSide = splitedLine[0].toDouble()
  val rightSide = splitedLine[1].split(" ").filter {it != ""}.map {it.toDouble()}
  fun solveTravel(left: Double, rightSide: List<Double>, result: Double): Boolean {
    if (rightSide.size <= 0) {
      return left == result
    }
    val isNormalOp = solveTravel(left * rightSide.get(0), rightSide.drop(1), leftSide) || solveTravel(left + rightSide.get(0), rightSide.drop(1), leftSide)
  val newNum = String.format("%.0f%.0f", left, rightSide.get(0)).toDouble()
  return isNormalOp || solveTravel(newNum, rightSide.drop(1), leftSide)
  }
  val isNormalOp = solveTravel(rightSide.get(0), rightSide.drop(1), leftSide) || solveTravel(rightSide.get(0), rightSide.drop(1), leftSide)
  return if(isNormalOp) leftSide else 0.0
}
