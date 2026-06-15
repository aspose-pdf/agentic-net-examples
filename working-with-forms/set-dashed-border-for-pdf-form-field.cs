using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing a form field
        const string inputPath = "input.pdf";
        // Output PDF with the modified field border
        const string outputPath = "output.pdf";
        // Name of the form field whose border style should be changed
        const string fieldName = "MyTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field from the form collection and cast to Field
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Create a new Border object for the field.
            // The Border constructor requires the parent annotation (the field itself).
            Border border = new Border(field)
            {
                // Set the border style to dashed
                Style = BorderStyle.Dashed,
                // Set a reasonable border width (in points)
                Width = 1,
                // Define the dash pattern: 3 points on, 3 points off
                Dash = new Dash(3, 3)
            };

            // Assign the configured border to the field
            field.Border = border;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}