using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string fieldName = "Color";
        // Expected option names for the radio button group
        string[] expectedOptions = new string[] { "White", "Black", "Red" };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF form
        Form form = new Form(inputPath);
        try
        {
            Dictionary<string, string> optionValues = form.GetButtonOptionValues(fieldName);
            // Verify that the retrieved option keys match the expected set
            bool allMatch = true;
            if (optionValues.Count != expectedOptions.Length)
            {
                allMatch = false;
            }
            else
            {
                foreach (string expected in expectedOptions)
                {
                    if (!optionValues.ContainsKey(expected))
                    {
                        allMatch = false;
                        break;
                    }
                }
            }

            if (allMatch)
            {
                Console.WriteLine("Radio button options match the expected set.");
            }
            else
            {
                Console.WriteLine("Radio button options do NOT match the expected set.");
                Console.WriteLine("Found options:");
                foreach (KeyValuePair<string, string> kvp in optionValues)
                {
                    Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
                }
            }
        }
        finally
        {
            form.Dispose();
        }
    }
}
