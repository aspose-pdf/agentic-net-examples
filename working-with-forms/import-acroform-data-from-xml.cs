using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlData = "updatedData.xml";
        const string outputPdf = "output.pdf";

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

        // Bind the PDF to the Form facade and import XML data into its AcroForm fields
        using (Form form = new Form())
        {
            form.BindPdf(inputPdf);
            using (FileStream xmlStream = new FileStream(xmlData, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }
            // Save the updated PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"AcroForm data imported and saved to '{outputPdf}'.");
    }
}