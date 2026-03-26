using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFolder = "input";
        const string outputFolder = "output";

        // Ensure both input and output directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        int totalFiles = pdfFiles.Length;

        if (totalFiles == 0)
        {
            Console.WriteLine($"No PDF files found in the '{inputFolder}' folder.");
            return;
        }

        const int progressBarWidth = 50; // characters

        for (int i = 0; i < totalFiles; i++)
        {
            string inputPath = pdfFiles[i];
            string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputPath) + "_clean.pdf");

            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                editor.DeleteAnnotations();
                editor.Save(outputPath);
            }

            // ----- Progress reporting -----
            int percent = (i + 1) * 100 / totalFiles;
            int filledLength = percent * progressBarWidth / 100;
            string bar = new string('█', filledLength) + new string('─', progressBarWidth - filledLength);
            Console.Write($"\rProcessing {i + 1}/{totalFiles} [{bar}] {percent}%");
        }

        // Move to the next line after the loop finishes
        Console.WriteLine();
        Console.WriteLine("Batch annotation deletion completed.");
    }
}
