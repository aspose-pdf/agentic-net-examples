using System;
using System.IO;
using Aspose.Pdf;

public class XmlToPdfConverter
{
    public static void Main()
    {
        // Directory containing XML files
        string dataDir = "Data";

        if (!Directory.Exists(dataDir))
        {
            Console.WriteLine($"Directory '{dataDir}' does not exist.");
            return;
        }

        // Get XML files (limit to 4 due to evaluation mode)
        string[] allXmlFiles = Directory.GetFiles(dataDir, "*.xml");
        int filesToProcess = Math.Min(4, allXmlFiles.Length);

        if (filesToProcess == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        for (int i = 0; i < filesToProcess; i++)
        {
            string xmlPath = allXmlFiles[i];
            Console.WriteLine($"Processing file {i + 1}/{filesToProcess}: {xmlPath}");

            try
            {
                // Initialize load options
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Load XML into PDF document
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    Console.WriteLine("  XML loaded successfully.");

                    // Determine output PDF file name (simple filename)
                    string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";

                    // Save PDF
                    pdfDocument.Save(pdfFileName);
                    Console.WriteLine($"  PDF saved as '{pdfFileName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Error: {ex.Message}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Processing completed.");
    }
}