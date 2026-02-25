using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath  = "input.xml";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML (which may represent a PDF/A document) using XmlLoadOptions
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            // Document implements IDisposable, so wrap it in a using block
            using (Document pdfDoc = new Document(xmlInputPath, loadOptions))
            {
                // Save as a regular PDF. No SaveOptions needed for PDF output.
                pdfDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Conversion completed: '{pdfOutputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}