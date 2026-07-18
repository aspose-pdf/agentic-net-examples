using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf; // Core PDF API – load option classes (e.g., XmlLoadOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Directory containing source XML files
        const string inputDirectory = @"C:\InputXml";
        // Directory where generated PDFs will be placed
        const string outputDirectory = @"C:\OutputPdf";

        // Verify that the input directory exists before attempting to enumerate files.
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory '{inputDirectory}' does not exist. No files to process.");
            return; // Gracefully exit – prevents DirectoryNotFoundException.
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Gather all *.xml files
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml", SearchOption.TopDirectoryOnly);

        // If there are no XML files, inform the user and exit.
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine($"No XML files found in '{inputDirectory}'.");
            return;
        }

        // Process each XML file in parallel – each iteration works with its own Document instance,
        // therefore there is no shared mutable state and the operation is thread‑safe.
        Parallel.ForEach(xmlFiles, xmlPath =>
        {
            try
            {
                // Prepare load options for XML → PDF conversion (XmlLoadOptions resides in Aspose.Pdf)
                var loadOptions = new XmlLoadOptions();

                // Load XML into a new Document (using statement guarantees disposal)
                using (var pdfDoc = new Document(xmlPath, loadOptions))
                {
                    // Determine output PDF file name (same base name, .pdf extension)
                    string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                    string pdfPath = Path.Combine(outputDirectory, pdfFileName);

                    // Save the generated PDF
                    pdfDoc.Save(pdfPath);
                }

                Console.WriteLine($"Converted '{Path.GetFileName(xmlPath)}' to PDF successfully.");
            }
            catch (Exception ex)
            {
                // Log any conversion errors; they do not affect other parallel tasks
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(xmlPath)}': {ex.Message}");
            }
        });
    }
}
