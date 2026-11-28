using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "SampleForm.pdf";
        const string xmlPath = "SampleExport.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable; ensure deterministic disposal
        using (Form form = new Form())
        {
            // Bind the PDF that contains the form fields
            form.BindPdf(pdfPath);

            // Export all form field data to an XML file
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
                // xmlStream is automatically closed by the using block
            }
        }

        Console.WriteLine($"Form data exported to '{xmlPath}'.");
    }
}