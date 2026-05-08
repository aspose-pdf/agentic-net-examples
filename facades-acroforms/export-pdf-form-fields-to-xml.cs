using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "PdfForm.pdf";
        const string outputXmlPath = "export.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the PDF file
            using (Form form = new Form(inputPdfPath))
            {
                // Create a file stream for the XML output
                using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                {
                    // Export all form field values (except button fields) to the XML stream
                    form.ExportXml(xmlStream);
                }
            }

            Console.WriteLine($"Form fields exported successfully to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}