using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    /// <summary>
    /// Console entry point that removes all annotations from a PDF.
    /// Usage: PdfCleaner <inputPdfPath> <outputPdfPath>
    /// </summary>
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfCleaner <inputPdfPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using FileStream inputStream = File.OpenRead(inputPath);
        // MemoryStream will hold the cleaned PDF
        using MemoryStream outputStream = new MemoryStream();

        // Aspose.Pdf.Facades editor for annotation manipulation
        using PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputStream);
        // Remove every annotation present in the document
        editor.DeleteAnnotations();
        // Save the modified document into the output stream
        editor.Save(outputStream);

        // Persist the cleaned PDF to the requested output path
        File.WriteAllBytes(outputPath, outputStream.ToArray());

        Console.WriteLine($"Cleaned PDF saved to: {outputPath}");
    }
}
