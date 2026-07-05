using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Logo";

        // Desired size of the field (width and height in points)
        const float fieldWidth  = 100f; // e.g., 100 points
        const float fieldHeight = 50f;  // e.g., 50 points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document only to obtain the height of the first page.
        // This is needed because PDF coordinates start from the lower‑left corner.
        using (Document doc = new Document(inputPath))
        {
            // Height of the first page (in points)
            double pageHeight = doc.Pages[1].PageInfo.Height;

            // Calculate rectangle coordinates for the top‑left corner.
            // Lower‑left (llx, lly) and upper‑right (urx, ury) as required by MoveField.
            float llx = 0f;                                 // left edge
            float lly = (float)(pageHeight - fieldHeight); // bottom edge (page top minus height)
            float urx = fieldWidth;                         // right edge
            float ury = (float)pageHeight;                  // top edge

            // Use FormEditor (Facades API) to reposition the field.
            // FormEditor works directly with file paths, so we pass input and output files.
            FormEditor formEditor = new FormEditor(inputPath, outputPath);
            bool moved = formEditor.MoveField(fieldName, llx, lly, urx, ury);

            if (!moved)
            {
                Console.Error.WriteLine($"Failed to move field '{fieldName}'.");
            }

            // Persist changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Field '{fieldName}' moved to top‑left corner and saved as '{outputPath}'.");
    }
}