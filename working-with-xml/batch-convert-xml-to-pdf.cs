using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source XML files
        const string inputFolder = "XmlInput";
        // Folder where the generated PDFs will be saved
        const string outputFolder = "PdfOutput";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Prepare load options for XML → PDF conversion
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Process each .xml file in the input folder
        foreach (string xmlPath in Directory.GetFiles(inputFolder, "*.xml"))
        {
            try
            {
                // Determine output PDF file path (same base name, .pdf extension)
                string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                string pdfPath = Path.Combine(outputFolder, pdfFileName);

                // Load XML and convert to PDF using the core Document API
                using (Document pdfDoc = new Document(xmlPath, loadOptions))
                {
                    // Save the resulting PDF
                    pdfDoc.Save(pdfPath);
                }

                Console.WriteLine($"Converted: {xmlPath} → {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}