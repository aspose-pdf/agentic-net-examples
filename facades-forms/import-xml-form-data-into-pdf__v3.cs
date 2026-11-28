using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "ImportXmlTarget.pdf";
        const string xmlPath = "FormData.xml";
        const string outputPath = "ImportedXml.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            // Form implements IDisposable; use using for deterministic disposal
            using (Form form = new Form())
            {
                // Bind the existing PDF document
                form.BindPdf(pdfPath);

                // Import form data from the XML file (ImportXml expects a Stream)
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    form.ImportXml(xmlStream);
                }

                // Save the updated PDF
                form.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
