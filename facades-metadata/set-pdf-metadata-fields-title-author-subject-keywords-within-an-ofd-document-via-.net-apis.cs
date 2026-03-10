using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputOfdPath  = "input.ofd";          // OFD source file
        const string outputPdfPath = "output.pdf";         // Resulting PDF file

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"File not found: {inputOfdPath}");
            return;
        }

        // Load the OFD document (OFD is input‑only, we convert it to PDF)
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // Bind a PdfFileInfo facade to the loaded document
            using (PdfFileInfo info = new PdfFileInfo())
            {
                info.BindPdf(doc);

                // Set the required metadata fields
                info.Title    = "Sample OFD Converted Document";
                info.Author   = "John Doe";
                info.Subject  = "Demonstration of metadata setting via Aspose.Pdf.Facades";
                info.Keywords = "OFD, PDF, Aspose.Pdf, Metadata";

                // Save the updated document as PDF (metadata is written)
                info.SaveNewInfo(outputPdfPath);
            }
        }

        Console.WriteLine($"OFD converted and metadata saved to '{outputPdfPath}'.");
    }
}