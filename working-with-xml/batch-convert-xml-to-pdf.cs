using System;
using System.IO;
using Aspose.Pdf; // Document, XmlLoadOptions

class Program
{
    static void Main()
    {
        // Base directory of the running application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputXml");
        string outputFolder = Path.Combine(baseDir, "OutputPdf");

        // Ensure the folders exist (creates them if they are missing)
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Retrieve all XML files in the input folder (non‑recursive)
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml", SearchOption.TopDirectoryOnly);
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine($"No XML files found in '{inputFolder}'. Place XML files there and rerun the program.");
            return;
        }

        foreach (string xmlPath in xmlFiles)
        {
            // Build the output PDF file name based on the XML file name
            string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
            string pdfPath = Path.Combine(outputFolder, pdfFileName);

            try
            {
                // Load the XML and convert it to a PDF document (no XSL transformation)
                XmlLoadOptions loadOptions = new XmlLoadOptions();
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    pdfDocument.Save(pdfPath); // Save as PDF (default format)
                }

                Console.WriteLine($"Converted '{xmlPath}' → '{pdfPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert '{xmlPath}': {ex.Message}");
            }
        }
    }
}
