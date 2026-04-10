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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the PDF document
        using (Form form = new Form(inputPdf))
        {
            // Create a file stream for the XML output
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                // Export all form field values to the XML stream
                form.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form fields exported to '{outputXml}'.");
    }
}