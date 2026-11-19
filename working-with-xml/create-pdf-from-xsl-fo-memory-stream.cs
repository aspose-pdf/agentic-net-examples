using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // Required for text‑related classes (if needed)

class Program
{
    static void Main()
    {
        // Path to the source XML file (replace with your actual file)
        const string xmlFilePath = "input.xml";

        // Path for the generated PDF file
        const string pdfOutputPath = "output.pdf";

        // Ensure the XML source exists
        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFilePath}");
            return;
        }

        // Read the XML content into a memory stream
        byte[] xmlBytes = File.ReadAllBytes(xmlFilePath);
        using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
        {
            // Load the XML into an Aspose.Pdf Document.
            // XmlLoadOptions tells the parser that the input is XML.
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            // Wrap the Document in a using block for deterministic disposal.
            using (Document pdfDocument = new Document(xmlStream, loadOptions))
            {
                // Save the document as PDF using default settings.
                pdfDocument.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"PDF created successfully at '{pdfOutputPath}'.");
    }
}