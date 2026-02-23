using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the XML file.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input XML file (PDFXML or generic XML representation).
        string xmlPath = Path.Combine(dataDir, "input.xml");

        // Desired output PDF file.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the XML source exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Load the XML into a PDF document using default XmlLoadOptions.
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                // Save the bound PDF instance to a PDF file.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"PDF successfully saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}