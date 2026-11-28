using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormF.pdf";
        const string outputXml = "FormF_form.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block
            using (Form form = new Form())
            {
                // Bind the PDF containing the form fields
                form.BindPdf(inputPdf);

                // Export the form data to an XML file – ExportXml expects a Stream, not a file path
                using (FileStream xmlStream = File.Create(outputXml))
                {
                    form.ExportXml(xmlStream);
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
