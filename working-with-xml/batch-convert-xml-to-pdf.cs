using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source XML files
        const string inputFolder = "XmlInput";
        // Folder where the generated PDFs will be placed
        const string outputFolder = "PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all XML files in the input folder (non‑recursive)
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml", SearchOption.TopDirectoryOnly);

        foreach (string xmlPath in xmlFiles)
        {
            // Derive the PDF file name from the XML file name
            string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
            string pdfPath = Path.Combine(outputFolder, pdfFileName);

            try
            {
                // Load the XML into a PDF Document using XmlLoadOptions
                using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
                {
                    // Save the document as PDF; Document.Save without options always writes PDF
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Converted: {xmlPath} → {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}