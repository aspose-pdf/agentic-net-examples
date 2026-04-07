using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // PDF containing the radio button group
        const string outputPath = "output.pdf";     // PDF after setting the export value
        const string fieldName  = "RadioGroup1";   // Fully qualified name of the radio button field
        const string optionName = "OptionA";       // Visible name of the option (as shown in the UI)
        const string exportCode = "CODE123";       // Export value required by downstream systems

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form collection
            Form form = doc.Form;

            // Retrieve the radio button field by its name
            if (form[fieldName] is RadioButtonField radioField)
            {
                // OPTIONAL: Remove an existing option with the same visible name
                // (prevents duplicate entries if the option already exists)
                try { radioField.DeleteOption(optionName); } catch { /* ignore if not present */ }

                // Add a new option with the desired export value and visible name
                // The first parameter is the export value, the second is the option's display name
                radioField.AddOption(exportCode, optionName);

                // OPTIONAL: Select the newly added option (Selected is 1‑based)
                // radioField.Selected = radioField.Count; // selects the last added option
            }
            else
            {
                Console.Error.WriteLine($"Radio button field '{fieldName}' not found or is not a RadioButtonField.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Export value set and PDF saved to '{outputPath}'.");
    }
}