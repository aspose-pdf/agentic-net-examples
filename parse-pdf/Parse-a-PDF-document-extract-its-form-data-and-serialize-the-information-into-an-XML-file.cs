using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // Required for XmlSaveOptions (in Aspose.Pdf namespace)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF to be processed
        const string outputXmlPath = "form_data.xml";      // Resulting XML file

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure XML save options – required when saving to a non‑PDF format
                XmlSaveOptions xmlOptions = new XmlSaveOptions();

                // Save the document (including its form fields) as XML
                pdfDoc.Save(outputXmlPath, xmlOptions);
            }

            Console.WriteLine($"Form data extracted and saved to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}