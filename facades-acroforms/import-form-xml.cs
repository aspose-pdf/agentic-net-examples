using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string xmlData = "data.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlData))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlData}");
            return;
        }

        using (Form pdfForm = new Form(inputPdf, outputPdf))
        {
            using (FileStream xmlStream = new FileStream(xmlData, FileMode.Open, FileAccess.Read))
            {
                pdfForm.ImportXml(xmlStream);
            }
            pdfForm.Save();
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdf}'.");
    }
}
