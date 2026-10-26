using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // The console version expects file paths as arguments.
        // Example: dotnet run file1.pdf file2.pdf [file3.pdf ...]
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <pdf1> <pdf2> [<pdf3> ...]");
            return;
        }

        var inputPaths = new List<string>();
        foreach (var arg in args)
        {
            if (File.Exists(arg))
                inputPaths.Add(arg);
            else
                Console.WriteLine($"Warning: file not found '{arg}' – ignored.");
        }

        if (inputPaths.Count < 2)
        {
            Console.WriteLine("At least two existing PDF files are required.");
            return;
        }

        // Create a temporary file for the merged PDF.
        string outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed.
            var editor = new PdfFileEditor();
            editor.Concatenate(inputPaths.ToArray(), outputPath);
            Console.WriteLine($"Merged PDF created at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}
