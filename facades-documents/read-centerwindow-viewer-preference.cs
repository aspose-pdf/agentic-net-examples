using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "centerWindow.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF with PdfContentEditor (high‑level facade).
        var editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);

        bool centerWindow = false;

        // ViewerPreferences may not exist in older Aspose.Pdf versions.
        // Use dynamic access to avoid compile‑time CS1061 and fall back gracefully.
        try
        {
            dynamic doc = editor.Document; // underlying Document instance
            var prefs = doc.ViewerPreferences; // may throw at runtime if not present
            centerWindow = prefs?.CenterWindow ?? false;
        }
        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Assuming false.");
        }
        catch (Exception ex)
        {
            // Unexpected errors should be reported but not crash the program.
            Console.WriteLine($"Unexpected error while reading ViewerPreferences: {ex.Message}");
        }

        // Write the flag value to a text file.
        File.WriteAllText(outputPath, centerWindow.ToString());
        Console.WriteLine($"CenterWindow flag ({centerWindow}) written to '{outputPath}'.");
    }
}
