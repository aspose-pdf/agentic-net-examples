using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf; // Load option classes (e.g., XmlLoadOptions) are in this namespace

class Program
{
    // Entry point – demonstrates thread‑safe processing of multiple XML files.
    static void Main()
    {
        // Example input: all XML files in a folder.
        string inputFolder = @"C:\InputXml";
        string outputFolder = @"C:\OutputPdf";

        // Validate input folder existence before enumerating files (rule: always check Directory.Exists).
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: '{inputFolder}'. No files to process.");
            return; // Gracefully exit – prevents DirectoryNotFoundException.
        }

        // Ensure output folder exists.
        Directory.CreateDirectory(outputFolder);

        // Gather XML file paths.
        var xmlFiles = Directory.GetFiles(inputFolder, "*.xml", SearchOption.TopDirectoryOnly);

        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to convert.");
            return;
        }

        // Process files in parallel – each iteration works with its own Document instance.
        Parallel.ForEach(xmlFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, xmlPath =>
        {
            try
            {
                // Load XML using the correct load options (rule: XmlLoadOptions).
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Declare pdfPath outside the using block so it is visible after disposal.
                string pdfPath = null;

                using (Document pdfDoc = new Document(xmlPath, loadOptions))
                {
                    // Determine output PDF path (same file name, .pdf extension).
                    string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                    pdfPath = Path.Combine(outputFolder, pdfFileName);

                    // Save as PDF (rule: Document.Save(string) writes PDF).
                    pdfDoc.Save(pdfPath);

                    // Optional: release memory held by the document before disposal.
                    pdfDoc.FreeMemory();
                }

                // Log successful conversion – pdfPath is now in scope.
                Console.WriteLine($"Converted: {Path.GetFileName(xmlPath)} → {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                // Log conversion errors per file – does not affect other threads.
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All XML files have been processed.");
    }
}
