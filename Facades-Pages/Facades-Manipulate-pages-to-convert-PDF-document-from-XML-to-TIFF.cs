using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlInputPath  = "input.xml";   // XML representation of the PDF
        const string tiffOutputPath = "output.tiff";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Create a PDF document and bind the XML data to it
        using (Document pdfDoc = new Document())
        {
            pdfDoc.BindXml(xmlInputPath);

            // Use PdfConverter facade to convert the PDF pages to a single TIFF image
            PdfConverter converter = new PdfConverter(pdfDoc);
            converter.DoConvert();                       // Prepare conversion
            converter.SaveAsTIFF(tiffOutputPath);        // Save all pages as one TIFF file
            converter.Close();                           // Release resources used by the converter
        }

        Console.WriteLine($"TIFF file created at '{tiffOutputPath}'.");
    }
}