using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder containing PDFs and output booklet file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <inputFolder> <outputBookletPdf>");
            return;
        }

        string inputFolder = args[0];
        string outputBooklet = args[1];

        // Gather all PDF files from the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found in the specified folder.");
            return;
        }

        // List to store paths of temporary resized PDFs
        List<string> resizedFiles = new List<string>();
        PdfFileEditor editor = new PdfFileEditor();

        // Resize each PDF to 1024x768 points (default space units)
        foreach (string file in pdfFiles)
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            // Resize all pages (null pages array indicates all pages)
            editor.ResizeContents(file, tempFile, null, 1024, 768);
            resizedFiles.Add(tempFile);
        }

        // Concatenate all resized PDFs into a single PDF
        string concatenatedFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + "_concat.pdf");
        editor.Concatenate(resizedFiles.ToArray(), concatenatedFile);

        // Convert the concatenated PDF into a booklet
        editor.MakeBooklet(concatenatedFile, outputBooklet);

        // Clean up temporary files
        foreach (string temp in resizedFiles)
        {
            try { File.Delete(temp); } catch { }
        }
        try { File.Delete(concatenatedFile); } catch { }

        Console.WriteLine($"Booklet created successfully at '{outputBooklet}'.");
    }
}