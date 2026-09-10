using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder  = "InputPdfs";
        // Output folder where cleaned PDFs will be saved
        const string outputFolder = "OutputPdfs";

        // Validate folders
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        int totalFiles = pdfFiles.Length;

        if (totalFiles == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF and display progress
        for (int i = 0; i < totalFiles; i++)
        {
            string sourcePath = pdfFiles[i];
            string destPath   = Path.Combine(outputFolder, Path.GetFileName(sourcePath));

            // Use PdfAnnotationEditor to delete all annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(sourcePath);          // Load the PDF
                editor.DeleteAnnotations();          // Remove all annotations
                editor.Save(destPath);               // Save the cleaned PDF
            }

            // Calculate and display progress percentage
            int percent = (i + 1) * 100 / totalFiles;
            Console.WriteLine($"Processed {i + 1}/{totalFiles} ({percent}%)");
        }

        Console.WriteLine("Batch annotation deletion completed.");
    }
}