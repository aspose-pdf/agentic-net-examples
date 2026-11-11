using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Required for PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF to ensure it has at least five pages
        Document pdfDoc = new Document(inputPath);
        if (pdfDoc.Pages.Count < 5)
        {
            Console.Error.WriteLine("The PDF does not contain a page 5.");
            return;
        }

        // Set the display duration for page 5 using PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = new int[] { 5 }; // target page 5
            editor.DisplayDuration = 5; // seconds
            editor.Save(outputPath);
        }

        Console.WriteLine($"DisplayDuration of page 5 set to 5 seconds in '{outputPath}'.");
    }
}
