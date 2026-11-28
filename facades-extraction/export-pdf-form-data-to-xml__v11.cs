using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormD.pdf";
        const string outputXml = "FormD_form.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Form implements IDisposable; using ensures proper resource cleanup
            using (Form pdfForm = new Form())
            {
                // Load the PDF that contains the form fields
                pdfForm.BindPdf(inputPdf);

                // Export the form data to an XML file – ExportXml expects a Stream, not a file path
                using (FileStream xmlStream = File.Create(outputXml))
                {
                    pdfForm.ExportXml(xmlStream);
                }
            }

            Console.WriteLine($"Form data exported to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
