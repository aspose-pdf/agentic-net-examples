using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for form field classes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_field.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the text field will appear (coordinates in points)
            // Lower‑left (llx, lly) = (100, 500), Upper‑right (urx, ury) = (300, 530)
            Rectangle fieldRect = new Rectangle(100, 500, 300, 530);

            // Create a text box field on the first page
            TextBoxField textField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "MyTextField",
                Value = "Enter text" // default value shown in the field
            };

            // Add the field to the document's form collection (page numbers are 1‑based)
            doc.Form.Add(textField, 1);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text field added and saved to '{outputPath}'.");
    }
}