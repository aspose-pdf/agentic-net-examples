using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, XmlLoadOptions, PdfException)

class Program
{
    static void Main()
    {
        // Directory containing source XML files
        const string inputDir  = @"C:\XmlFiles";
        // Directory where generated PDFs will be placed
        const string outputDir = @"C:\GeneratedPdfs";

        // Verify that the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory '{inputDir}' does not exist. No files to process.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all XML files in the input directory
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml", SearchOption.TopDirectoryOnly);
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        foreach (string xmlPath in xmlFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath  = Path.Combine(outputDir, fileName + ".pdf");
            Console.WriteLine($"--- Processing '{xmlPath}' ---");

            try
            {
                // Step 1: Initialize load options for XML → PDF conversion
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Step 2: Load the XML file into a PDF Document
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    Console.WriteLine("  Loaded XML successfully.");

                    // Optional: Validate the document before saving (logs result)
                    // bool isValid = pdfDocument.Validate("validation.log", PdfFormat.PDF_A_1B);
                    // Console.WriteLine($"  Validation result: {isValid}");

                    // Step 3: Save the generated PDF
                    pdfDocument.Save(pdfPath);
                    Console.WriteLine($"  Saved PDF to '{pdfPath}'.");
                }
            }
            catch (PdfException ex)
            {
                // Handles errors specific to Aspose.Pdf processing
                Console.Error.WriteLine($"  PDF processing error for '{xmlPath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handles any other unexpected errors
                Console.Error.WriteLine($"  Unexpected error for '{xmlPath}': {ex.Message}");
            }

            Console.WriteLine($"--- Finished '{xmlPath}' ---\n");
        }

        Console.WriteLine("All files processed.");
    }
}
