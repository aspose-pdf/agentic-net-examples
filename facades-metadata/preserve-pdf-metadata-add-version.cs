using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF and preserve all current metadata.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Add or update the custom field "Version".
            pdfInfo.SetMetaInfo("Version", "1.0");

            // Save the PDF with the new custom metadata while keeping all other metadata intact.
            bool success = pdfInfo.SaveNewInfo(outputPdf);
            Console.WriteLine(success
                ? $"Metadata updated and saved to '{outputPdf}'."
                : "Failed to save the updated PDF.");
        }
    }
}