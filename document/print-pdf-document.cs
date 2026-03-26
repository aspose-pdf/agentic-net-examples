using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfViewer instance (it implements IDisposable)
            using (PdfViewer viewer = new PdfViewer())
            {
                // Bind the PDF document to the viewer
                viewer.BindPdf(doc);

                // Optional: set the form presentation mode before printing
                // NOTE: The FormPresentationMode property is not available in recent
                // versions of Aspose.Pdf, so it is omitted. If needed, use the
                // appropriate API for the version you target.

                // Send the document to the default printer
                viewer.PrintDocument();
            }
        }

        Console.WriteLine("Print job sent.");
    }
}
