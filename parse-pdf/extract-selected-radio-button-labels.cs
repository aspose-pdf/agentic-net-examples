using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to hold field full name -> selected option label (nullable)
            var radioSelections = new Dictionary<string, string?>();

            // Iterate over all form fields
            foreach (Field field in doc.Form)
            {
                // Process only radio button fields
                if (field is RadioButtonField radio)
                {
                    // Selected index is 1‑based; 0 means no selection
                    int selectedIndex = radio.Selected;
                    if (selectedIndex > 0 && selectedIndex <= radio.Options.Count)
                    {
                        // Retrieve the display label of the selected option.
                        // In Aspose.Pdf the label is stored in the Option.Value property.
                        string selectedLabel = radio.Options[selectedIndex - 1].Value;
                        radioSelections[radio.FullName] = selectedLabel;
                    }
                    else
                    {
                        // No option selected
                        radioSelections[radio.FullName] = null;
                    }
                }
            }

            // Output the mapping
            foreach (var kvp in radioSelections)
            {
                string valueDisplay = kvp.Value ?? "(none)";
                Console.WriteLine($"Radio field '{kvp.Key}' selected option: {valueDisplay}");
            }
        }
    }
}
