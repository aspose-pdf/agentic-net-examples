using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Name of the form field to update
            const string fieldName = "TimeField";

            // Retrieve the field safely and ensure it is a TextBoxField
            var field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == fieldName) as TextBoxField;
            if (field != null)
            {
                // Current system time formatted as HH:mm:ss
                string time = DateTime.Now.ToString("HH:mm:ss");

                // Set the visible value of the field (acts as the default on load)
                field.Value = time;
                // NOTE: Aspose.Pdf's TextBoxField does not expose a DefaultValue property.
                // The field's current value will be shown when the document is opened.
                // If a reset-to-default behavior is required, it must be implemented via JavaScript
                // attached to the field's OnCalculate action or by using a custom PDF dictionary entry.
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
