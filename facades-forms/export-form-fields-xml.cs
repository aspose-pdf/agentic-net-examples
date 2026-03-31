using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "PdfForm.pdf";
        const string outputXml = "form_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        using (Form pdfForm = new Form(inputPdf))
        {
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                pdfForm.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form fields exported to '{outputXml}'.");
    }
}