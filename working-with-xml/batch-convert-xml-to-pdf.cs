using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, XmlLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Folder containing XML files
        const string inputFolder = @"C:\InputXmlFolder";
        // Folder where PDFs will be saved
        const string outputFolder = @"C:\OutputPdfFolder";

        // Validate folders
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all XML files in the input folder (non‑recursive)
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml", SearchOption.TopDirectoryOnly);

        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Prepare load options for XML (no XSL required)
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Load XML and convert to PDF
                using (Document pdfDoc = new Document(xmlPath, loadOptions))
                {
                    // Determine output PDF file name (same base name, .pdf extension)
                    string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                    string pdfPath = Path.Combine(outputFolder, pdfFileName);

                    // Save as PDF (Document.Save without SaveOptions writes PDF)
                    pdfDoc.Save(pdfPath);
                }

                Console.WriteLine($"Converted: {Path.GetFileName(xmlPath)} → {Path.GetFileNameWithoutExtension(xmlPath)}.pdf");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}