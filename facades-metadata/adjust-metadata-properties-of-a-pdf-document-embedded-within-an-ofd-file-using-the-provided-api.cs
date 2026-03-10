using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Facades;        // Facades for metadata handling

class Program
{
    static void Main()
    {
        const string inputOfd  = "input.ofd";   // OFD file containing the PDF
        const string outputPdf = "output.pdf";  // Destination PDF with updated metadata

        // Verify input file exists
        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"File not found: {inputOfd}");
            return;
        }

        // Load the OFD file as a PDF document using the appropriate load options
        using (Document doc = new Document(inputOfd, new OfdLoadOptions()))
        {
            // Bind a PdfFileInfo facade to the loaded document for metadata manipulation
            PdfFileInfo pdfInfo = new PdfFileInfo(doc);

            // Adjust desired metadata properties
            pdfInfo.Title    = "Updated Document Title";
            pdfInfo.Author   = "Jane Smith";
            pdfInfo.Subject  = "Demonstration of OFD to PDF metadata update";
            pdfInfo.Keywords = "Aspose.Pdf, OFD, metadata, example";

            // Save the document with the new metadata to a PDF file
            pdfInfo.SaveNewInfo(outputPdf);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPdf}'.");
    }
}