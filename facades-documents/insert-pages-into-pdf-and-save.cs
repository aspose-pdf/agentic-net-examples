using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";      // PDF to be inserted
        const string destinationPdfPath = "destination.pdf"; // PDF that will receive the insertion
        const string outputPdfPath = "merged_output.pdf";   // Result file

        // Verify that both input files exist
        if (!File.Exists(sourcePdfPath) || !File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine("Source or destination PDF not found.");
            return;
        }

        // Load both PDFs using the high‑level Document API (PdfContentEditor does not expose InsertPage in recent versions)
        using (Document destDoc = new Document(destinationPdfPath))
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // Append each page from the source document to the destination document.
            // Adding the page directly copies its content, so the source document can be disposed afterwards.
            foreach (Page srcPage in srcDoc.Pages)
            {
                destDoc.Pages.Add(srcPage);
            }

            // Save the merged PDF to the desired output path.
            destDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Pages from '{sourcePdfPath}' have been inserted into '{destinationPdfPath}'.");
        Console.WriteLine($"Result saved as '{outputPdfPath}'.");
    }
}
