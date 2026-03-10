using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputCgm = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputCgm))
        {
            Console.Error.WriteLine($"File not found: {inputCgm}");
            return;
        }

        // Load the CGM file and convert it to a PDF document.
        using (Document doc = new Document(inputCgm, new CgmLoadOptions()))
        {
            // Use the PdfFileInfo facade to manipulate PDF metadata.
            using (PdfFileInfo info = new PdfFileInfo(doc))
            {
                // Assign desired metadata fields.
                info.Title        = "Converted CGM Document";
                info.Author       = "John Doe";
                info.Subject      = "CGM to PDF conversion";
                info.Keywords     = "CGM, PDF, Aspose.Pdf.Facades";
                info.Creator      = "Aspose.Pdf.Facades";

                // PdfFileInfo expects dates as strings in PDF date format (yyyyMMddHHmmss).
                string pdfDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                info.CreationDate = pdfDate;
                info.ModDate      = pdfDate;

                // Save the PDF with the updated metadata.
                info.SaveNewInfo(outputPdf);
            }
        }

        Console.WriteLine($"Metadata assigned and PDF saved to '{outputPdf}'.");
    }
}
