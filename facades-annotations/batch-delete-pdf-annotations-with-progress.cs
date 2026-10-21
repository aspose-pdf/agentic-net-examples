using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\PdfBatch\Input";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        int total = pdfFiles.Length;

        if (total == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF and delete all annotations
        for (int i = 0; i < total; i++)
        {
            string sourcePath = pdfFiles[i];
            string fileName   = Path.GetFileName(sourcePath);
            string destPath   = Path.Combine(outputFolder, fileName);

            // PdfAnnotationEditor implements IDisposable, so use a using block
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF
                editor.BindPdf(sourcePath);

                // Delete every annotation in the document
                editor.DeleteAnnotations();

                // Optional: flatten any remaining annotation objects (makes them part of the page content)
                editor.FlatteningAnnotations();

                // Save the modified PDF
                editor.Save(destPath);
            }

            // Calculate and display progress percentage
            int percent = (i + 1) * 100 / total;
            Console.Write($"\rProcessed {i + 1}/{total} PDFs ({percent}%)");
        }

        Console.WriteLine(); // Move to next line after progress bar
        Console.WriteLine("Batch annotation deletion completed.");
    }
}
