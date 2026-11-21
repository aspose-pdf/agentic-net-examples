using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // needed for Field and Rectangle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_moved_fields.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Offset to apply to each form field (points)
            const double offsetX = 10.0;
            const double offsetY = 15.0;

            // Iterate over all form fields and adjust their rectangle positions
            foreach (Field field in pdfDoc.Form)
            {
                // The field rectangle is an Aspose.Pdf.Rectangle
                Rectangle rect = field.Rect;

                // Create a new rectangle shifted by the offset
                Rectangle newRect = new Rectangle(
                    rect.LLX + offsetX,
                    rect.LLY + offsetY,
                    rect.URX + offsetX,
                    rect.URY + offsetY);

                // Assign the updated rectangle back to the field
                field.Rect = newRect;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Form fields moved and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
