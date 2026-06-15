using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "input_pdfs";
        // Folder where the XML files will be written
        const string outputFolder = "output_xml";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Build the output XML file path (same name, .xml extension)
            string xmlPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + ".xml");

            try
            {
                // Load the PDF document (lifecycle: load)
                using (Document doc = new Document(pdfPath))
                {
                    // Export the document (including form data) to XML
                    // Non‑PDF format requires explicit SaveOptions (rule: save-to-non-pdf-always-use-save-options)
                    XmlSaveOptions xmlOpts = new XmlSaveOptions();
                    doc.Save(xmlPath, xmlOpts); // lifecycle: save
                }

                Console.WriteLine($"Exported: {pdfPath} → {xmlPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}