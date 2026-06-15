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
        const string fieldName = "myLabel";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name (assumed to be a TextBoxField)
            var field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or not a TextBoxField.");
                return;
            }

            // Rotate the field's rectangle by 45 degrees
            // Rectangle.Rotate(int) accepts an angle in degrees (0‑360)
            field.Rect.Rotate(45);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved rotated label PDF to '{outputPath}'.");
    }
}