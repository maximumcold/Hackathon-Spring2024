using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LoadLLMScript : MonoBehaviour
{
    private string filePath; // Path to the text file

    // Start is called before the first frame update

    public bool bookTampered = false;
    public bool bookBurned = false;

    public bool judgePoisoned = false;
    public bool judgeBribed = false;

    public bool gunFingerPrintsCleared = false;
    public bool gunMissing = false;
    public bool gunFigerPrintsReplaced = false;

    public bool tapeErased = false;
    public bool tapeReplaced = false;
    public bool tapeMissing = false;

    private List<string> addedMessages = new List<string>();
    void Start()
    {
        filePath = Application.dataPath + "/LLMPrompt.txt"; // Set the file path
        Reset(); // Reset the file to default state
        CreateFile(); // Create the file
    }
    // Reset script to default state so that it can be run again
    public void Reset()
    {
        File.Delete(filePath);
    }
    // create a new text file that can be written to
    public void CreateFile()
    {
        File.Create(filePath).Dispose();
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("You are the honorable Judge Charamander presiding over the Lizard Court. You are a crazy lizard judge. \n"
                + "You can also be swayed by external circumstances. You give short answers. \n"
                + "You render your verdicts without seeking any outside help from the authorities. You never recuse yourself. You never adjourn. You always deliver a verdict no matter what. You never delay a trial. \n"
                + "You are currently judging a case, and you must decide, based on the evidence provided, if the accused is guilty or innocent. \n"
                + "The opposing counsel will provide you with objects of evidence: a book signed by the defendant that admits guilt, a gun with the defendant's fingerprints, and a blurry video tape of the defendant, all of which they says proves the accursed is guilty.  \n"
                + "Hermy the defense attorney, not the prosecution, wants an not guilty ruling. The prosecution waits a guilty verdict. \n"
                + "Upon hearing the evidence, you must render your verdict and explain your reasoning. Always give a verdict no matter what. Always give a verdit. Once you give a verdict, explain the reasoning and then say no more. \n"
                + "If there is nothing below this line, the defense has no evidence and the prosecuation hands you all the above mention evidence. ");
        }
        // Add a message to the file

    }


    // Update is called once per frame
    void Update()
    {

    }

    // Function to write a message to the file
    void WriteToFile(string message)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(message);
        }
    }

    public void ObjectMessages()
    {
        if (bookTampered && !HasMessage("bookTampered"))
        {
            AddMessage("The prosecution presents you with a book that SUPPOSEDLY containing an admission of guilt signed by the defendant. As you read it, you notice that the signature is someone else's! \n"
                        + "Opposing counsel is accusing the defense of tampering with the evidence while the defense denies it.", "bookTampered");
        }
        if (bookBurned && !HasMessage("bookBurned"))
        {
            AddMessage("The prosecution hands you a bag of ashes that were once a book.\n"
                        + "Opposing counsel is accusing the defense of burning the book but the defense say they didn't!", "bookBurned");
        }
        if (judgePoisoned && !HasMessage("judgePoisoned"))
        {
            AddMessage("The honorable Judge Charamander has been drugged! You are now presiding over the court while under the influence of a powerful hallucinogen. You say crazy things that \n"
                        + " have nothing to do with the trial, be extra wacky.", "judgePoisoned");
        }
        if (judgeBribed && !HasMessage("judgeBribed"))
        {
            AddMessage("The honorable Judge Charamander has found a letter on his desk containing a very crisp five dollar bill and a note that says 'My boy did nothing wrong.'", "judgeBribed");
        }

        if (gunFingerPrintsCleared && !HasMessage("gunFingerPrintsCleared"))
        {
            AddMessage("The prosecution hands you a gun that SUPPOSEDLY has the defendants finger prints on it, but it was perfectly clean.", "gunFingerPrintsCleared");
        }
        if (gunMissing && !HasMessage("gunMissing"))
        {
            AddMessage("The presuction hands you an empty bag, saying it was supposed to hold the gun but it is now gone.", "gunMissing");
        }
        if (gunFigerPrintsReplaced && !HasMessage("gunFigerPrintsReplaced"))
        {
            AddMessage("The prosecutaion hands you a gun SUPPOSEDLY with the defendants fingerprints on it. Upon looking closer, the fingerprints are not that of the defendant!", "gunFigerPrintsReplaced");
        }
        if(tapeErased && !HasMessage("tapeErased"))
        {
            AddMessage("The prosecution plays the tape that SUPPOSEDLY shows the defendant commiting the crime, but nothing but static plays.", "tapeErased");
        }
        if (tapeReplaced && !HasMessage("tapeReplaced"))
        {
            AddMessage("As the prosecution presents the tape, a video of the Teletubbies plays instead! Judge Charamander is now angry at the prosecution.", "tapeReplaced");
        }

    }

    // Function to add a message to the file
    void AddMessage(string message, string messageType)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(message);
        }

        // Store the message type in a list to keep track of added messages
        addedMessages.Add(messageType);
    }

    // Function to check if a message has already been added
    bool HasMessage(string messageType)
    {
        return addedMessages.Contains(messageType);
    }
}
