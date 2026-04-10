using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;   // Form, FormEditor, etc.

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with form fields
        const string outputPdf = "renamed_output.pdf"; // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF as a Form facade (provides field enumeration and rename capability)
        using (Form form = new Form(inputPdf))
        {
            // Collect all field names
            string[] allFieldNames = form.FieldNames;

            // Dictionary to track how many times a base name has appeared
            var nameCounters = new Dictionary<string, int>(StringComparer.Ordinal);

            foreach (string originalName in allFieldNames)
            {
                // If this name has been seen before, we need to rename it
                if (nameCounters.ContainsKey(originalName))
                {
                    // Increment the counter for this base name
                    int index = ++nameCounters[originalName];

                    // Build a new unique name (e.g., "fieldName_1", "fieldName_2", ...)
                    string newName = $"{originalName}_{index}";

                    // Perform the rename operation
                    form.RenameField(originalName, newName);
                }
                else
                {
                    // First occurrence – store it with an initial counter of 0
                    nameCounters[originalName] = 0;
                }
            }

            // Save the modified PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"Renamed PDF saved to '{outputPdf}'.");
    }
}