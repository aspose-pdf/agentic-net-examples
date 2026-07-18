using System;
using System.IO;
using Aspose.Pdf; // Document, XmlLoadOptions are in this namespace

class Program
{
    static void Main()
    {
        // Folder containing XML files
        const string inputFolder = @"C:\XmlInput";
        // Folder where PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Verify that the input directory exists before proceeding
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Please create the folder and place XML files inside before running the program.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Enumerate all .xml files in the input folder
        foreach (string xmlPath in Directory.EnumerateFiles(inputFolder, "*.xml"))
        {
            // Derive PDF file name from XML file name
            string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
            string pdfPath = Path.Combine(outputFolder, pdfFileName);

            // Load XML with default XmlLoadOptions
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            // Use a using block for deterministic disposal of the Document
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                // Save as PDF (Document.Save without SaveOptions always writes PDF)
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"Converted '{xmlPath}' to '{pdfPath}'.");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
