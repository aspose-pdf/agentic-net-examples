using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and XmlSaveOptions

class Program
{
    static void Main(string[] args)
    {
        // Resolve paths relative to the executable location to avoid missing‑directory issues
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string inputDirectory = Path.Combine(basePath, "InputPdfs");
        string outputDirectory = Path.Combine(basePath, "OutputXml");

        // Verify that the input folder exists; if not, inform the user and stop execution
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory not found: '{inputDirectory}'. Please create it and place PDF files inside.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string pdfFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Preserve the original file name (without extension) for the XML output
            string baseFileName = Path.GetFileNameWithoutExtension(pdfFilePath);
            string xmlFilePath = Path.Combine(outputDirectory, baseFileName + ".xml");

            // Load the PDF document
            using (Document pdfDocument = new Document(pdfFilePath))
            {
                // Initialize XML save options (required for non‑PDF output)
                XmlSaveOptions xmlOptions = new XmlSaveOptions();

                // Save the document as XML, preserving the original name
                pdfDocument.Save(xmlFilePath, xmlOptions);
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
