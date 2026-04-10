using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "formdata.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Export all AcroForm (interactive form) data to XFDF.
            // XFDF is an XML-based format, so the resulting file is XML.
            pdfDoc.ExportAnnotationsToXfdf(outputXml);
        }

        Console.WriteLine($"AcroForm data exported to '{outputXml}'.");
    }
}