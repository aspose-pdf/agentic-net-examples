using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for Field class

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

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Iterate over all form fields in the document.
        // The Form collection contains Field objects.
        foreach (Field field in pdfDocument.Form.Fields)
        {
            // PageIndex is zero‑based. Page 2 => index 1.
            if (field.PageIndex == 1)
            {
                // Get the current rectangle that defines the field's position.
                Aspose.Pdf.Rectangle rect = field.Rect;

                // Shift the field 5 points to the right.
                rect.LLX += 5; // lower‑left X
                rect.URX += 5; // upper‑right X (keep width unchanged)

                // Assign the modified rectangle back to the field.
                field.Rect = rect;
            }
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Fields on page 2 moved 5 points right. Saved to '{outputPath}'.");
    }
}
