using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the XML data, and the resulting PDF
        const string sourcePdfPath = "source.pdf";
        const string xmlDataPath   = "data.xml";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist
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

        // Initialize the Form facade with the source PDF and the target PDF file name
        Form form = new Form(sourcePdfPath, outputPdfPath);

        // Open the XML file as a stream and import its field values into the PDF
        using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
        {
            form.ImportXml(xmlStream);
        }

        // Persist the changes to the output PDF
        form.Save();

        Console.WriteLine($"XML data successfully imported into '{outputPdfPath}'.");
    }
}