using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Fully qualified field name of the radio button group in the PDF form
        const string radioFieldName = "RadioGroup1";

        // Desired option name (visible to the user) and its export value (used by downstream systems)
        const string optionName   = "OptionA";
        const string exportValue  = "CODE123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the radio button field; cast to RadioButtonField
            RadioButtonField radioField = doc.Form[radioFieldName] as RadioButtonField;
            if (radioField == null)
            {
                Console.Error.WriteLine($"Radio button field '{radioFieldName}' not found.");
                return;
            }

            // Remove any existing options (optional – ensures a clean state)
            // The Options collection is read‑only, so we delete by name
            foreach (var opt in radioField.Options)
            {
                radioField.DeleteOption(opt.Name);
            }

            // Add a new option with the specific export value
            // AddOption(exportValue, optionName) sets both the export value and the visible name
            radioField.AddOption(exportValue, optionName);

            // Select the newly added option (Selected is 1‑based)
            radioField.Selected = 1;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Export value set for '{radioFieldName}' and saved to '{outputPath}'.");
    }
}