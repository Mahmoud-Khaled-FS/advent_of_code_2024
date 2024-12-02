#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int compare(const void *x, const void *y)
{
  return (*(int *)x - *(int *)y);
}

int findIndex(int *array, int start, int search)
{
  for (int i = start; i < 1000; i++)
  {
    if (search == array[i])
    {
      return i;
    }
  }
  return -1;
}

int repeatTime(int *array, int *start, int search)
{
  int index = findIndex(array, 0, search);
  int result = 0;
  if (index == -1)
  {
    return result;
  }
  while (array[index + result] == search)
  {
    result++;
  }
  *start += index;
  return result;
}

int main(int argc, char **argv)
{
  if (argc < 2)
  {
    printf("ERROR: Enter input file!");
    return 1;
  }

  FILE *fptr = fopen(argv[1], "r");
  if (!fptr)
  {
    printf("ERROR: Invalid file name %s", argv[1]);
    return 1;
  }
  int leftSide[1000];
  int rightSide[1000];

  int scan_size = 0;
  while (!feof(fptr))
  {
    int numberLeft;
    int numberRight;
    fscanf(fptr, "%d %d", &numberLeft, &numberRight);
    leftSide[scan_size] = numberLeft;
    rightSide[scan_size] = numberRight;
    scan_size++;
  }

  qsort(leftSide, 1000, sizeof(int), compare);
  qsort(rightSide, 1000, sizeof(int), compare);
  int dist = 0;
  for (int i = 0; i < 1000; i++)
  {
    if (!leftSide[i])
    {
      break;
    }
    dist += abs(leftSide[i] - rightSide[i]);
  }
  printf("Result Part 1: %d\n", dist);

  // Part 2
  int repeatedTime = 0;
  int rightSideStart = 0;
  for (int i = 0; i < 1000; i++)
  {
    int r = repeatTime(rightSide, &rightSideStart, leftSide[i]);
    repeatedTime += leftSide[i] * r;
  }

  printf("Result Part 2: %d\n", repeatedTime);
}
