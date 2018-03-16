using System;
using System.IO;

public class InputParser
{
    private File cardData;
    private File boardData;
    private string boardPath = @".\PropertyTycoonBoardData_Converted.csv";
    private string cardPath = @".\PropertyTycoonCardData_Converted.csv";
    private GameData data;

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
        for(int i = 0; i < spaces.Size() + 1; i++)
        {
            if(spaces.Get(i + 1).Get(3)
        }
        data.AddGameSpace()
    }

    void ReadBoardData()
    {
        using (var reader = new StreamReader(boardPath))
        {
            List<List<String>> spaces = new List<List<string>>();
            for(int i = 0; i < 40; i++)
            {
                spaces.add(new List<String>(2));
            }
            SkipLine(reader, 4);
            var lineNo = 1;
            var line = reader.ReadLine();
            while(!reader.EndOfStream)
            {
                var delimited = line.Split(",");
                spaces.Get(lineNo).Add(delimited);
                line = reader.readLine();
                lineNo++;
            }
        }
    }

    void ReadCardData()
    {
        using (var reader = new StreanReader(cardPath))
        {
            List<List<String>> potLuck = new List<List<String>>();
            List<List<String>> knocks = new List<List<String>>();
            for(int i = 0; i < 16; i++)
            {
                potLuck.Add(new List<String>(2));
            }
            for(int i = 0; i <16; i++)
            {
                knocks.Add(new List<String>(2));
            }
            SkipLine(reader, 5);
            var line = reader.readLine();
            while (!line.Equals(",,,"))
            {
                var delimited = line.Split(",");
                potLuck.Get(0).add(delimited[0]);
                potLuck.Get(1).add(delimited[3]);
                line = reader.readLine();
            }
            SkipLine(reader, 3);
            var line = reader.readLine();
            while (!line.Equals(",,,"))
            {
                var delimited = line.Split(",");
                knocks.Get(0).Add(delimited[0]);
                knocks.Get(1).Add(delimited[1]);
                line = reader.readLine();
            }

        }
    }

    Action createPayAction()
    {

    }

    void SkipLine(StreamReader r, int n)
    {
        for (int i = 0; i < n; i++)
        {
            r.ReadLine();
        }
    }
}
