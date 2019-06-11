using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class KeywordManager : Singleton<KeywordManager>
{
    public Dictionary<string, Keyword> keywordTable;
    // Start is called before the first frame update
    void Start()
    {
        keywordTable = new Dictionary<string, Keyword>();
    }

    public bool checkid(string id)
    {
        return (keywordTable.ContainsKey(id));
    }

    public void addKeyword(Keyword keyword)
    {
        if (!checkid(keyword.name))
        {
            keywordTable.Add(keyword.name, keyword);
        }
    }

    public void deleteKeyword(string name)
    {
        if (checkid(name))
        {
            keywordTable.Remove(name);
        }
    }

 
}

public class Keyword
{
    public string name;

    public Keyword(string name)
    {
        this.name = name;
    }
}
