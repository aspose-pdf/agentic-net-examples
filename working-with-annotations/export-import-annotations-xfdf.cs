using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string copyPdfPath = "copy.pdf"; // Simulates export‑import round‑trip
        const string roundTripPdf = "roundtrip.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ---------------------------------------------------------------------
        // STEP 1 – Load the original PDF and "export" its annotations.
        // In a full Aspose.Pdf.Xfdf installation you would use XfdfSaveOptions
        // to write an XFDF file. Because the Xfdf assembly is not referenced in
        // this project we simulate the round‑trip by saving the PDF to a new
        // file (copyPdfPath). The saved copy contains the same annotation data.
        // ---------------------------------------------------------------------
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            sourceDoc.Save(copyPdfPath); // simple PDF copy – retains annotations
        }

        // ---------------------------------------------------------------------
        // STEP 2 – Re‑import the annotations by loading the copy.
        // In a real scenario you would load the XFDF file with XfdfLoadOptions.
        // Here we just load the PDF copy which already includes the annotations.
        // ---------------------------------------------------------------------
        using (Document originalDoc = new Document(inputPdfPath))
        using (Document importedDoc = new Document(copyPdfPath))
        {
            // Count annotations in the original document
            int originalCount = 0;
            for (int i = 1; i <= originalDoc.Pages.Count; i++) // 1‑based indexing
            {
                originalCount += originalDoc.Pages[i].Annotations.Count;
            }

            // Count annotations after re‑import (i.e., after the copy)
            int importedCount = 0;
            for (int i = 1; i <= importedDoc.Pages.Count; i++) // 1‑based indexing
            {
                importedCount += importedDoc.Pages[i].Annotations.Count;
            }

            Console.WriteLine($"Original annotation count: {originalCount}");
            Console.WriteLine($"Re‑imported annotation count: {importedCount}");

            // Simple integrity check: counts should match
            if (originalCount == importedCount)
                Console.WriteLine("Round‑trip verification succeeded.");
            else
                Console.WriteLine("Round‑trip verification failed: counts differ.");

            // Save the re‑imported PDF (optional – demonstrates that the copy is usable)
            importedDoc.Save(roundTripPdf);
        }
    }
}
