using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_department.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF to the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdf);

        // Add a custom XMP field "Department" with value "Finance"
        // Using the generic Add(string, object) overload
        xmp.Add("xmp:Department", "Finance");

        // Save the PDF with the updated XMP metadata
        xmp.Save(outputPdf);

        Console.WriteLine($"PDF saved with Department metadata: {outputPdf}");
    }
}