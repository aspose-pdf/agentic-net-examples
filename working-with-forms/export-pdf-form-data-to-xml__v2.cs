using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputXml = @"C:\Temp\formdata.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the directory for the XML file exists
        string xmlDir = Path.GetDirectoryName(outputXml);
        if (!Directory.Exists(xmlDir))
            Directory.CreateDirectory(xmlDir);

        // Export the PDF form data to an XML file (offline collection)
        using (Form facadeForm = new Form())
        {
            facadeForm.BindPdf(inputPdf);
            using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                facadeForm.ExportXml(fs);
            }
        }

        // Optionally, save a copy of the PDF (e.g., after filling fields)
        using (Document doc = new Document(inputPdf))
        {
            doc.Save("processed.pdf");
        }

        Console.WriteLine($"Form data exported to {outputXml}");
    }
}
