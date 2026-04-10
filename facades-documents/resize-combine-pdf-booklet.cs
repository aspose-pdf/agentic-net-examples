using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class BatchPdfProcessor
{
    static void Main()
    {
        // Input folder containing the original PDFs
        const string inputFolder = "InputPdfs";
        // Temporary folder to store resized PDFs
        const string tempFolder = "ResizedTemp";
        // Path for the concatenated PDF (before booklet conversion)
        const string concatenatedPdf = "Combined.pdf";
        // Final booklet PDF output
        const string bookletPdf = "Booklet.pdf";

        // Ensure the temporary folder exists
        if (!Directory.Exists(tempFolder))
            Directory.CreateDirectory(tempFolder);

        // Verify the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Please create it and place PDF files inside.");
            return;
        }

        // Collect all PDF files from the input folder
        string[] sourceFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (sourceFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to process.");
            return;
        }

        var resizedFiles = new List<string>();

        // Resize each PDF to 1024x768 using PdfFileEditor.ResizeContents
        for (int i = 0; i < sourceFiles.Length; i++)
        {
            string sourcePath = sourceFiles[i];
            string resizedPath = Path.Combine(tempFolder, $"resized_{i}.pdf");

            // PdfFileEditor does not implement IDisposable, so we just instantiate it
            PdfFileEditor editor = new PdfFileEditor();

            // Resize contents of all pages (pages parameter = null) to 1024x768 points
            // The width and height are specified in default space units (points)
            editor.ResizeContents(sourcePath, resizedPath, null, 1024, 768);

            resizedFiles.Add(resizedPath);
        }

        // Concatenate all resized PDFs into a single PDF
        PdfFileEditor concatEditor = new PdfFileEditor();
        concatEditor.Concatenate(resizedFiles.ToArray(), concatenatedPdf);

        // Convert the concatenated PDF into a booklet
        PdfFileEditor bookletEditor = new PdfFileEditor();
        bookletEditor.MakeBooklet(concatenatedPdf, bookletPdf);

        Console.WriteLine("Batch processing completed.");
        Console.WriteLine($"Resized PDFs stored in: {tempFolder}");
        Console.WriteLine($"Combined PDF: {concatenatedPdf}");
        Console.WriteLine($"Booklet PDF: {bookletPdf}");
    }
}
