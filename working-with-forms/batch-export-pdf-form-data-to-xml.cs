using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where the XML files will be written
        const string outputFolder = "OutputXml";

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
            // Build the corresponding XML file name
            string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
            string xmlPath = Path.Combine(outputFolder, xmlFileName);

            try
            {
                // Load the PDF document (lifecycle: create/load)
                using (Document doc = new Document(pdfPath))
                {
                    // Export the document (including form data) to XML (lifecycle: save)
                    doc.SaveXml(xmlPath);
                }

                Console.WriteLine($"Exported '{pdfPath}' → '{xmlPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}