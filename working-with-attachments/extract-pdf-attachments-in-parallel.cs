using System;
using System.IO;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        string inputDir = "pdfs";
        // Directory where extracted attachments will be stored
        string outputDir = "attachments";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Retrieve all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        // Thread‑safe collection for error reporting
        ConcurrentBag<string> errors = new ConcurrentBag<string>();

        // Process each PDF file concurrently
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Skip PDFs without embedded files (attachments)
                    if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                        return;

                    // Create a subfolder named after the PDF (without extension) to hold its attachments
                    string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                    string pdfAttachmentDir = Path.Combine(outputDir, pdfName);
                    Directory.CreateDirectory(pdfAttachmentDir);

                    // Iterate over each embedded file (attachment)
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        if (fileSpec == null || fileSpec.Contents == null)
                            continue;

                        // Ensure a safe file name for the attachment
                        string safeName = Path.GetFileName(fileSpec.Name);
                        string outPath = Path.Combine(pdfAttachmentDir, safeName);

                        // Write the embedded file's contents to disk
                        using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            // Reset stream position if possible
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;
                            fileSpec.Contents.CopyTo(outStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Capture any exception for later reporting
                errors.Add($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        // Output any errors that occurred during processing
        foreach (var err in errors)
        {
            Console.Error.WriteLine(err);
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}
