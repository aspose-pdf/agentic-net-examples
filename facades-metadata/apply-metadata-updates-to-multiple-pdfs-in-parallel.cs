using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        string inputDirectory = "InputPdfs";
        // Directory where updated PDFs will be saved
        string outputDirectory = "OutputPdfs";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Verify that the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist. No files to process.");
            return;
        }

        // Gather all PDF files to process
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'.");
            return;
        }

        // Metadata values to apply to each document
        string newAuthor  = "John Doe";
        string newTitle   = "Updated Title";
        string newSubject = "Updated Subject";

        // Process files in parallel using TPL
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Determine output file path (same name, different folder)
                string fileName   = Path.GetFileName(pdfPath);
                string outputPath = Path.Combine(outputDirectory, fileName);

                // Use the PdfFileInfo facade to read and modify metadata
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    info.Author  = newAuthor;
                    info.Title   = newTitle;
                    info.Subject = newSubject;

                    // Save the updated metadata to a new PDF file
                    info.SaveNewInfo(outputPath);
                }
            }
            catch (Exception ex)
            {
                // Log the error but allow other files to continue processing
                Console.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Metadata update completed for all PDFs.");
    }
}
