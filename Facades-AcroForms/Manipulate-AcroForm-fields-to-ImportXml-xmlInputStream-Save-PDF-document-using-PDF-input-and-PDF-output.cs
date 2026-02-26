using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xmlDataPath   = "data.xml";

        // Verify that required files exist
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

        // Open the XML stream that contains the form field values
        using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the Form facade with the source PDF document
            using (Form form = new Form(inputPdfPath))
            {
                // Import field values from the XML stream into the PDF form
                form.ImportXml(xmlStream);

                // Save the updated PDF to the specified output file
                form.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"AcroForm fields imported from XML and saved to '{outputPdfPath}'.");
    }
}