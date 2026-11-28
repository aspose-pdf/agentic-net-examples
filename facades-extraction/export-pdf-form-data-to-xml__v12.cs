using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormE.pdf";
        const string xmlPath = "FormE_form.xml";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable – use a using block for deterministic cleanup
        using (Form form = new Form())
        {
            // Bind the PDF that contains the interactive form
            form.BindPdf(pdfPath);

            // Export the form fields and values to an XML file via a stream
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form data exported to '{xmlPath}'.");
    }
}
