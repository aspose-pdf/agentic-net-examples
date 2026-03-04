using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Use PdfContentEditor (a SaveableFacade) to load and edit the PDF.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF file for editing.
            editor.BindPdf(inputPdfPath);

            // Example operation: replace all occurrences of "Hello" with "Hi".
            // This demonstrates that the PDF is successfully loaded and mutable.
            editor.ReplaceText("Hello", "Hi");

            // Save the modified document to a new file.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF editing completed. Output saved to '{outputPdfPath}'.");
    }
}