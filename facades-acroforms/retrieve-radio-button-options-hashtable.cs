using System;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade for the PDF document
        using (Form form = new Form(pdfPath))
        {
            // Retrieve radio button options as a Dictionary<string, string>
            Dictionary<string, string> optionsDict = form.GetButtonOptionValues("RadioGroupName");

            // Transfer the dictionary entries into a Hashtable
            Hashtable optionsTable = new Hashtable();
            foreach (KeyValuePair<string, string> kvp in optionsDict)
            {
                optionsTable.Add(kvp.Key, kvp.Value);
            }

            // Example usage: display the option values
            foreach (DictionaryEntry entry in optionsTable)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }
        }
    }
}