using System;
using System.Collections;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // Path to the PDF containing the radio button group
        const string fieldName = "Color";        // Name of the radio button group to query

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF as a Form facade (implements IDisposable)
        using (Form form = new Form(pdfPath))
        {
            // Get the option values for the specified radio button group.
            // The method returns a Dictionary<string,string>.
            var dict = form.GetButtonOptionValues(fieldName);

            // Convert the generic dictionary to a non‑generic Hashtable as required.
            Hashtable values = new Hashtable();
            foreach (var kvp in dict)
            {
                values.Add(kvp.Key, kvp.Value);
            }

            // Example: iterate over the Hashtable and display each option.
            foreach (DictionaryEntry entry in values)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }

            // No saving is needed because we only read form data.
        }
    }
}