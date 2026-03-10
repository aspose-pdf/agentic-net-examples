using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PDF editing namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (from Aspose.Pdf.Facades) to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF file for editing
            editor.BindPdf(inputPath);

            // Example operation: replace all occurrences of "Hello" with "Hi"
            editor.ReplaceText("Hello", "Hi");

            // Save the edited PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}