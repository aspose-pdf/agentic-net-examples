using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string xmlPath       = "fields.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Bind the PDF, import XML field values, and save.
        using (Form form = new Form())
        {
            form.BindPdf(inputPdfPath);                     // Load PDF
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);                  // Import fields from XML (order preserved)
            }
            form.Save(outputPdfPath);                       // Save the updated PDF
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdfPath}'.");
    }
}