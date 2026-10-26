using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Optional: set Aspose.Pdf license once (thread‑safe if done before parallel work)
        // License license = new License();
        // license.SetLicense("Aspose.Pdf.lic");

        // Directory containing the source XML files
        const string inputDirectory = "InputXml";
        // Directory where generated PDFs will be placed
        const string outputDirectory = "OutputPdf";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Gather all XML files to process
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml", SearchOption.TopDirectoryOnly);

        // Process files in parallel – each iteration works with its own Document instance
        Parallel.ForEach(xmlFiles, xmlPath =>
        {
            try
            {
                // Load the XML representation of a PDF document
                using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
                {
                    // Determine output PDF path (same file name, .pdf extension, placed in output folder)
                    string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                    string pdfPath = Path.Combine(outputDirectory, pdfFileName);

                    // Save as PDF; Document.Save without SaveOptions writes PDF by default
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Successfully generated PDF: {Path.GetFileName(xmlPath)}");
            }
            catch (Exception ex)
            {
                // Log per‑file errors without aborting the whole batch
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All files processed.");
    }
}