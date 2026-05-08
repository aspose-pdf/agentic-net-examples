using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing XML files to be converted
        const string inputDirectory = "XmlFiles";
        // Directory where generated PDFs will be saved
        const string outputDirectory = "PdfOutput";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Process each XML file in the input directory
        foreach (string xmlPath in Directory.GetFiles(inputDirectory, "*.xml"))
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDirectory, fileName + ".pdf");

            Console.WriteLine($"--- Processing '{xmlPath}' ---");
            try
            {
                // Initialize load options for XML conversion
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Load XML and convert to PDF
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    // Save the generated PDF
                    pdfDocument.Save(pdfPath);
                }

                Console.WriteLine($"Successfully generated PDF: {pdfPath}");
            }
            catch (PdfException pdfEx)
            {
                // Specific Aspose.Pdf exception handling
                Console.Error.WriteLine($"PdfException while processing '{xmlPath}': {pdfEx.Message}");
            }
            catch (Exception ex)
            {
                // General exception handling
                Console.Error.WriteLine($"Error while processing '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}