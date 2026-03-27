using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Deletes the specified pages from each PDF file in the list.
    // Returns a list of file names for the cleaned PDFs.
    static List<string> DeletePagesFromFiles(List<string> inputFiles, int[] pagesToDelete)
    {
        List<string> outputFiles = new List<string>();
        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = $"{fileNameWithoutExt}_cleaned.pdf"; // simple filename, no directory

            // PdfFileEditor does NOT implement IDisposable, so do NOT use a using statement.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to delete pages from {inputPath}");
                continue;
            }

            outputFiles.Add(outputPath);
        }
        return outputFiles;
    }

    static void Main()
    {
        List<string> pdfFiles = new List<string> { "sample1.pdf", "sample2.pdf" };
        int[] pagesToRemove = new int[] { 2, 3 }; // pages are 1‑based
        List<string> cleanedFiles = DeletePagesFromFiles(pdfFiles, pagesToRemove);
        foreach (string cleanedPath in cleanedFiles)
        {
            Console.WriteLine($"Cleaned PDF saved to '{cleanedPath}'");
        }
    }
}
