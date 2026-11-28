using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormB.pdf";
        const string xmlPath = "FormB_form.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block
            using (Form form = new Form())
            {
                // Bind the PDF containing the form fields
                form.BindPdf(pdfPath);

                // Export the form data to an XML file – ExportXml expects a Stream, not a file path
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
