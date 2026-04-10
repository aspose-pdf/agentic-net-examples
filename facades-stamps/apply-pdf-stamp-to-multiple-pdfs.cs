using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to be stamped
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where stamped PDFs will be saved
        const string outputFolder = @"C:\StampedPdfs";
        // Path to the PDF file that will be used as a stamp (first page will be the stamp)
        const string stampPdfPath = @"C:\Stamp\stamp.pdf";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the stamp file exists
        if (!File.Exists(stampPdfPath))
        {
            Console.Error.WriteLine($"Stamp file not found: {stampPdfPath}");
            return;
        }

        // Process each PDF file in the input directory
        foreach (string inputFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Initialize the PdfFileStamp facade
                PdfFileStamp fileStamp = new PdfFileStamp();

                // Bind the source PDF
                fileStamp.BindPdf(inputFile);

                // Create a stamp object and bind the first page of the stamp PDF
                Stamp stamp = new Stamp();
                stamp.BindPdf(stampPdfPath, 1); // use page 1 as the stamp
                stamp.IsBackground = true;      // place stamp behind page content
                stamp.Opacity = 0.5f;           // semi‑transparent

                // Add the stamp to the document
                fileStamp.AddStamp(stamp);

                // Determine the output file path (preserve original file name)
                string outputFile = Path.Combine(outputFolder, Path.GetFileName(inputFile));

                // Save the stamped PDF
                fileStamp.Save(outputFile);

                // Close the facade (writes changes and releases resources)
                fileStamp.Close();

                Console.WriteLine($"Stamped PDF saved: {outputFile}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
            }
        }
    }
}