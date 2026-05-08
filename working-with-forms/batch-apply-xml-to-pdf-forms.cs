using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve absolute paths based on the executable location
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string pdfFolder = Path.Combine(baseDir, "InputPdfs");
        string xmlFolder = Path.Combine(baseDir, "InputXmls");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the source PDF folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Source PDF folder not found: '{pdfFolder}'. Please create the folder and add PDF files before running the program.");
            return;
        }

        // Iterate over all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(pdfFolder, "*.pdf"))
        {
            // Determine the corresponding XML file based on the file name (without extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(xmlFolder, baseName + ".xml");

            // Skip if the matching XML file does not exist
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"XML file not found for PDF '{baseName}'. Skipping.");
                continue;
            }

            try
            {
                // Load the PDF document
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Bind the XML data to the PDF form fields
                    pdfDocument.BindXml(xmlPath);

                    // Save the updated PDF to the output folder
                    string outputPath = Path.Combine(outputFolder, baseName + "_filled.pdf");
                    pdfDocument.Save(outputPath);

                    Console.WriteLine($"Processed '{baseName}': XML applied and saved to '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
