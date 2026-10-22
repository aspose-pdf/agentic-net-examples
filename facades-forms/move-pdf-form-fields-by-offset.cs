using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_moved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define the offset to apply to each field
        const double offsetX = 10; // horizontal offset
        const double offsetY = 15; // vertical offset

        // Iterate over each form field and adjust its rectangle
        foreach (Field field in doc.Form.Fields)
        {
            // Get the current rectangle (position) of the field.
            Aspose.Pdf.Rectangle rect = field.Rect;

            // Apply the offset to all sides of the rectangle.
            rect.LLX += offsetX;
            rect.LLY += offsetY;
            rect.URX += offsetX;
            rect.URY += offsetY;

            // Update the field's rectangle with the new coordinates.
            field.Rect = rect;
        }

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"All form fields moved by ({offsetX},{offsetY}) and saved to '{outputPath}'.");
    }
}
