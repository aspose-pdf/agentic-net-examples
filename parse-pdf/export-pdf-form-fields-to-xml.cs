using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "form_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use the Facade Form class to export PDF form fields to XML
        using (Form form = new Form())
        {
            form.BindPdf(inputPdf);
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form fields have been exported to '{outputXml}'.");
    }
}
