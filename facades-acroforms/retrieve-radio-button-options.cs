using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "RadioGroup";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        // Bind the PDF document to the Form object
        Form form = new Form(inputPath);

        // Retrieve the option values dictionary for the specified radio button group
        Dictionary<string, string> optionDictionary = form.GetButtonOptionValues(fieldName);

        // Transfer the dictionary entries into a Hashtable
        Hashtable optionValues = new Hashtable();
        foreach (KeyValuePair<string, string> entry in optionDictionary)
        {
            optionValues.Add(entry.Key, entry.Value);
        }

        // Example: display the retrieved options
        foreach (object key in optionValues.Keys)
        {
            Console.WriteLine($"{key} = {optionValues[key]}");
        }
    }
}