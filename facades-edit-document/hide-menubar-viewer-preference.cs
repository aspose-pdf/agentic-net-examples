using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hide_menubar.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Attempt to set the HideMenubar viewer preference.
        // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
        // Using dynamic bypasses compile‑time checks and allows the code to compile
        // against older versions. If the property is absent at runtime, we catch
        // the RuntimeBinderException and continue without failing.
        try
        {
            dynamic doc = pdfDocument;
            doc.ViewerPreferences.HideMenubar = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. HideMenubar preference was not applied.");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved with HideMenubar preference (if supported) to '{outputPath}'.");
    }
}
