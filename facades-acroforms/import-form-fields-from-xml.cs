using System;
using System.IO;
using Aspose.Pdf.Facades; // Form class resides here

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XML data file and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string xmlDataPath   = "data.xml";
        const string outputPdfPath = "output.pdf";

        // Ensure the source files exist before proceeding
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Use a Form facade to bind the PDF, import XML field values and save the result
        using (Form form = new Form())
        {
            // Bind the existing PDF document to the Form facade
            form.BindPdf(sourcePdfPath);

            // Open the XML file as a read‑only stream and import its field values
            using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
            {
                // ImportXml reads the field values from the XML stream and applies them to the bound PDF
                form.ImportXml(xmlStream);
            }

            // Save the modified PDF to the desired output location
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields imported from XML and saved to '{outputPdfPath}'.");
    }
}