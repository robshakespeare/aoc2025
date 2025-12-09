# 🎄 Shakey's AoC 2025 🌟

Rob Shakespeare's solutions to the Advent of Code 2025 challenges at https://adventofcode.com/2025.


### Prerequisites

* [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* Optional: to be able to run the cake scripts, first: `dotnet tool restore`


### Run

To run the console application:

```
dotnet run --project AoC.CLI
```

To pull the puzzle input (requires access to the key vault containing valid AoC session token):

```
dotnet run --project AoC.CLI --pull
```

To decrypt the puzzle inputs in this repository (requires `AocPuzzleInputCryptoKey` config value):

```
dotnet run --project AoC.CLI --decrypt
```


### Test

```
dotnet test
```
