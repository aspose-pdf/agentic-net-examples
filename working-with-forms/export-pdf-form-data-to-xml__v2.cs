using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the destination XML file.
        const string pdfPath = "input.pdf";
        const string xmlPath = "formData.xml";

        // Verify that the PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(pdfPath))
            {
                // Export the form fields (and other document structure) to an XML file.
                // This operation works offline; no network request is performed.
                doc.SaveXml(xmlPath);
            }

            Console.WriteLine($"Form data successfully exported to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}