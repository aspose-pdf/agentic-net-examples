using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Checked.pdf";
        const string checkBoxName = "Checkbox1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the checkbox field from the document's form collection
            var field = doc.Form[checkBoxName] as CheckboxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Checkbox field '{checkBoxName}' not found.");
                return;
            }

            // Set the checkbox to checked (true)
            field.Checked = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox '{checkBoxName}' set to true. Saved as '{outputPath}'.");
    }
}
