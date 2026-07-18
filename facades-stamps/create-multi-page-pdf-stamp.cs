using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the target PDF, the output PDF and the stamp PDFs
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        string[] stampPdfs = { "stamp1.pdf", "stamp2.pdf", "stamp3.pdf" };

        // Validate existence of files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        foreach (string sp in stampPdfs)
        {
            if (!File.Exists(sp))
            {
                Console.Error.WriteLine($"Stamp file not found: {sp}");
                return;
            }
        }

        // Initialize the facade and bind the target document
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        try
        {
            // Bind the source PDF that will receive the stamps
            fileStamp.BindPdf(inputPdf);

            // Create a stamp for each PDF file and add it to the facade
            foreach (string stampPath in stampPdfs)
            {
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

                // Use the first page of the stamp PDF as the stamp content
                stamp.BindPdf(stampPath, 1);

                // Place the stamp behind existing page content
                stamp.IsBackground = true;

                // Optional: set origin (lower‑left corner) and other properties
                stamp.SetOrigin(0, 0);
                // stamp.SetImageSize(width, height);
                // stamp.Opacity = 0.8f;

                // Add the configured stamp to the file stamp object
                fileStamp.AddStamp(stamp);
            }

            // Save the resulting PDF with all stamps applied
            fileStamp.Save(outputPdf);
        }
        finally
        {
            // Ensure resources are released and the output file is finalized
            fileStamp.Close();
        }

        Console.WriteLine($"Multi‑page stamp applied. Output saved to '{outputPdf}'.");
    }
}