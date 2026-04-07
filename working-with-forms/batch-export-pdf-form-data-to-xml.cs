using System;
using System.IO;
using Aspose.Pdf; // Document, XmlSaveOptions

class Program
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputFolder = "InputPdfs";
        // Directory where the XML files will be written
        const string outputFolder = "ExportedXml";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            return;
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Build the output XML file path (same name, .xml extension)
            string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
            string xmlPath = Path.Combine(outputFolder, xmlFileName);

            // Load the PDF document (using the standard Document constructor)
            using (Document doc = new Document(pdfPath))
            {
                // Export the document (including form data) to XML.
                // Must use XmlSaveOptions; otherwise Save(string) would produce a PDF.
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                doc.Save(xmlPath, xmlOptions);
            }

            Console.WriteLine($"Exported XML: '{pdfPath}' → '{xmlPath}'");
        }
    }
}
