using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // required for annotation types if needed

class Program
{
    static void Main()
    {
        const string pdfInputPath = "input.pdf";          // source PDF
        const string xfdfInputPath = "annotations.xfdf";   // XFDF containing FreeTextAnnotation callout
        const string pdfOutputPath = "output.pdf";         // PDF with imported annotations

        // Verify files exist
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(xfdfInputPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfInputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfInputPath))
            // Open the XFDF stream also inside a using block
            using (FileStream xfdfStream = File.OpenRead(xfdfInputPath))
            {
                // Import all annotations from the XFDF stream (including FreeTextAnnotation callout lines)
                pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);

                // Save the modified PDF; no special SaveOptions needed because we keep PDF format
                pdfDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Annotations imported and PDF saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
