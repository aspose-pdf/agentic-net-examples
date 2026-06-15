using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "PdfForm.pdf";
        const string outputXml = "export.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the Form facade with the PDF file
            using (Form form = new Form(inputPdf))
            // Create the output XML file stream
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                // Export all form field values (except button fields) to the XML stream
                form.ExportXml(xmlStream);
            }

            Console.WriteLine($"Form data successfully exported to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}