using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchImageStamp
{
    static void Main()
    {
        // Directory containing the PDFs to process
        const string inputDirectory = @"C:\PdfInput";
        // Directory where stamped PDFs will be saved
        const string outputDirectory = @"C:\PdfOutput";
        // Path to the image that will be used as a stamp
        const string stampImagePath = @"C:\Images\stamp.png";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Validate the stamp image exists
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Prepare output file name
                string outputPath = Path.Combine(
                    outputDirectory,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_stamped.pdf");

                // Initialize the PdfFileStamp facade (no using – it does not implement IDisposable)
                PdfFileStamp fileStamp = new PdfFileStamp();

                // Bind the source PDF
                fileStamp.BindPdf(pdfPath);

                // Create a stamp object (fully qualified to avoid ambiguity)
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

                // Bind the image that will be used as the stamp
                stamp.BindImage(stampImagePath);

                // Apply a rotation of 45 degrees (use the Rotation property)
                stamp.Rotation = 45f;

                // Optional visual settings
                stamp.Opacity = 0.5f;          // 50% transparent
                stamp.IsBackground = true;    // place stamp behind page content

                // Restrict the stamp to the first page only (1‑based indexing)
                stamp.Pages = new int[] { 1 };

                // Add the configured stamp to the PDF
                fileStamp.AddStamp(stamp);

                // Save the result to the output path
                fileStamp.Save(outputPath);

                // Close the facade to release resources
                fileStamp.Close();

                Console.WriteLine($"Stamped PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
