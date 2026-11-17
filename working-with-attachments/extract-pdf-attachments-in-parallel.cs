using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing PDF files to process
        const string inputDirectory = "InputPdfs";
        // Root directory where extracted attachments will be saved
        const string outputRoot = "ExtractedAttachments";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Gather all PDF file paths
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRoot);

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Create a subfolder for this PDF's embedded files
                    string pdfNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                    string pdfOutputDir = Path.Combine(outputRoot, pdfNameWithoutExt);
                    Directory.CreateDirectory(pdfOutputDir);

                    int fileIndex = 1;
                    // Iterate over all embedded files in the document
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        // Build a safe file name: index_originalName
                        string safeFileName = $"{fileIndex}_{fileSpec.Name}";
                        string outPath = Path.Combine(pdfOutputDir, safeFileName);

                        // Save the embedded file stream to disk
                        using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            // Ensure the content stream is positioned at the start
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;
                            fileSpec.Contents.CopyTo(outStream);
                        }

                        fileIndex++;
                    }
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                // Log any errors for this specific PDF without aborting other tasks
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Attachment extraction completed.");
    }
}
