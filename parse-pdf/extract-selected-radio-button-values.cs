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
            // Dictionary to hold field name -> selected option label
            var selectedRadioValues = new Dictionary<string, string>();

            // Iterate over all form fields
            foreach (Field field in doc.Form)
            {
                // Check if the field is a radio button group
                if (field is RadioButtonField radio)
                {
                    // Selected index is 1‑based; 0 means no selection
                    int selectedIndex = radio.Selected;
                    if (selectedIndex > 0 && selectedIndex <= radio.Options.Count)
                    {
                        // Options collection returns Option objects; use the Name property for the label
                        string selectedLabel = radio.Options[selectedIndex - 1].Name;
                        selectedRadioValues[radio.FullName] = selectedLabel;
                    }
                    else
                    {
                        // No option selected
                        selectedRadioValues[radio.FullName] = "(none)";
                    }
                }
            }

            // Output the mapping
            Console.WriteLine("Selected Radio Button Values:");
            foreach (var kvp in selectedRadioValues)
            {
                Console.WriteLine($"Field: {kvp.Key} => Selected Option: {kvp.Value}");
            }
        }
    }
}
