using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlData  = "updatedData.xml";
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

        // Use the Form facade to import XML data into the PDF's AcroForm
        using (Form form = new Form())
        {
            // Bind the source PDF document
            form.BindPdf(inputPdf);

            // Import the XML data (XFDF format) into the form fields
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