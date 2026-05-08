using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input PDF exists; create a minimal document if it does not.
        if (!File.Exists(inputPdf))
        {
            var emptyDoc = new Document();
            emptyDoc.Pages.Add(); // add a blank page
            emptyDoc.Save(inputPdf);
        }

        // Get current application version (e.g., "1.2.3.0")
        string appVersion = Assembly.GetExecutingAssembly()
                                    ?.GetName()
                                    ?.Version?
                                    .ToString() ?? "0.0.0.0";

        // Load the PDF document (lifecycle: load, modify, save)
        Document pdfDoc = new Document(inputPdf);

        // Initialize XMP metadata facade and bind it to the document
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(pdfDoc);

        // Add or update the CreatorTool property in XMP metadata
        xmp.Add("xmp:CreatorTool", $"MyApp v{appVersion}");

        // Save the updated PDF with the modified XMP metadata
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"CreatorTool set successfully. Output saved to '{outputPdf}'.");
    }
}