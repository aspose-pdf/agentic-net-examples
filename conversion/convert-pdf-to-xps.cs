using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXps = "output.xps";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document within a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create XpsSaveOptions with default settings
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS, passing the SaveOptions explicitly
            pdfDoc.Save(outputXps, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputXps}");
    }
}