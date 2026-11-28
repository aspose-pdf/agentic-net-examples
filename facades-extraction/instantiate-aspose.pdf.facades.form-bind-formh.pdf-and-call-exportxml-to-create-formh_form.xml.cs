using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormH.pdf";
        const string outputXml = "FormH_form.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Form implements IDisposable; using ensures proper resource cleanup
        using (Form form = new Form())
        {
            // Load the PDF that contains the form fields
            form.BindPdf(inputPdf);

            // Export the form fields to an XML file – ExportXml expects a Stream
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form data exported to '{outputXml}'.");
    }
}
