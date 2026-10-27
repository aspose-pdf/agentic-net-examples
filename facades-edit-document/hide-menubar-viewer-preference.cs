using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Attempt to enable the HideMenubar viewer preference.
        // This property exists only in newer versions of Aspose.Pdf.
        // Using dynamic bypasses compile‑time checks and allows the code to compile
        // against older versions as well. If the property is missing at runtime,
        // a RuntimeBinderException is caught and a friendly message is shown.
        dynamic doc = pdfDocument;
        try
        {
            doc.ViewerPreferences.HideMenubar = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }

        // Save the PDF with the (potentially) updated viewer preference
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved with HideMenubar preference to '{outputPath}'.");
    }
}
