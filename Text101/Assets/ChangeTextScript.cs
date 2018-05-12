using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextScript : MonoBehaviour
{
    public Text storyTelling;

    bool hairclip, key, clothes,closetDoorOpen;

    enum cellStates { CELL,CELL_KEY, LOCK ,LOCK_KEY ,CRACKS , BED,BED_KEY , CORRIDOR };
    enum corridorStates { CORRIDOR_0, CORRIDOR_1, CORRIDOR_2, CORRIDOR_3, FLOOR,FLOOR_CLIP,STAIRS_0,STAIRS_1,STAIRS_2,CLOSET_DOOR,COURT_YARD ,IN_CLOSET,FINISHED};

    cellStates currentCellState = cellStates.CELL;
    corridorStates currentCorridorState = corridorStates.CORRIDOR_0;

    string items = "";
       string firstParagraph = "First day in cell, bed with a thin mattress toilets with no door to cover and cracks above it myself and small window with iron bars " +
        "still not the worst as i thought but I wont sit down on a murdered i didnt commit.." +
        "Press for inspection on of the following  L for Lock C for Cracks B Bed ";


    // Use this for initialization
    void Start()
    {
        storyTelling.text = firstParagraph;
        print(currentCellState);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (currentCellState != cellStates.CORRIDOR)
            firstSection();
        else
            secondSection(); 
    }

    void firstSection()
    {
        switch (currentCellState)
        {
            case cellStates.CELL:   cellState();   break;
            case cellStates.CELL_KEY:   cellKeyState();   break;
            case cellStates.CRACKS:   returnToCellKeyState();    break;
            case cellStates.BED_KEY: returnToCellKeyState(); break;
            case cellStates.LOCK_KEY: lockKey();  break;
            default:  returnToCell();  break;
        }
    }

    void secondSection()
        {
            switch (currentCorridorState)
            {
            case corridorStates.CORRIDOR_0: corridorState0();    break;
            case corridorStates.STAIRS_0:  stairs0();      break;
             case corridorStates.FLOOR:   floor0();      break;
            case corridorStates.FLOOR_CLIP:  floorClip();  break;
            case corridorStates.CLOSET_DOOR:   closetDoor();   break;
            case corridorStates.IN_CLOSET:   inCloset();  break;
            case corridorStates.CORRIDOR_1:   corridor1();   break;
            case corridorStates.CORRIDOR_2:   corridor2();  break;
            case corridorStates.STAIRS_1:      stairs1();     break;
        }
        }


    void returnToCell() {
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentCellState = cellStates.CELL;
                storyTelling.text = firstParagraph;
            }
        }

    void cellState()
    {
            if (Input.GetKeyDown(KeyCode.L))
            {
                lockState();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                cracksState();
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                bedState();
            }
    }

    void cracksState()
    {
        currentCellState = cellStates.CRACKS;
        storyTelling.text = "Damn this place very old even the walls start to cracks.... wait there is something shining between " +
            "is that..... is that a key for the.... i must check it out no way ? \n " +
            "Key was Picked !\n  Press R to return. ";
        key = true;
        returnToCellKeyState();
    }

    void bedState()
    {
        currentCellState = cellStates.BED;
        storyTelling.text = "Small bed with thin mattress, my back will hurt so much after sleeping on this \n " + "Press R to return.";
        returnToCell();
    }

    void lockState()
    {
            currentCellState = cellStates.LOCK;
        storyTelling.text = "mmm strong bars for sure no humans hand could break them. shame I dont have some lock pick to open the lock..\n " + "Press R to return.";
        returnToCell();
    }

    void cellKeyState()
    {
        print("cell key state now");
        string cracksBedText = "Nothing else here alrdy took the KEY .. \n Press R to return ";
        if (Input.GetKeyDown(KeyCode.L)   )
        {
            if (key)
            {
                storyTelling.text = "WOW the key match now lets get out queitly\n   You have unlocked the door Press Space to exit to corrider ";
                currentCellState = cellStates.LOCK_KEY;
            }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            currentCellState = cellStates.CRACKS;
            storyTelling.text = cracksBedText ;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            currentCellState = cellStates.BED_KEY;
            storyTelling.text = cracksBedText;
        }
    }

   void returnToCellKeyState()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentCellState = cellStates.CELL_KEY;
            storyTelling.text = "Ok i have the KEY let see if it is match the lock on the cell door ..\n" +
                "Press for inspection on of the following  L for Lock C for Cracks B Bed ";
            print("R was pressed");
        }
      
    }

    void lockKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentCellState = cellStates.CORRIDOR;
            storyTelling.text = "No one did not notice me yet i should be carful with my next steps..\n Press S to go up stairs ,F inspect floor , C closet door. ";
        }
    }

    public void corridorState0()
    {
        storyTelling.text = "No one did not notice me yet i should be carful with my next steps..\n Press S to go up stairs ,F inspect floor , C closet door. ";
        if (Input.GetKeyDown(KeyCode.S))
            currentCorridorState = corridorStates.STAIRS_0;
        else if (Input.GetKeyDown(KeyCode.F))
            currentCorridorState = corridorStates.FLOOR;
        else if (Input.GetKeyDown(KeyCode.C))
            if (closetDoorOpen)
                currentCorridorState = corridorStates.IN_CLOSET;
        else
            currentCorridorState = corridorStates.CLOSET_DOOR;
    }

    public void stairs0()
    {
        storyTelling.text = "I cant go outside with these clothes i will be spoted in a second ...\nR to return.";
        if (Input.GetKeyDown(KeyCode.R))
            currentCorridorState = corridorStates.CORRIDOR_0;
    }

    public void floor0()
    {
        if (hairclip)
        {
            storyTelling.text = "I got the hairclip nothing else useful here...\nR to return.";
        }
        else
        {
            storyTelling.text = "The floor very dirty the only usfull thing might be the hairclicp   ,\n Press P to pick the hairclip R to return";
        }
        if (Input.GetKeyDown(KeyCode.R))
            currentCorridorState = corridorStates.CORRIDOR_0;
        else if (Input.GetKeyDown(KeyCode.P))
        {
            currentCorridorState = corridorStates.FLOOR_CLIP;
        }
    }

    public void floorClip()
    {
        hairclip = true;
        storyTelling.text = "Hairclip was picked ... Press R to return";
        if (Input.GetKeyDown(KeyCode.R))
            currentCorridorState = corridorStates.CORRIDOR_1;
    }
    
    public void closetDoor()
    {
        if (hairclip){ 
            storyTelling.text = "Some twist will do the job ..... got it \n Closet door unlocked. Press space to continue.";
            closetDoorOpen = true;
            if(Input.GetKeyDown(KeyCode.Space))
            currentCorridorState = corridorStates.IN_CLOSET;
        }
        else
        {
            storyTelling.text = "The closet may have some clothes that would help me to escape...\nDamn the door locked, must find something to unlock it...\n Press R to return..";
            if (Input.GetKeyDown(KeyCode.R))
                currentCorridorState = corridorStates.CORRIDOR_0;
        }

    }

    public void inCloset()
    {
        if (clothes)
        {
            storyTelling.text = "Cleaner cloth was picked nothing else here.. Press U to undress R to return.";
            if (Input.GetKeyDown(KeyCode.U))
            {
                clothes = false;
                storyTelling.text = "You undress the clothes and put them back to the closet .\n Press P to pick the clothes R to return.";
            }else if (Input.GetKeyDown(KeyCode.R))
            {
                currentCorridorState = corridorStates.CORRIDOR_2;
            }
        }
        else
        {
            storyTelling.text = "There are some cleaner clothes if i will pick them the gaurds won't notice me ...\n Press P to pick the cloth R to return";
            if (Input.GetKeyDown(KeyCode.R))
                currentCorridorState = corridorStates.CORRIDOR_2;
            else if (Input.GetKeyDown(KeyCode.P))
            {
                clothes = true;
            }
        }
    }

    public void corridor1()
    {
        storyTelling.text = "I got the hair clip i might try to unlock the closet with it. \n Press S to go up stairs ,F inspect floor , C closet door.";
        if (Input.GetKeyDown(KeyCode.S))
            currentCorridorState = corridorStates.STAIRS_0;
        else if (Input.GetKeyDown(KeyCode.F))
            currentCorridorState = corridorStates.FLOOR;
        else if (Input.GetKeyDown(KeyCode.C))
            if (closetDoorOpen)
                currentCorridorState = corridorStates.IN_CLOSET;
            else
                currentCorridorState = corridorStates.CLOSET_DOOR;
    }

    public void corridor2()
    {
        if (clothes)
        {
            storyTelling.text = "I dressed as a cleaner now , if I keep distance from the gaurds they wont notice me..\n Press S to go up stairs , C closet door ";
        }
        else
        {
            storyTelling.text = "I still didnt pick the cleaner cloth..\n Press S to go up stairs , C closet door.";
        }
        if (Input.GetKeyDown(KeyCode.S))
            currentCorridorState = corridorStates.STAIRS_1;
        else if (Input.GetKeyDown(KeyCode.C))
            currentCorridorState = corridorStates.IN_CLOSET;
    }

    public void stairs1()
    {
        if (clothes)
        {
            storyTelling.text = "This is it, I am going for it. Hope I wont get caught ...\n Press E to exit R to return.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                storyTelling.text = "You escaped successfully from prison congratulations!!";
                currentCorridorState = corridorStates.FINISHED;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                currentCorridorState = corridorStates.CORRIDOR_2;
            }
        }
        else
        {
            storyTelling.text = "I must take the cleaner clothes before I tried to escape ...\n Press R to return.";
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentCorridorState = corridorStates.CORRIDOR_2;
            }
        }
    }
    }



