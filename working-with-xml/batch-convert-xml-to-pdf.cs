using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source XML files
        const string inputFolder = "InputXmlFolder";
        // Folder where generated PDFs will be placed
        const string outputFolder = "OutputPdfFolder";

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
            // Build output PDF file name (same base name, .pdf extension)
            string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
            string pdfPath = Path.Combine(outputFolder, pdfFileName);

            try
            {
                // Use a using block for deterministic disposal (rule: document-disposal-with-using)
                using (Document pdfDoc = new Document())
                {
                    // Load the XML content into the document (rule: xml-load-use-bindxml-not-document-constructor)
                    pdfDoc.BindXml(xmlPath);

                    // Save the document as PDF (rule: use Document.Save(string))
                    pdfDoc.Save(pdfPath);
                }

                Console.WriteLine($"Converted: '{xmlPath}' → '{pdfPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }
    }
}