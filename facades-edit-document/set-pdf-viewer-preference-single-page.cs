using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_single_page.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (full object model) – this provides access to viewer preferences.
        Document pdfDocument = new Document(inputPath);

        // Attempt to set the page layout to SinglePage if the ViewerPreferences API is available.
        try
        {
            dynamic doc = pdfDocument;               // Bypass compile‑time member check
            dynamic prefs = doc.ViewerPreferences;   // May throw RuntimeBinderException on older versions
            prefs.PageLayout = PageLayout.SinglePage;
            Console.WriteLine("ViewerPreferences.PageLayout set to SinglePage.");
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }

        // Save the modified PDF with the (potential) new viewer preference.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Viewer preference handling completed and saved to '{outputPath}'.");
    }
}