using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the button field
        const string buttonName = "MyRadioGroup";      // replace with the actual button field name
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF as a Form facade. The constructor binds the document automatically.
        using (Form form = new Form(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Retrieve all option values for the specified button (radio group)
            // -----------------------------------------------------------------
            Dictionary<string, string> optionValues = form.GetButtonOptionValues(buttonName);

            Console.WriteLine($"Option values for button field \"{buttonName}\":");
            foreach (KeyValuePair<string, string> kvp in optionValues)
            {
                // kvp.Key   = export value (the value stored in the PDF)
                // kvp.Value = display caption shown to the user
                Console.WriteLine($"  Export: \"{kvp.Key}\"  Caption: \"{kvp.Value}\"");
            }

            // ---------------------------------------------------------------
            // 2. Get the currently selected option for the button field
            // ---------------------------------------------------------------
            string currentValue = form.GetButtonOptionCurrentValue(buttonName);
            Console.WriteLine($"\nCurrent selected value: \"{currentValue}\"");

            // ---------------------------------------------------------------
            // 3. Example: change the selected option (if at least one exists)
            // ---------------------------------------------------------------
            if (optionValues.Count > 0)
            {
                // Pick the first export value from the dictionary
                string newExportValue = null;
                foreach (string key in optionValues.Keys)
                {
                    newExportValue = key;
                    break;
                }

                if (!string.IsNullOrEmpty(newExportValue))
                {
                    bool filled = form.FillField(buttonName, newExportValue);
                    Console.WriteLine(filled
                        ? $"Button field \"{buttonName}\" set to \"{newExportValue}\"."
                        : $"Failed to set button field \"{buttonName}\".");

                    // Save the modified PDF
                    form.Save(outputPdf);
                    Console.WriteLine($"Modified PDF saved to \"{outputPdf}\".");
                }
            }
        }
    }
}