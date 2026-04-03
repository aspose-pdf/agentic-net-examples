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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Export all AcroForm (annotation) data to an XFDF file.
            // XFDF is an XML-based format, satisfying the requirement for XML output.
            doc.ExportAnnotationsToXfdf(outputXml);
        }

        Console.WriteLine($"AcroForm data exported to '{outputXml}'.");
    }
}