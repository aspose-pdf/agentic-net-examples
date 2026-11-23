using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Annotation types live here

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // existing PDF to receive the annotation
        const string xfdfPath = "annotations.xfdf"; // XFDF file containing a FreeTextAnnotation with callout line
        const string outputPdf = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Import XFDF annotations using the core XFDF API.
                // The core API provides ImportAnnotationsFromXfdf(string path) (or stream) – no extra options needed.
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF. No SaveOptions needed because the default format is PDF.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
