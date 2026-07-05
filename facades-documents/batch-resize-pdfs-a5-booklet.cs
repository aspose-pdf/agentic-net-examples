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
        // Folder where resized PDFs and booklets will be saved
        const string outputFolder = "OutputBooklets";

        // Ensure both folders exist so the program does not throw DirectoryNotFoundException
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Conversion factor from millimetres to points (1 mm = 2.83464566929134 pt)
        const double mmToPt = 2.83464566929134;
        // A5 page size in points
        double a5Width = 148 * mmToPt;   // ≈ 419.53 pt
        double a5Height = 210 * mmToPt;  // ≈ 595.28 pt

        // Process each PDF file in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{Path.GetFullPath(inputFolder)}'." );
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(inputPath);
            string resizedPath = Path.Combine(outputFolder, $"{baseName}_A5.pdf");
            string bookletPath = Path.Combine(outputFolder, $"{baseName}_Booklet.pdf");

            // Resize all pages to A5 dimensions
            PdfFileEditor editor = new PdfFileEditor();
            // Passing null for pages processes every page in the document
            editor.ResizeContents(inputPath, resizedPath, null, a5Width, a5Height);

            // Create a booklet from the resized PDF; output pages are also A5
            editor.MakeBooklet(resizedPath, bookletPath, PageSize.A5);
        }

        Console.WriteLine("Batch resizing and booklet creation completed.");
    }
}
