using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // Required for text handling if needed

class Program
{
    static void Main()
    {
        // Paths for input PDF and output XML file
        const string inputPdfPath  = "input.pdf";
        const string outputXmlPath = "formData.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document (core API, no Facades)
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // The core Document API provides a SaveXml method that writes the
                // complete PDF model (including form fields) to an XML file.
                // This can be used for offline collection of form data.
                pdfDoc.SaveXml(outputXmlPath);

                Console.WriteLine($"Form data exported to XML: '{outputXmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}