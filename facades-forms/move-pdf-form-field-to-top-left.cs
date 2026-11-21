using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the first page (1‑based indexing) and its height
            Aspose.Pdf.Page firstPage = doc.Pages[1];
            // PageInfo.Height returns double – cast to float for Aspose.Pdf APIs that expect float
            float pageHeight = (float)firstPage.PageInfo.Height;

            // Desired dimensions for the "Logo" field
            float fieldWidth = 150f;   // example width in points
            float fieldHeight = 50f;   // example height in points

            // Calculate coordinates for the top‑left corner of the page
            // PDF origin is bottom‑left, so Y coordinate is pageHeight - fieldHeight
            float llx = 0f;                         // lower‑left X
            float lly = pageHeight - fieldHeight;   // lower‑left Y
            float urx = fieldWidth;                 // upper‑right X
            float ury = pageHeight;                 // upper‑right Y

            // Move the existing form field named "Logo" to the calculated rectangle
            FormEditor editor = new FormEditor();
            editor.BindPdf(doc);
            editor.MoveField("Logo", llx, lly, urx, ury);

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field 'Logo' moved to top‑left corner and saved as '{outputPath}'.");
    }
}
