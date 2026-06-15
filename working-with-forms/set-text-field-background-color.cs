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
        const string fieldName  = "MyTextField"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the text field by its name
            TextBoxField textField = doc.Form[fieldName] as TextBoxField;
            if (textField == null)
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                return;
            }

            // Set a light‑gray background color.
            // TextBoxField does not expose a BackgroundColor property; use the generic Color property
            // which controls the field's fill (background) appearance.
            textField.Color = Aspose.Pdf.Color.LightGray;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color set to LightGray for field '{fieldName}'. Saved to '{outputPath}'.");
    }
}
