using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlFile = "data.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlFile}");
            return;
        }

        using (Form form = new Form(inputPdf, outputPdf))
        {
            using (FileStream xmlStream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }
            form.Save();
        }

        Console.WriteLine($"Form fields imported from XML and saved to '{outputPdf}'.");
    }
}