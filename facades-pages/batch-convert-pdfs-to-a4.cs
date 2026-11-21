using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where A4‑converted PDFs will be saved
        const string outputFolder = "OutputA4";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string inputPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_A4.pdf");

            try
            {
                // PdfPageEditor does NOT implement IDisposable, so no using block
                PdfPageEditor editor = new PdfPageEditor();

                // Load the source PDF
                editor.BindPdf(inputPath);

                // Set the target page size to A4 using the correct enum
                editor.PageSize = Aspose.Pdf.PageSize.A4;

                // Save the resized PDF
                editor.Save(outputPath);

                Console.WriteLine($"Converted '{inputPath}' to A4 -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}