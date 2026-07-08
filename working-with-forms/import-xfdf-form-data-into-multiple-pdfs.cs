using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the XFDF (XML) file that contains the form data to be imported.
        const string xfdfPath = "formData.xfdf";

        // List of PDF files that should receive the same form data.
        string[] pdfInputPaths = { "document1.pdf", "document2.pdf", "document3.pdf" };

        // Output directory for the updated PDFs.
        const string outputDir = "SyncedPdfs";
        Directory.CreateDirectory(outputDir);

        // Verify that the XFDF file exists.
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Process each PDF file.
        foreach (string inputPath in pdfInputPaths)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"PDF file not found: {inputPath}");
                continue;
            }

            try
            {
                // Open the PDF document inside a using block for deterministic disposal.
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Import the form field values (and any associated annotations) from the XFDF file.
                    // This method works with the core Aspose.Pdf API; no Facades are required.
                    pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Build the output file path (same name, different folder).
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

                    // Save the updated PDF.
                    pdfDoc.Save(outputPath);
                    Console.WriteLine($"Synchronized PDF saved to '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}