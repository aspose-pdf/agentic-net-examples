using System;
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
            // Iterate over all form fields
            foreach (Field field in doc.Form)
            {
                // Process only radio button fields
                if (field is RadioButtonField radio)
                {
                    // Get the export value of the selected option (empty if none selected)
                    string selectedExportValue = radio.Value ?? string.Empty;

                    // Get the list of option objects (each contains Name and ExportValue)
                    var options = radio.Options;

                    // Determine the label (display name) of the selected option using the Selected index (1‑based)
                    string selectedLabel = string.Empty;
                    if (radio.Selected > 0 && radio.Selected <= options.Count)
                    {
                        // Option.Name holds the visible label for the radio button choice
                        selectedLabel = options[radio.Selected - 1].Name;
                    }

                    // Output the mapping
                    Console.WriteLine($"Radio Button Field: {radio.FullName}");
                    Console.WriteLine($"  Selected Export Value: \"{selectedExportValue}\"");
                    Console.WriteLine($"  Selected Label: \"{selectedLabel}\"");
                    Console.WriteLine();
                }
            }
        }
    }
}
