# define the game board
board = ["_", "_", "_",
         "_", "_", "_",
         "_", "_", "_"]

# define the winning combinations
winning_combinations = [
    [0, 1, 2],
    [3, 4, 5],
    [6, 7, 8],
    [0, 3, 6],
    [1, 4, 7],
    [2, 5, 8],
    [0, 4, 8],
    [2, 4, 6]
]

# define a function to print the game board
def print_board():
  for i in range(0, len(board), 3):
    print(board[i] + " | " + board[i+1] + " | " + board[i+2])
    if i < 6:
      print("---------")

# define a function to check if the game has ended
def game_ended():
  # check for a winner
  for combination in winning_combinations:
    if board[combination[0]] == board[combination[1]] == board[combination[2]]:
      return True

  # check for a draw
  if "_" not in board:
    return True

  # if the game has not ended, return False
  return False

# define a function to get the computer's move
def computer_move():
  # check if the computer can win in the next move
  for combination in winning_combinations:
    if board[combination[0]] == board[combination[1]] == "O":
      if board[combination[2]] == "_":
        return combination[2]
    elif board[combination[0]] == board[combination[2]] == "O":
      if board[combination[1]] == "_":
        return combination[1]
    elif board[combination[1]] == board[combination[2]] == "O":
      if board[combination[0]] == "_":
        return combination[0]

  # check if the player can win in the next move
  for combination in winning_combinations:
    if board[combination[0]] == board[combination[1]] == "X":
      if board[combination[2]] == "_":
        return combination[2]
    elif board[combination[0]] == board[combination[2]] == "X":
      if board[combination[1]] == "_":
        return combination[1]
    elif board[combination[1]] == board[combination[2]] == "X":
      if board[combination[0]] == "_":
        return combination[0]

  # if the computer cannot win and the player cannot win, choose a random move
  while True:
    move = random.randint(0, 8)
    if board[move] == "_":
      return move

# define a function to get the player's move
def player_move():
  # get the player's move
  move = int(input("Enter your move (0-8): "))

  # check if the move is valid
  if move >= 0 and move <= 8 and board[move] == "_