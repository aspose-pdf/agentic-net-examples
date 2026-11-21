using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";      // input PDF containing the form
        const string fieldName = "RadioGroup1"; // name of the radio button group

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create a Form object that works with the loaded document
            Form form = new Form(doc);

            // Retrieve all option values of the specified radio button group
            // GetButtonOptionValues returns a Dictionary<string,string>
            Dictionary<string, string> optionDict = form.GetButtonOptionValues(fieldName);

            // Store the values in a Hashtable (key = option value, value = its index)
            Hashtable optionTable = new Hashtable();
            int index = 0;
            foreach (KeyValuePair<string, string> kvp in optionDict)
            {
                // kvp.Value is the option's export value (or display text depending on PDF)
                optionTable[kvp.Value] = index++;
            }

            // Demonstrate the contents of the Hashtable
            Console.WriteLine($"Radio button group '{fieldName}' contains {optionDict.Count} options:");
            foreach (DictionaryEntry entry in optionTable)
            {
                Console.WriteLine($"  Value: '{entry.Key}'  Index: {entry.Value}");
            }
        }
    }
}
