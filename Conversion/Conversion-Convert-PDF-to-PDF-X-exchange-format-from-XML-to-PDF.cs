using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Directory that contains the source XML file.
            string dataDir = "Data";

            // Input XML file (PDFXML or other supported XML format).
            string xmlFile = Path.Combine(dataDir, "input.xml");

            // Desired output PDF file.
            string pdfFile = Path.Combine(dataDir, "output.pdf");

            // Verify that the source XML exists.
            if (!File.Exists(xmlFile))
                throw new FileNotFoundException($"Input XML file not found: {xmlFile}");

            // Load options for XML files.
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            // Load the XML document and convert it to PDF.
            using (Document pdfDocument = new Document(xmlFile, loadOptions))
            {
                // No Conformance property is required/available in the current Aspose.Pdf version.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"Conversion completed successfully. Output saved to: {pdfFile}");
        }
        catch (Exception ex)
        {
            // Generic error handling – prints the message to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}