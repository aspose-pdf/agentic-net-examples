using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchResizeAndConcat
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Directory for intermediate resized PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "ResizedPdfs");
        Directory.CreateDirectory(tempDir);

        List<string> resizedFiles = new List<string>();

        // Resize each PDF to 1024x768 using PdfPageEditor (Facades API)
        foreach (string srcPath in inputFiles)
        {
            if (!File.Exists(srcPath))
            {
                Console.Error.WriteLine($"Input file not found: {srcPath}");
                continue;
            }

            string resizedPath = Path.Combine(
                tempDir,
                Path.GetFileNameWithoutExtension(srcPath) + "_resized.pdf");

            // PdfPageEditor does NOT implement IDisposable; instantiate directly
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(srcPath);
            // Set custom page size (width, height) in points (1 point = 1/72 inch)
            editor.PageSize = new PageSize(1024, 768);
            editor.ApplyChanges();
            editor.Save(resizedPath);
            // No disposal required

            resizedFiles.Add(resizedPath);
        }

        // Concatenate all resized PDFs into a single document
        string outputPath = "merged.pdf";
        // PdfFileEditor does NOT implement IDisposable; instantiate directly
        PdfFileEditor fileEditor = new PdfFileEditor();
        fileEditor.Concatenate(resizedFiles.ToArray(), outputPath);

        Console.WriteLine($"Resized and concatenated PDF saved to '{outputPath}'.");
    }
}