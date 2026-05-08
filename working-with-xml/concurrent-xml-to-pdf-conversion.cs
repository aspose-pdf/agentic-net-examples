using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, XmlLoadOptions)

class Program
{
    static void Main()
    {
        // Input folder containing XML files to be converted.
        // Use Path.Combine with the current directory to build a platform‑independent absolute path.
        string inputFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "InputXml"));
        // Output folder where generated PDFs will be placed.
        string outputFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "OutputPdf"));

        // Ensure both input and output directories exist before starting work.
        // Creating the input folder prevents a DirectoryNotFoundException when the folder is missing.
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Get all XML files in the input folder (non‑recursive).
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml", SearchOption.TopDirectoryOnly);
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine($"No XML files found in '{inputFolder}'." );
            return;
        }

        // Process each XML file concurrently.
        // Each iteration creates its own Document instance – Document is NOT thread‑safe,
        // so sharing a single instance would cause race conditions.
        ParallelOptions opts = new ParallelOptions
        {
            // Use a sensible degree of parallelism (e.g., number of logical processors).
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        Parallel.ForEach(xmlFiles, opts, xmlPath =>
        {
            try
            {
                // Derive PDF file name from XML file name.
                string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                string pdfPath = Path.Combine(outputFolder, pdfFileName);

                // Load the XML into a new Document using XmlLoadOptions.
                using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
                {
                    // Save as PDF. Document.Save(string) writes a PDF regardless of extension.
                    pdfDoc.Save(pdfPath);
                }

                Console.WriteLine($"Converted: {Path.GetFileName(xmlPath)} → {pdfFileName}");
            }
            catch (Exception ex)
            {
                // Log the error but continue processing other files.
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All conversions completed.");
    }
}
