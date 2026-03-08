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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (from Aspose.Pdf.Facades) to edit PDF content.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Example operation: replace all occurrences of "Hello" with "Hi".
            editor.ReplaceText("Hello", "Hi");

            // Save the edited PDF to a new file.
            editor.Save(outputPath);

            // Close the editor (optional, Dispose will be called by using).
            editor.Close();
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}