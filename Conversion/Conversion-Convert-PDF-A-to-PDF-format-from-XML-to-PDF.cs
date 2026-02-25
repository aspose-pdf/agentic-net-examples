using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML (or PDF/A XML) into a Document
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Remove PDF/A compliance to obtain a regular PDF
                doc.RemovePdfaCompliance();

                // Save as a standard PDF (no SaveOptions needed)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}