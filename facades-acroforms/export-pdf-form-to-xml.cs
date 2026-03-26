using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "form_data.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Form pdfForm = new Form(inputPdf))
        {
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                pdfForm.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form data exported to '{outputXml}'.");
    }
}