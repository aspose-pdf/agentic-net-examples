using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "pdfs";
        // Folder where XML files will be written
        const string outputFolder = "xmls";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(outputFolder, baseName + ".xml");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Export the document (including form data) to an XML file
                    doc.SaveXml(xmlPath);
                }

                Console.WriteLine($"Exported: {pdfPath} -> {xmlPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}