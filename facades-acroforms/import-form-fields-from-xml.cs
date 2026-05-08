using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the XML data file and the resulting PDF
        const string inputPdfPath  = "input.pdf";
        const string xmlDataPath   = "data.xml";
        const string outputPdfPath = "output.pdf";

        // Ensure the source files exist before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Use the Form facade to bind to the PDF, import XML field values, and save the result
        using (Form form = new Form(inputPdfPath)) // Binds the facade to the source PDF
        {
            // Open the XML file as a read‑only stream and import its data into the form fields
            using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream); // Imports field values from the XML stream
            }

            // Save the updated PDF to a new file
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields imported from XML and saved to '{outputPdfPath}'.");
    }
}