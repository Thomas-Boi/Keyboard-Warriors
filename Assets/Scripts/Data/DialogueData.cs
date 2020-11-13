using System;
using System.Collections;
using System.Collections.Generic;

// holds the data about a dialogue for a specific week/scenario
[Serializable]
public struct DialogueData
{
    public DialogueStruct[] onStartDialogue;
}

// holds the data on a dialogue speaker and what they says
[Serializable]
public struct DialogueStruct
{
    public string speaker;
    public string speech;
}