using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmpLogPath = "original_xmp.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the PDF to the XMP metadata facade
            using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
            {
                xmpFacade.BindPdf(pdfDoc);

                // Retrieve the original XMP metadata as a byte array
                byte[] xmpBytes = xmpFacade.GetXmpMetadata();

                // Write the XMP XML to a log file for audit purposes
                File.WriteAllBytes(xmpLogPath, xmpBytes);
                Console.WriteLine($"Original XMP metadata saved to '{xmpLogPath}'.");
            }

            // Place for any further PDF modifications...

            // Example: save the (potentially modified) PDF
            // pdfDoc.Save("modified.pdf");
        }
    }
}