using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormA.pdf";
        const string xmlPath = "FormA_form.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable; wrap in using for deterministic disposal
        using (Form form = new Form())
        {
            // Bind the PDF that contains the form fields
            form.BindPdf(pdfPath);

            // Export the form data to an XML file – ExportXml expects a Stream
            using (FileStream xmlStream = File.Create(xmlPath))
            {
                form.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form data exported to '{xmlPath}'.");
    }
}
