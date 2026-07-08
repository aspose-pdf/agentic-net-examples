using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where the XML (XFDF) files will be written
        const string outputFolder = "ExportedXml";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Build output file name with .xfdf extension (XML format)
            string xmlPath = Path.Combine(outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + ".xfdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Export form annotations (including field values) to XFDF (XML) file
                    doc.ExportAnnotationsToXfdf(xmlPath);
                }

                Console.WriteLine($"Exported form data to: {xmlPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}