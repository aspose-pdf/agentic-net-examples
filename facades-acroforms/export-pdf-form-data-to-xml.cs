using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "form_data.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(inputPdf))
            {
                // Create the output XML file stream
                using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                {
                    // Export all form field values to the XML stream
                    form.ExportXml(xmlStream);
                }
            }

            Console.WriteLine($"Form fields exported successfully to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}