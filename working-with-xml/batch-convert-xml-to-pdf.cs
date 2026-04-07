using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing XML files
        const string inputFolder = "XmlFolder";
        // Folder where PDFs will be saved
        const string outputFolder = "PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all XML files in the input folder (non‑recursive)
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml", SearchOption.TopDirectoryOnly);

        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load the XML file with default XmlLoadOptions
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Use the Document constructor that accepts the XML file path and load options
                using (Document pdfDoc = new Document(xmlPath, loadOptions))
                {
                    // Build the output PDF file name
                    string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                    string pdfPath = Path.Combine(outputFolder, pdfFileName);

                    // Save the document as PDF
                    pdfDoc.Save(pdfPath);

                    Console.WriteLine($"Converted: '{xmlPath}' → '{pdfPath}'");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{xmlPath}': {ex.Message}");
            }
        }
    }
}