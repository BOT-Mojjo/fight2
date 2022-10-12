using System;
using System.Threading;
using System.Diagnostics;
var Stopwatch = new Stopwatch();
Console.CursorVisible = false;
int temp = 0;
double timeDifference = 0;
Player player = new Player("fig");
Player enemy = new Player("fig");
Stopwatch.Start();
while(true){
    Console.SetCursorPosition(0,0);
    if(Console.KeyAvailable){
        temp++;
        char input = Console.ReadKey(true).KeyChar;
        if(input == 'q'){
            player.AtkAction(enemy, 1);
        }
    }
    timeDifference = Stopwatch.ElapsedMilliseconds;
    Stopwatch.Restart();
    player.DebugPrntStats();
    enemy.DebugPrntStats();
    player.PlayerTick(enemy, timeDifference);
    enemy.PlayerTick(player, timeDifference);
}





