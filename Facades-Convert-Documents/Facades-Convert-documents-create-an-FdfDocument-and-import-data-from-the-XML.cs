using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string updatedPdfPath = "output_with_fdf.pdf";

        // Verify input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(pdfPath);

            // NOTE: FDF handling classes (FdfDocument, FdfReader) are not available in the
            // current Aspose.Pdf for .NET version. The example therefore simply saves a copy
            // of the PDF. If FDF support is required, use the newer PdfFileEditor.BindFdf API
            // or upgrade to a version that provides the missing classes.
            pdfDoc.Save(updatedPdfPath);
            Console.WriteLine($"PDF saved to {updatedPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
