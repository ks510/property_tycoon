using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

//using Xamarin.Forms;

namespace PropertyTycoonProject
{

    public class InputParser
{
    //private File cardData;
    //private File boardData;
    private string boardPath = @".\PropertyTycoonBoardData_Converted.csv";
    private string cardPath = @".\PropertyTycoonCardData_Converted.csv";
    private GameData data;
    private List<List<string>> spaces;
    private List<List<string>> potLuck;
    private List<List<string>> knocks;

    public enum spaceType
    {
        Property,
        Card,
        Instruction,
        Jail
    }
    public enum Colors
    {
        Brown,
        Blue,
        Purple,
        Orange,
        Red,
        Yellow,
        Green,
        DeepBlue,
    }

    public InputParser(String boardSheet, String cardSheet)
    {
        this.boardPath = boardSheet;
        this.cardPath = cardSheet;
        this.data = new GameData();
    }

    void Read()
    {
        this.ReadBoardData();
        this.ReadCardData();
        /*
        using (var reader = new StreamReader(boardPath))
        {
            List<List<String>> spaces = new List<List<String>>();

            for (int i = 0; i < 40; i++)
            {
                spaces.Add(new List<String>());
            }
            SkipLine(reader, 4);
            var lineNo = 1;
            var line = reader.ReadLine();
            while (!line.Equals(",,,,,,,,,,,,,,"))
            {
                var delimited = line.Split(",");
                spaces.Get(lineNo).Add(delimited);
                line = reader.ReadLine();
                lineNo++;
            }
        }
        
        using (reader = new StreamReader(cardPath))
        {
            List<List<String>> potLuck = new List<List<String>>();
            List<List<String>> knocks = new List<List<String>>();
            for(int i = 0; i < 16; i++)
            {
                potLuck.Add(new List<String>(2));
            }
            for(int i = 0; i < 16; i++)
            {
                knocks.Add(new List<String>(2));
            }
            SkipLine(reader, 5);
            var line = reader.readLine();
            while(!line.Equals(",,,"))
            {
                var delimited = line.Split(",");
                potLuck.Get(0).Add(delimited[0]);
                potLuck.Get(1).Add(delimited[1]);
                line = reader.readLine();
            }
            SkipLine(reader, 3);
            var line = reader.readLine();
            while(!line.Equals(",,,"))
            {
                var delimited = line.Split(",");
                knocks.Get(0).Add(delimited[0]);
                knocks.Get(1).Add(delimited[1]);
                line = reader.readLine();
            }
        }
        */

        //create space objects
        for (int i = 0; i < this.spaces.Count + 1; i++)
        {
            var group = this.spaces[i + 1][3];
            var action = this.spaces[i + 1][4];
            Color color = Color.FromName(group);
                Color black = Color.FromName("Black");
            if (color.Equals(black)) {
                if (group == "Go to jail")
                {
                    this.data.AddGameSpace(new JailSpace());
                } else if (group.Equals("Station"))
                {
                    this.data.AddGameSpace(new PropertySpace());
                } else if (group.Equals("Utilities"))
                {
                    this.data.AddGameSpace(new PropertySpace());
                }
                else if (group == "")
                {
                    if (action.Equals("Collect £200") || action.Equals("Pay £200") || action.Equals("Pay £100"))
                    {
                        data.AddGameSpace(new InstructionSpace());
                    }
                }
            } else {
                this.data.AddGameSpace(new PropertySpace());
            };
        }

        //create card objects


        //this.data.AddGameSpace();
    }

    void ReadBoardData()
    {
            FileStream fs = new FileStream(boardPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using (var reader = new StreamReader(fs))
        {
            this.spaces = new List<List<string>>();
            for (int i = 0; i < 40; i++)
            {
                this.spaces.Add(new List<String>(2));
            }
            this.SkipLine(reader, 4);
            var lineNo = 1;
            var line = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var delimited = line.Split(',');
                for (int i = 0; i < delimited.Length; i++) {
                    //Debug.WriteLine(delimited[i]);
                }
                this.spaces[lineNo] =new List<string>(delimited);
                line = reader.ReadLine();
                lineNo++;
            }
        }
    }

    void ReadCardData()
    {
            //File cardFile = new File(cardPath);
            FileStream fs = new FileStream(cardPath, FileMode.Open, FileAccess.Read, FileShare.Read );
        using (var reader = new StreamReader(fs))
        {
            this.potLuck = new List<List<String>>();
            this.knocks = new List<List<String>>();
            for (int i = 0; i < 16; i++)
            {
                this.potLuck.Add(new List<String>(2));
            }
            for (int i = 0; i < 16; i++)
            {
                this.knocks.Add(new List<String>(2));
            }
            this.SkipLine(reader, 5);
            var line1 = reader.ReadLine();
            while (!line1.Equals(",,,"))
            {
                var delimited = line1.Split(',');
                this.potLuck[0].Add(delimited[0]);
                this.potLuck[1].Add(delimited[3]);
                line1 = reader.ReadLine();
            }
            this.SkipLine(reader, 3);
            var line2 = reader.ReadLine();
            while (!line2.Equals(",,,"))
            {
                var delimited = line2.Split(',');
                this.knocks[0].Add(delimited[0]);
                this.knocks[1].Add(delimited[1]);
                line2 = reader.ReadLine();
            }

        }
    }

    public List<List<string>> getRawSpaces() 
    {
        return this.spaces;
    }

    public List<List<string>> getRawPotLuck()
    {
        return this.potLuck;
    }

    public List<List<string>> getRawKnocks()
    {
        return this.knocks;
    }

    public GameData getData()
    {
        return this.data;
    }

    Action createPayAction()
    {
            return null;
    }

    private void SkipLine(StreamReader r, int n)
    {
        for (int i = 0; i < n; i++)
        {
            r.ReadLine();
        }
    }
}

}
