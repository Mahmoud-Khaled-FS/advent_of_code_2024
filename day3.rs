use std::env;
use std::fs;

fn main() {
    let args: Vec<String> = env::args().collect();
    if args.len() != 2 {
        println!("Usage: day3.rs <input file>");
        std::process::exit(1);
    }
    let file_path = &args[1];
    let contents = fs::read_to_string(file_path).expect("ERROR: Can not read file {file_path}");
    let result = get_mul_instructions(contents.clone());
    println!("Part 1 Result: {}", result.get(0).unwrap());
    println!("Part 2 Result: {}", result.get(1).unwrap());
}

fn get_mul_instructions(content: String) -> Vec<i64> {
    let mut instructions_index: Vec<i32> = vec![];
    let mut i: usize = 0;
    let mut result1: i64 = 0;
    let mut result2: i64 = 0;
    let mut switcher: i32 = 1;
    while i < content.len() {
        let char = content.chars().nth(i).unwrap();
        if char.is_whitespace() {
            i += 1;
            continue;
        }
        if char == 'd' {
            if content.get(i..i + 4).unwrap() == "do()" {
                switcher = 1;
            }
            if content.get(i..i + 7).unwrap() == "don't()" {
                switcher = 0;
            }
        }
        if char == 'm' {
            let m_instruction = content.get(i..i + 4).expect("");
            if m_instruction == "mul(" {
                instructions_index.push((i + 4) as i32);
                i += 1;
                continue;
            }
        }
        if char == ')' {
            if instructions_index.len() == 0 {
                i += 1;
                continue;
            }
            let ins_index = instructions_index.pop().unwrap() as usize;
            let inst_body = String::from(content.get(ins_index..i).expect(""));
            let numbers = inst_body.split(",");
            let mut nums: Vec<i32> = vec![];
            for num in numbers {
                match num.parse::<i32>() {
                    Ok(num) => nums.push(num),
                    Err(_) => {
                        continue;
                    }
                }
            }
            if nums.len() != 2 {
                i += 1;
                continue;
            }
            result1 += (nums.get(0).unwrap() * nums.get(1).unwrap()) as i64;
            result2 += (nums.get(0).unwrap() * nums.get(1).unwrap() * switcher) as i64;
        }
        i += 1;
    }
    return vec![result1, result2];
}
