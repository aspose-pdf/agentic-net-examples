using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

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
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Import all annotations from the XFDF file (including FreeTextAnnotations with callout lines).
                // Correct API name is ImportAnnotationsFromXfdf (lower‑case "f").
                pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF. Document.Save(string) writes PDF regardless of extension.
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
