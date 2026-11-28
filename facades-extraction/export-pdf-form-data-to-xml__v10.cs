using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormC.pdf";
        const string xmlPath = "FormC_form.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, so use a using block.
            using (Form form = new Form())
            {
                // Bind the PDF that contains the form fields.
                form.BindPdf(pdfPath);

                // ExportXml expects a Stream, not a file path.
                using (FileStream xmlStream = File.Create(xmlPath))
                {
                    form.ExportXml(xmlStream);
                }
            }

            Console.WriteLine($"Form data exported to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
