using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";                     // source PDF
        const string outputFolder = "Chapters";                     // folder for results
        const string resizedPdfPath = "resized.pdf";                // intermediate resized PDF

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Step 1: Resize contents of all pages to achieve uniform formatting.
        // PdfFileEditor.ResizeContents(string inputFile, string outputFile,
        //     int[] pages, double newWidth, double newHeight)
        // Passing null for the pages array applies the operation to every page.
        // The width and height values are in default PDF units (points).
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();

        // Example target size – modify to match desired chapter layout
        double targetWidth  = 500; // points
        double targetHeight = 700; // points

        editor.ResizeContents(inputPdfPath, resizedPdfPath, null, targetWidth, targetHeight);

        // -----------------------------------------------------------------
        // Step 2: Split the resized PDF into separate chapter files.
        // PdfFileEditor.SplitToPages(string inputFile, string fileNameTemplate)
        // The template must contain %NUM% which will be replaced by the page number.
        // -----------------------------------------------------------------
        string chapterTemplate = Path.Combine(outputFolder, "Chapter%NUM%.pdf");
        editor.SplitToPages(resizedPdfPath, chapterTemplate);

        Console.WriteLine("PDF has been resized and split into individual chapter files.");
    }
}