using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "formdata.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the Form facade to export AcroForm data to XML
        using (Form form = new Form())
        {
            form.BindPdf(inputPdf); // Load the PDF document

            // Export all form fields to an XML file
            using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(fs);
            }
        }

        Console.WriteLine($"AcroForm data exported to '{outputXml}'.");
    }
}
