using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the legal disclaimer PDF that will be used as a stamp
        const string disclaimerPath = "disclaimer.pdf";

        // List of target PDF files to which the disclaimer will be applied
        string[] targetPdfs = { "document1.pdf", "document2.pdf", "document3.pdf" };

        // Output directory for stamped PDFs
        const string outputDir = "StampedOutputs";

        // Verify disclaimer PDF exists
        if (!File.Exists(disclaimerPath))
        {
            Console.Error.WriteLine($"Disclaimer PDF not found: {disclaimerPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        foreach (string srcPath in targetPdfs)
        {
            // Verify each source PDF exists before processing
            if (!File.Exists(srcPath))
            {
                Console.Error.WriteLine($"Source PDF not found: {srcPath}");
                continue;
            }

            // Construct output file name (original name + "_stamped.pdf")
            string outPath = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(srcPath) + "_stamped.pdf");

            try
            {
                // Create a PdfFileStamp instance (does NOT implement IDisposable)
                PdfFileStamp fileStamp = new PdfFileStamp();

                // Bind the source PDF document that will receive the stamp
                fileStamp.BindPdf(srcPath);

                // Prepare the stamp that contains the disclaimer PDF page
                Aspose.Pdf.Facades.Stamp disclaimerStamp = new Aspose.Pdf.Facades.Stamp();
                // Bind the disclaimer PDF and specify which page of the disclaimer to use (1‑based)
                disclaimerStamp.BindPdf(disclaimerPath, 1);
                // Apply the stamp only to the first page of the target document
                disclaimerStamp.Pages = new int[] { 1 };
                // Place the stamp in front of existing content (false = overlay, true = background)
                disclaimerStamp.IsBackground = false;
                // Optional: set the origin (lower‑left corner) of the stamp on the page
                // disclaimerStamp.SetOrigin(0, 0);

                // Add the prepared stamp to the PdfFileStamp instance
                fileStamp.AddStamp(disclaimerStamp);

                // Save the stamped document to the output path
                fileStamp.Save(outPath);

                // Release any resources held by the stamp object
                fileStamp.Close();

                Console.WriteLine($"Stamped PDF saved: {outPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{srcPath}': {ex.Message}");
            }
        }
    }
}
