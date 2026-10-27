using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_zoomed.pdf";
        const int zoomPercent = 150; // Desired initial zoom (e.g., 150%)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core API.
        Document pdfDoc = new Document(inputPath);

        // Attempt to set the initial zoom level for the PDF viewer.
        // Use dynamic access to avoid compile‑time errors on older Aspose.Pdf versions
        // where ViewerPreferences may not be available.
        dynamic doc = pdfDoc;
        try
        {
            doc.ViewerPreferences.Zoom = zoomPercent;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences.Zoom is not supported by the current Aspose.Pdf library version.");
        }

        // Save the updated PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF saved with zoom level {zoomPercent}% to '{outputPath}'.");
    }
}
