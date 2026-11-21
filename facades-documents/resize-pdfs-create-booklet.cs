using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Temporary folder for resized PDFs
    private static readonly string TempFolder = Path.Combine(Path.GetTempPath(), "AsposePdfResizeTemp");

    static void Main(string[] args)
    {
        // Expect at least two arguments: output booklet PDF and one or more input PDFs
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <output_booklet.pdf> <input1.pdf> [<input2.pdf> ...]");
            return;
        }

        string outputBookletPath = args[0];
        string[] inputPdfPaths = args[1..]; // all remaining arguments are source PDFs

        // Ensure all input files exist
        foreach (string path in inputPdfPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Prepare temporary folder
        Directory.CreateDirectory(TempFolder);
        var resizedPdfPaths = new List<string>();

        try
        {
            // Step 1: Resize each PDF to 1024x768
            foreach (string srcPath in inputPdfPaths)
            {
                string resizedPath = Path.Combine(TempFolder, Path.GetFileNameWithoutExtension(srcPath) + "_resized.pdf");
                ResizePdf(srcPath, resizedPath, 1024, 768);
                resizedPdfPaths.Add(resizedPath);
            }

            // Step 2: Concatenate resized PDFs into a single booklet PDF
            ConcatenatePdfs(resizedPdfPaths.ToArray(), outputBookletPath);

            Console.WriteLine($"Booklet PDF created at: {outputBookletPath}");
        }
        finally
        {
            // Clean up temporary files
            foreach (string tempPath in resizedPdfPaths)
            {
                try { File.Delete(tempPath); } catch { /* ignore */ }
            }
            try { Directory.Delete(TempFolder, true); } catch { /* ignore */ }
        }
    }

    // Resizes all pages of a PDF to the specified width and height (in points)
    private static void ResizePdf(string sourcePath, string destinationPath, double width, double height)
    {
        // Load the PDF document
        using (Document pdfDoc = new Document(sourcePath))
        {
            // Adjust each page's dimensions via PageInfo
            foreach (Page page in pdfDoc.Pages)
            {
                page.PageInfo.Width = width;
                page.PageInfo.Height = height;
            }

            // Save the resized PDF
            pdfDoc.Save(destinationPath);
        }
    }

    // Concatenates multiple PDFs into one using PdfFileEditor (no using block)
    private static void ConcatenatePdfs(string[] sourcePaths, string outputPath)
    {
        PdfFileEditor fileEditor = new PdfFileEditor();
        fileEditor.Concatenate(sourcePaths, outputPath);
    }
}
