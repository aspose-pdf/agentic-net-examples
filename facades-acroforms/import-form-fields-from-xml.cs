using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input_form.pdf";   // PDF with form fields
        const string xmlPath   = "form_data.xml";    // XML containing field values
        const string outputPdf = "filled_form.pdf";

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
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Open the XML file as a stream
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    // Use the Form facade to import XML data
                    using (Form form = new Form(pdfDoc))
                    {
                        form.ImportXml(xmlStream);   // <-- corrected method
                        form.Save(outputPdf);         // Save via the facade (disposes Document internally)
                    }
                }
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
