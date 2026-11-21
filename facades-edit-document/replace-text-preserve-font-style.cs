using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Needed for TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldText    = "OldString";   // text to replace
        const string newText    = "NewString";   // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so no using block is required.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document.
        editor.BindPdf(inputPath);

        // Replace the specified text on page 2 only, preserving the original font style.
        // In newer Aspose.PDF versions the overload is:
        // ReplaceText(string oldText, int pageNumber, string newText, TextState textState)
        // Passing null for TextState keeps the original formatting.
        editor.ReplaceText(oldText, 2, newText, null);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Text replacement completed. Saved to '{outputPath}'.");
    }
}
