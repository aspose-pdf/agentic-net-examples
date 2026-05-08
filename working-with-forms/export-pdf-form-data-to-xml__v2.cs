using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the target XML file.
        const string pdfPath = "input.pdf";
        const string xmlOutputPath = "formData.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(pdfPath))
            {
                // OPTIONAL: Fill a form field (replace with actual field name).
                // The core Form class provides an indexer to access fields by name.
                // Example: doc.Form["CustomerName"].Value = "John Doe";
                // Uncomment and modify as needed.
                // if (doc.Form["CustomerName"] != null)
                // {
                //     doc.Form["CustomerName"].Value = "John Doe";
                // }

                // Export the entire document model, which includes form field data,
                // to an XML file. This serves as an offline collection of the form data.
                doc.SaveXml(xmlOutputPath);
            }

            Console.WriteLine($"Form data exported to XML: {xmlOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}