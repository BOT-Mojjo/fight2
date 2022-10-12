using System;
public class Player{
    string name = "null";
    (bool atkq, int atkt, double atkr, double atkc) atk = (false, 0, 0, 0); //attack check, attack type, attack rampup, attack cooldown
    double stun = 0;    //amount of stun ticks left for this stunlock
    int stuntms = 0; //amount of times stunned for 1 stunlock
    bool block = false;  //if you're blocking
    int parryGrace = 0; // amount of ticks for grace parrying.
    double hp = 25;
    double healCD = 0; //if you don't take damage for 2 seconds, heal to a minimum of 8 hp.
    int str = 10;  //atk dmg modifier
    int dex = 10;  //atk spd modifier
    int con = 10;  //hp taken and stun modifier
    double deltaTime = 0;

    //taking dmg doesn't interrupt atkr, but does interuppt atkc, imposing stun instead.

    public Player(string type){
        switch (type){
            case("fig"):
                name = "Fighter";
                break;
            case("ski"):
                name = "Skirmisher";
                dex = 15;
                con = 8;
                break;
            case("def"):
                name = "Defender";
                str = 8;
                con = 15;
                break;
        }
    }

    public void DebugPrntStats(){
        Console.WriteLine(name + "\n" + hp + "\n" + stun + "\n" + stuntms + "\n" + block + "\n" + atk.atkq + "\n" + atk.atkt + "\n" + atk.atkr + "\n" + atk.atkc + "\n" + healCD + "\n" + parryGrace + "\n" + deltaTime);
    }

    public void PlayerTick(Player target, double time){
        deltaTime = time;
        if(stun > 0){   //if in stun, reduce stun and end tick
            block = false; 
            stun -= deltaTime;
            if(stun < 0) stun = 0;
            return;
        }
        stuntms = 0;
        if(atk.atkq){  //atk logic
            if(atk.atkr >= 0){
                atk.atkr -= deltaTime;
                if(atk.atkr < 0) atk.atkr = 0;  //if less than 0 then it should be 0
            } else {
                DoDmg(target, atk.atkt);
                atk.atkq = false;
            }
        } else {
            if(atk.atkc > 0){
                atk.atkc -= deltaTime;
                if(atk.atkc < 0) atk.atkc = 0;  //if less than 0 then it should be 0
            }
        }
        healCD -= healCD > 0 ? deltaTime : 0;
        if(healCD <= 0 && hp < 8){
            hp += 2000/deltaTime;
        }
    }

    public void AtkAction(Player target, int atkType){
        if(stun <= 0 && atk.atkc <= 0 && !atk.atkq){  //if you aren't stunned and you don't have attack cooldown, and you aren't already attacking
            atk.atkr = 200 * atkType;
            atk.atkq = true;
            atk.atkt = atkType;
        }
    }

    void DoDmg(Player target, int atkType){
        target.TakeDmg(str*atkType);
        atk.atkc = 400*atkType;
    }

    public void TakeDmg(double dmg){
        if(block){
            if(parryGrace < 0) return;  //TODO make a stun fuction, so a parry stuns the oponent
            hp = hp - dmg - con;        //TODO also make it so you can cancel an attack.
            return;
        }
        double conmod = (5f/con);
        dmg = dmg*conmod;
        if(!atk.atkq){ //if you arn't pre-attack, become stunned by damage
            double tempStun = dmg*100; //stunticks calculated
            tempStun = (tempStun/5) * (5 - stuntms); //if you're being comboed, take less stun. Up to 80% less
            stun += tempStun;
            if(stuntms < 4) stuntms++;
        } 
        healCD = 2;
        hp = hp - dmg;
        if(hp < 0) hp = 0;
    }


}
