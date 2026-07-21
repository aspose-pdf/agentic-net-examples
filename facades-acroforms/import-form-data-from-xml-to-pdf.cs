using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // PDF with form fields
        const string inputXmlPath  = "data.xml";       // XML containing field values
        const string outputPdfPath = "filled.pdf";     // Resulting PDF

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {inputXmlPath}");
            return;
        }

        // Open the XML file as a read‑only stream
        using (FileStream xmlStream = new FileStream(inputXmlPath, FileMode.Open, FileAccess.Read))
        {
            // Initialise the Form facade with input and output PDF paths
            using (Form form = new Form(inputPdfPath, outputPdfPath))
            {
                // Import the form field values from the XML stream
                form.ImportXml(xmlStream);

                // Persist the changes to the output PDF
                form.Save();
            }
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdfPath}'.");
    }
}