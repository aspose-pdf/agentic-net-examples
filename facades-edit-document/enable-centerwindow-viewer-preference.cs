using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Try to enable the CenterWindow viewer preference.
        // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
        // Using dynamic bypasses compile‑time checks and allows the code to compile against older versions as well.
        dynamic doc = pdfDocument;
        try
        {
            doc.ViewerPreferences.CenterWindow = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. The PDF will be saved without changing the CenterWindow setting.");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved: {outputPath}");
    }
}
