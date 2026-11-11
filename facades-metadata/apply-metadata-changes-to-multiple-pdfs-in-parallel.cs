using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where updated PDFs will be written
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Collect all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each file in parallel using TPL
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Each iteration gets its own PdfFileInfo instance
                PdfFileInfo pdfInfo = new PdfFileInfo();

                // Load the PDF without fully parsing the document
                pdfInfo.BindPdf(pdfPath);

                // Apply desired metadata changes
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                pdfInfo.Title   = $"Processed - {baseName}";
                pdfInfo.Author  = "MyCompany";
                pdfInfo.Subject = "Batch metadata update";
                pdfInfo.Keywords = "Aspose.Pdf;Metadata;Batch";

                // Determine where to save the modified PDF
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                // Persist the changes
                pdfInfo.Save(outputPath);
            }
            catch (Exception ex)
            {
                // Log any errors for the specific file
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Metadata update completed.");
    }
}