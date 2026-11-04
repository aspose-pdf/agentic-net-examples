using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "form_template.pdf";   // PDF containing XFA form
        const string xmlData = "form_data.xml";       // XML with XFA data
        const string outputPdf = "filled_form.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlData))
        {
            Console.Error.WriteLine($"XML data not found: {xmlData}");
            return;
        }

        try
        {
            // Load the PDF that has an XFA form structure
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Import XFA data from the XML file into the PDF.
                // In recent Aspose.Pdf versions the XFA data is bound via BindXml.
                pdfDoc.BindXml(xmlData);

                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"XFA data imported successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
