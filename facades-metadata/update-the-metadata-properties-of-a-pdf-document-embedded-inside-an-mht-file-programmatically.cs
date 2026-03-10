using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";   // Path to the source MHT file
        const string outputPdf = "updated.pdf"; // Path for the resulting PDF

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file as a PDF document using MhtLoadOptions
        var loadOptions = new MhtLoadOptions();
        using (var pdfDoc = new Document(mhtPath, loadOptions))
        {
            // Attach the PdfFileInfo facade to the loaded document
            using (var pdfInfo = new PdfFileInfo(pdfDoc))
            {
                // Update desired metadata properties
                pdfInfo.Title        = "Updated Title";
                pdfInfo.Author       = "John Doe";
                pdfInfo.Subject      = "Sample Subject";
                pdfInfo.Keywords     = "Aspose, PDF, MHT";
                pdfInfo.Creator      = "My Application";

                // PdfFileInfo expects dates as strings in PDF date format (yyyyMMddHHmmss)
                string now = DateTime.Now.ToString("yyyyMMddHHmmss");
                pdfInfo.ModDate      = now;
                pdfInfo.CreationDate = now;

                // Persist the changes to a new PDF file
                pdfInfo.SaveNewInfo(outputPdf);
            }
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPdf}'.");
    }
}
