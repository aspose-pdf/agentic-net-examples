using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the PDF.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfFile = Path.Combine(dataDir, "PDF-to-XML.pdf");

        // Output XML file.
        string xmlFile = Path.Combine(dataDir, "PDF-to-XML.xml");

        // Verify that the input PDF exists.
        if (!File.Exists(pdfFile))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfFile}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(pdfFile))
            {
                // Initialize save options for XML format.
                XmlSaveOptions saveOptions = new XmlSaveOptions();

                // Save the document as XML.
                pdfDocument.Save(xmlFile, saveOptions);
            }

            Console.WriteLine($"PDF successfully saved as XML to: {xmlFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}