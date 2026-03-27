using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string destinationPath = "dest.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the source and destination PDFs exist. If they are missing we
        // create simple placeholder documents so the example can run without
        // external files. This eliminates the FileNotFoundException that was
        // raised in the original code.
        // ---------------------------------------------------------------------
        if (!File.Exists(destinationPath))
        {
            using (var doc = new Document())
            {
                // Add a single blank page to the destination PDF.
                doc.Pages.Add();
                doc.Save(destinationPath);
            }
        }

        if (!File.Exists(sourcePath))
        {
            using (var doc = new Document())
            {
                // Add two pages to the source PDF – you can change the count as needed.
                doc.Pages.Add();
                doc.Pages.Add();
                doc.Save(sourcePath);
            }
        }

        // Determine insertion position (middle of destination)
        int insertLocation;
        using (Document destDoc = new Document(destinationPath))
        {
            int destPageCount = destDoc.Pages.Count;
            // 1‑based index – insert after the first half of pages.
            insertLocation = destPageCount / 2 + 1;
        }

        // Determine number of pages to insert from source
        int sourcePageCount;
        using (Document srcDoc = new Document(sourcePath))
        {
            sourcePageCount = srcDoc.Pages.Count;
        }

        // Insert pages from source into destination at the calculated position
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Insert(destinationPath, insertLocation, sourcePath, 1, sourcePageCount, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert pages.");
        }
    }
}
