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
        const string fieldName = "Logo";

        // Desired size of the field (points)
        const float fieldWidth = 150f;   // replace with required width
        const float fieldHeight = 50f;   // replace with required height

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Page indexing is 1‑based
            Page firstPage = doc.Pages[1];

            // Height of the page (points)
            float pageHeight = (float)firstPage.PageInfo.Height;

            // Calculate rectangle for top‑left placement
            float llx = 0f;                     // left edge
            float lly = pageHeight - fieldHeight; // lower‑left Y (top edge minus height)
            float urx = llx + fieldWidth;       // right edge
            float ury = pageHeight;             // upper‑right Y (top edge)

            // Move the field using FormEditor
            using (FormEditor formEditor = new FormEditor(doc))
            {
                bool moved = formEditor.MoveField(fieldName, llx, lly, urx, ury);
                if (!moved)
                {
                    Console.Error.WriteLine($"Failed to move field '{fieldName}'.");
                }

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field '{fieldName}' repositioned and saved to '{outputPath}'.");
    }
}