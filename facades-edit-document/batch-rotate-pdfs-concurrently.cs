using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to process
        const string inputDirectory = @"C:\InputPdfs";
        // Output directory where processed PDFs will be saved
        const string outputDirectory = @"C:\OutputPdfs";

        // Verify that the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return; // Exit gracefully – no unhandled exception
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input directory.");
            return;
        }

        // Process each PDF concurrently
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Derive output file name
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileName}_rotated.pdf");

                // Use PdfPageEditor (a Facades class) to edit the PDF
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the source PDF file
                    editor.BindPdf(pdfPath);

                    // Example edit: rotate all pages by 90 degrees
                    editor.Rotation = 90;

                    // Apply the changes to the document
                    editor.ApplyChanges();

                    // Save the edited PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors for the specific file
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch processing completed.");
    }
}
