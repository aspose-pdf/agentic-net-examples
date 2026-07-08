using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and XmlSaveOptions

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDirectory = "InputPdfs";
        // Directory where XML files will be written
        const string outputDirectory = "OutputXml";

        // Validate input directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Preserve original file name (without extension) for the XML output
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string xmlPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".xml");

                // Load PDF and save as XML using XmlSaveOptions
                using (Document pdfDocument = new Document(pdfPath))
                {
                    XmlSaveOptions xmlOptions = new XmlSaveOptions(); // default options
                    pdfDocument.Save(xmlPath, xmlOptions);
                }

                Console.WriteLine($"Converted: {pdfPath} -> {xmlPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}