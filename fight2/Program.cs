using System;
using System.Threading;
using System.Diagnostics;
char input = 'l';
var Stopwatch = new Stopwatch();
Console.CursorVisible = false;
double timeDifference = 0;
List<double> temp = new List<double>();
Player player = new Player("fig");
Player enemy = new Player("fig");
Stopwatch.Start();
while(true){
    Console.SetCursorPosition(0,0);
    if(Console.KeyAvailable){
        input = Console.ReadKey(true).KeyChar;
        if(input == 'q'){
            player.AtkAction(enemy, 1);
        }
    }
    timeDifference = Stopwatch.ElapsedMilliseconds;
    temp.Add(timeDifference);
    Stopwatch.Restart();
    player.DebugPrntStats();
    enemy.DebugPrntStats();
    player.PlayerTick(enemy, timeDifference);
    enemy.PlayerTick(player, timeDifference);
    timeDifference = 5 - timeDifference; 
    if(timeDifference > 0) Thread.Sleep((int) (timeDifference)); //limit clockcyle to 5ms minimum
    if(input == 'p'){
        Stopwatch.Stop();
        break;
    }
}
double[] test = new double[5];
for(int i = 0; i < temp.Count; i++){
    test[0] = test[0] + temp[i];
    if(temp[i] < test[2]) test[2] = temp[i];
    if(temp[i] > test[3]) test[3] = temp[i];
    if(temp[i] == 0) test[4]++;
}

test[1] = test[0]/temp.Count;

for (int i = 0; i < test.Length; i++){
    Console.WriteLine(test[i]);
}
Console.WriteLine(temp.Count);

Console.ReadLine();




