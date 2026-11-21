using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDF files that will form the multi‑page stamp
        string[] stampFiles = { "stamp1.pdf", "stamp2.pdf", "stamp3.pdf" };
        // Path to the PDF that will receive the stamp
        string targetPdf = "target.pdf";
        // Path for the final stamped PDF
        string outputPdf = "stamped_output.pdf";

        // Verify that all source files exist before proceeding
        foreach (var file in stampFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Stamp file not found: {file}");
                return;
            }
        }
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdf}");
            return;
        }

        // Bind the target PDF to which the stamps will be applied
        using (PdfFileStamp pdfStamp = new PdfFileStamp())
        {
            pdfStamp.BindPdf(targetPdf);

            // Apply each stamp PDF to the corresponding page of the target PDF
            // (Assumes the number of stamp files equals the number of pages to stamp)
            for (int i = 0; i < stampFiles.Length; i++)
            {
                // Create a Stamp object for the current stamp PDF page
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                // Bind the first page of the stamp PDF (each stamp file is a single‑page PDF)
                stamp.BindPdf(stampFiles[i], 1);
                // Apply this stamp to the (i+1)th page of the target document
                stamp.Pages = new[] { i + 1 };
                // Position the stamp at the lower‑left corner (0,0) – adjust as needed
                stamp.SetOrigin(0, 0);
                // Optional: make the stamp appear on top of existing content
                stamp.IsBackground = false;
                // Add the configured stamp to the PdfFileStamp instance
                pdfStamp.AddStamp(stamp);
            }

            // Save the resulting PDF
            pdfStamp.Save(outputPdf);
        }

        Console.WriteLine($"Multi‑page stamp applied successfully. Output saved to '{outputPdf}'.");
    }
}
