using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_label.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the form field label
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            // Rotate the rectangle by 45 degrees
            rect.Rotate(45);

            // Create a text box field (used as a label) with the rotated rectangle
            TextBoxField labelField = new TextBoxField(page, rect);
            labelField.PartialName = "LabelField";
            labelField.Value = "Rotated Label";
            // Make the field read‑only so it behaves like a static label
            labelField.ReadOnly = true;

            // Add the field to the document's form collection
            doc.Form.Add(labelField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved rotated label PDF to '{outputPath}'.");
    }
}