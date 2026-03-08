using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Paths to source PDF files to be concatenated
        string[] sourceFiles = new string[]
        {
            "first.pdf",
            "second.pdf",
            "third.pdf"
        };

        // Path for the resulting merged PDF
        const string outputFile = "merged.pdf";

        // Verify that all source files exist before attempting concatenation
        foreach (string file in sourceFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Create the PdfFileEditor facade (does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate the source PDFs into the output file.
        // This method preserves original content, formatting, outlines, and logical structure.
        bool success = editor.Concatenate(sourceFiles, outputFile);

        if (success)
        {
            Console.WriteLine($"PDF files successfully concatenated into '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
        }
    }
}