using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use the directory of the executing assembly as the data directory.
        // This avoids the placeholder "YOUR_DATA_DIRECTORY" which caused the FileNotFoundException.
        string dataDir = AppDomain.CurrentDomain.BaseDirectory;

        // Full paths for the input XML and the output PDF.
        string xmlFile = Path.Combine(dataDir, "XML-to-PDF.xml");
        string pdfFile = Path.Combine(dataDir, "XML-to-PDF.pdf");

        // Ensure the source XML file exists. If it does not, create a minimal example XML.
        if (!File.Exists(xmlFile))
        {
            const string sampleXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<document>
    <section>
        <title>Sample XML to PDF conversion</title>
        <paragraph>This PDF was generated from an XML file using Aspose.Pdf.</paragraph>
    </section>
</document>";
            File.WriteAllText(xmlFile, sampleXml);
            Console.WriteLine($"Sample XML file created at: {xmlFile}");
        }

        // Initialize load options for XML (no XSL transformation required).
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();

        try
        {
            // Load the XML file and convert it to a PDF document.
            // The Document constructor with (string, LoadOptions) performs the conversion.
            using (Document pdfDocument = new Document(xmlFile, xmlLoadOptions))
            {
                // Save the resulting PDF to the specified path.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"XML file has been successfully converted to PDF: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
