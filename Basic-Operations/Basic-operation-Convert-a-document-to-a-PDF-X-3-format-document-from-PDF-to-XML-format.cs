using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file path.
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Output XML file path.
        string xmlPath = Path.Combine(dataDir, "output.xml");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Configure XML save options.
                XmlSaveOptions saveOptions = new XmlSaveOptions();

                // Save the document as XML.
                pdfDocument.Save(xmlPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to XML: {xmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}