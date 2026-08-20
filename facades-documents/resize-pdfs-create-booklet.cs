using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs and output booklet file path
        string inputFolder = args.Length > 0 ? args[0] : "InputPdfs";
        string outputBooklet = args.Length > 1 ? args[1] : "booklet.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to process.");
            return;
        }

        // List to hold paths of temporary resized PDFs
        List<string> resizedFiles = new List<string>();

        // PdfFileEditor provides the required Facades operations
        Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

        // Resize each PDF to a content size of 1024x768 points
        foreach (string srcFile in pdfFiles)
        {
            string tempResized = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            // ResizeContents returns a bool indicating success; ignore for brevity
            editor.ResizeContents(srcFile, tempResized, null, 1024, 768);
            resizedFiles.Add(tempResized);
        }

        // Concatenate all resized PDFs into a single intermediate PDF
        string concatenatedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + "_concat.pdf");
        editor.Concatenate(resizedFiles.ToArray(), concatenatedPath);

        // Create a booklet from the concatenated PDF
        editor.MakeBooklet(concatenatedPath, outputBooklet);

        // Clean up temporary files
        foreach (string tempFile in resizedFiles)
        {
            try { File.Delete(tempFile); } catch { /* ignore cleanup errors */ }
        }
        try { File.Delete(concatenatedPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Booklet created successfully at: {outputBooklet}");
    }
}