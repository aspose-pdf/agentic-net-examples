using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormG.pdf";
        const string outputXml = "FormG_form.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block
            using (Form form = new Form())
            {
                // Bind the PDF that contains the form fields
                form.BindPdf(inputPdf);

                // Export the form fields to an XML file – ExportXml expects a Stream
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
