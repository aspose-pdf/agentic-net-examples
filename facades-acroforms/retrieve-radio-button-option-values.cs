using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF containing the radio button group
        const string radioFieldName = "RadioGroup1";   // Name of the radio button field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Form is a Facade for working with AcroForm fields.
        // It implements IDisposable, so wrap it in a using block.
        using (Form form = new Form())
        {
            // Bind the PDF document to the Form object.
            form.BindPdf(inputPath);

            // Retrieve all option values of the specified radio button group.
            // GetButtonOptionValues returns a Dictionary<string,string> where the key is the option name
            // and the value is the export value.
            Dictionary<string, string> optionValues = form.GetButtonOptionValues(radioFieldName);

            // Store the values in a Hashtable (key = index, value = option export string).
            Hashtable optionsTable = new Hashtable();
            int index = 0;
            foreach (KeyValuePair<string, string> kvp in optionValues)
            {
                optionsTable[index++] = kvp.Value; // you could also store kvp.Key if you need the option name
            }

            // Example: display the collected options.
            foreach (DictionaryEntry entry in optionsTable)
            {
                Console.WriteLine($"Option {entry.Key}: {entry.Value}");
            }

            // No explicit UnbindPdf call is required – disposing the Form object releases resources.
        }
    }
}
