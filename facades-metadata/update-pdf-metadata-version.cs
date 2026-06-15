using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_version.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF file to the PdfFileInfo facade.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Add or update the custom metadata field "Version".
            pdfInfo.SetMetaInfo("Version", "1.0");

            // Save the updated PDF. Existing custom metadata is preserved.
            bool success = pdfInfo.SaveNewInfo(outputPdf);
            Console.WriteLine(success
                ? $"Metadata updated and saved to '{outputPdf}'."
                : "Failed to save the updated PDF.");
        }
    }
}