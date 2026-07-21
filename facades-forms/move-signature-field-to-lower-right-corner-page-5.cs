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
        const string fieldName  = "Signature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document to obtain page dimensions (page 5 is 1‑based)
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            Page page = doc.Pages[5];
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Desired size of the signature field (exact dimensions)
            const double fieldWidth  = 200; // points
            const double fieldHeight = 100; // points
            const double margin      = 10;  // distance from page edges

            // Calculate lower‑right corner rectangle
            double llx = pageWidth  - fieldWidth - margin; // lower‑left X
            double lly = margin;                           // lower‑left Y
            double urx = pageWidth  - margin;              // upper‑right X
            double ury = margin + fieldHeight;             // upper‑right Y

            // Move the existing signature field to the new rectangle
            FormEditor formEditor = new FormEditor(inputPath, outputPath);
            bool moved = formEditor.MoveField(fieldName,
                                              (float)llx,
                                              (float)lly,
                                              (float)urx,
                                              (float)ury);

            if (!moved)
            {
                Console.Error.WriteLine($"Failed to move field '{fieldName}'.");
                return;
            }

            // Persist changes
            formEditor.Save();
            Console.WriteLine($"Field '{fieldName}' moved to lower right corner of page 5 and saved to '{outputPath}'.");
        }
    }
}