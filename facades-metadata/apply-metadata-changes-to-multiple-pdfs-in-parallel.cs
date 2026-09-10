using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDirectory = @"C:\InputPdfs";
        // Output directory where updated PDFs will be saved
        const string outputDirectory = @"C:\OutputPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files from the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Define the metadata values to apply
        const string newTitle   = "Updated Title";
        const string newAuthor  = "Updated Author";
        const string newSubject = "Updated Subject";

        // Process each PDF file in parallel using TPL
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            // Determine the output file path (same file name, different folder)
            string outputPath = Path.Combine(outputDirectory, Path.GetFileName(pdfPath));

            // Load the PDF with PdfFileInfo facade, modify metadata, and save
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
            {
                // Apply metadata changes
                pdfInfo.Title   = newTitle;
                pdfInfo.Author  = newAuthor;
                pdfInfo.Subject = newSubject;

                // Save the updated PDF (Save writes a PDF regardless of extension)
                pdfInfo.Save(outputPath);
            }
        });

        Console.WriteLine("Metadata update completed for all PDFs.");
    }
}