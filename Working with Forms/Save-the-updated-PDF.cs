using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Rectangle used by PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor with the loaded document
            PdfContentEditor editor = new PdfContentEditor(doc);

            // Define the annotation rectangle (x, y, width, height)
            // Example: lower‑left (100, 500), upper‑right (300, 550)
            int llx = 100, lly = 500, urx = 300, ury = 550;
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                llx,                     // X (lower‑left)
                lly,                     // Y (lower‑left)
                urx - llx,               // Width
                ury - lly);              // Height

            // Add a free‑text annotation on page 1
            editor.CreateFreeText(rect, "Sample annotation", 1);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}