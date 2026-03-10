using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF and image stamp files
        const string inputPdfPath  = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF into a memory stream (in‑memory processing)
        byte[] pdfBytes = File.ReadAllBytes(inputPdfPath);
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        // Initialize PdfFileStamp facade
        using (Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp())
        {
            // Bind the PDF stream to the facade
            fileStamp.BindPdf(pdfStream);

            // Create a generic stamp object
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the image that will be used as the stamp
            stamp.BindImage(stampImagePath);

            // Set stamp to be placed on top (overlay) of the page content
            stamp.IsBackground = false;

            // Specify the pages that should receive the stamp (1‑based indexing)
            // Example: apply stamp to pages 2, 4 and 5
            stamp.Pages = new int[] { 2, 4, 5 };

            // Add the configured stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the resulting PDF to the output file
            fileStamp.Save(outputPdfPath);

            // Close the facade (releases resources)
            fileStamp.Close();
        }

        Console.WriteLine($"Image stamps applied. Output saved to '{outputPdfPath}'.");
    }
}