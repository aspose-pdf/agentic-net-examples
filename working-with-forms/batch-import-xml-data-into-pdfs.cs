using System;
using System.IO;
using Aspose.Pdf;

class BatchXmlFormImporter
{
    static void Main()
    {
        // Directories containing source PDFs, XML data files and the output PDFs.
        const string pdfDirectory = @"C:\Input\PDFs";
        const string xmlDirectory = @"C:\Input\XMLs";
        const string outputDirectory = @"C:\Output\PDFs";

        // Verify that the input directories exist before trying to enumerate files.
        if (!Directory.Exists(pdfDirectory))
        {
            Console.Error.WriteLine($"PDF source directory does not exist: '{pdfDirectory}'." );
            return;
        }
        if (!Directory.Exists(xmlDirectory))
        {
            Console.Error.WriteLine($"XML source directory does not exist: '{xmlDirectory}'." );
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file found in the PDF directory.
        foreach (string pdfPath in Directory.GetFiles(pdfDirectory, "*.pdf"))
        {
            // Derive the expected XML file name (same base name, .xml extension).
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(xmlDirectory, baseName + ".xml");

            // Skip if the matching XML file does not exist.
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"No XML data for '{baseName}'. Skipping.");
                continue;
            }

            // Load the PDF, bind the XML data, and save the result.
            try
            {
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Bind the XML data to the PDF document (XFA form data).
                    pdfDocument.BindXml(xmlPath);

                    // Construct the output file path.
                    string outputPath = Path.Combine(outputDirectory, baseName + "_filled.pdf");

                    // Save the updated PDF.
                    pdfDocument.Save(outputPath);
                }

                Console.WriteLine($"Processed '{baseName}': XML applied and saved to output.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }
    }
}
