using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf; // Core API namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document does not contain pages 3 through 6.");
                return;
            }

            // Collect pages 3 through 6 (1‑based indexing)
            List<Aspose.Pdf.Page> pagesToMove = new List<Aspose.Pdf.Page>();
            for (int i = 3; i <= 6; i++)
            {
                pagesToMove.Add(doc.Pages[i]);
            }

            // Insert the collected pages at the end of the document.
            // Insert position is Count + 1 because Insert expects a 1‑based index.
            int insertPosition = doc.Pages.Count + 1;
            doc.Pages.Insert(insertPosition, pagesToMove);

            // Delete the original pages (3‑6). Insertion at the end does not shift earlier indices.
            doc.Pages.Delete(new int[] { 3, 4, 5, 6 });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 moved to the end. Saved as '{outputPath}'.");
    }
}