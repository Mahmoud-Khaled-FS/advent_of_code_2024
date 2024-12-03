const std = @import("std");
const print = std.debug.print;
const allocator = std.heap.page_allocator;

pub fn main() !void {
    var args = try std.process.argsWithAllocator(allocator);
    _ = args.skip();
    const file_path = args.next();
    std.debug.assert(file_path != null);
    var file = try std.fs.cwd().openFile(file_path.?, .{});
    defer file.close();

    var buffer_reader = std.io.bufferedReader(file.reader());
    var in_stream = buffer_reader.reader();
    var line_buffer: [1024]u8 = undefined;

    var result: i32 = 0;
    var result2: i32 = 0;
    while (try in_stream.readUntilDelimiterOrEof(&line_buffer, '\n')) |line| {
        var levels: [10]i32 = undefined;
        const size = try parseLevelsLine(line, &levels);
        result += try isSafePart1(levels[0..size]);
        const s = try isSafePart2(levels[0..size]);
        result2 += s;
    }
    print("Part 1 Result= {d}\n", .{result});
    print("Part 2 Result= {d}\n", .{result2});
}

fn parseLevelsLine(line: []u8, levels: []i32) !usize {
    var newLevels: [10]i32 = undefined;
    var lastIndex: usize = 0;
    var size: usize = 0;
    for (0..line.len) |i| {
        if (line[i] == ' ' or i == line.len - 1) {
            newLevels[size] = try std.fmt.parseInt(i32, line[lastIndex..i], 10);
            size += 1;
            lastIndex = i + 1;
        }
    }
    std.mem.copyBackwards(i32, levels, &newLevels);
    return size;
}

fn isSafePart1(levels: []i32) !i32 {
    return isListIntSafe(levels);
}

fn isSafePart2(levels: []i32) !i32 {
    if (isListIntSafe(levels) == 0) {
        for (0..levels.len) |i| {
            var array = std.ArrayList(i32).init(allocator);
            try array.appendSlice(levels[0..i]);
            try array.appendSlice(levels[i + 1 .. levels.len]);
            if (isListIntSafe(try array.toOwnedSlice()) == 1) {
                return 1;
            }
            array.clearAndFree();
        }
        return 0;
    }
    return 1;
}
fn isListIntSafe(levels: []i32) i32 {
    var lastDiff: i32 = 0;
    for (0..levels.len - 1) |i| {
        var diff: i32 = levels[i] - levels[i + 1];
        if ((lastDiff > 0 and diff < 0) or (lastDiff < 0 and diff > 0)) {
            return 0;
        }
        lastDiff = diff;
        diff = @intCast(@abs(lastDiff));
        if (diff < 1 or diff > 3) {
            return 0;
        }
    }
    return 1;
}
