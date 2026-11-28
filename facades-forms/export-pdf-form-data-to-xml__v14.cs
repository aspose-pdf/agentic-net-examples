using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF with form fields
        const string xmlPath = "formData.xml";   // destination XML file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable – wrap it in a using block.
            using (Form form = new Form())
            {
                // Bind the PDF document to the Form object.
                form.BindPdf(pdfPath);

                // ExportXml expects a Stream, not a file path.
                using (FileStream fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(fs);
                }
            }

            Console.WriteLine($"Form data exported successfully to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during form export: {ex.Message}");
        }
    }
}
