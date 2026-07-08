using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string xmlInputPath  = "fields.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Initialize the Form facade with source and destination PDFs
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Import field values from the XML stream; field order is preserved
            using (FileStream xmlStream = new FileStream(xmlInputPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }

            // Persist the changes to the output PDF
            form.Save();
        }

        Console.WriteLine($"Form fields imported successfully to '{outputPdfPath}'.");
    }
}