using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    // Extracts all embedded file attachments from the given PDF files in parallel.
    // Each PDF gets its own sub‑folder under the outputRoot directory.
    static void Main()
    {
        // Example input PDF files – replace with your actual paths.
        string[] pdfFiles = {
            @"C:\Docs\Sample1.pdf",
            @"C:\Docs\Sample2.pdf",
            @"C:\Docs\Sample3.pdf"
        };

        // Directory where extracted attachments will be saved.
        string outputRoot = @"C:\ExtractedAttachments";

        // Ensure the root output directory exists.
        Directory.CreateDirectory(outputRoot);

        // Run extraction concurrently.
        ExtractAttachmentsParallel(pdfFiles, outputRoot);
    }

    static void ExtractAttachmentsParallel(string[] pdfPaths, string outputRoot)
    {
        // Parallel.ForEach will schedule a separate task for each PDF file.
        Parallel.ForEach(pdfPaths, pdfPath =>
        {
            try
            {
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    return;
                }

                // Create a sub‑folder named after the PDF (without extension) to avoid name clashes.
                string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                string pdfOutputDir = Path.Combine(outputRoot, pdfName);
                Directory.CreateDirectory(pdfOutputDir);

                // Load the PDF document inside a using block for deterministic disposal.
                using (Document doc = new Document(pdfPath))
                {
                    // The EmbeddedFiles collection holds all attached files.
                    // It uses 1‑based indexing (consistent with Aspose.Pdf page collections).
                    for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
                    {
                        // Each entry is a FileSpecification representing an attachment.
                        FileSpecification attachment = doc.EmbeddedFiles[i];

                        // Determine a safe file name for the extracted attachment.
                        string attachmentName = !string.IsNullOrEmpty(attachment.Name)
                            ? attachment.Name
                            : $"attachment_{i}";

                        string outputPath = Path.Combine(pdfOutputDir, attachmentName);

                        // Write the attachment's content stream to disk.
                        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            // The Contents property is a Stream containing the file data.
                            attachment.Contents.CopyTo(outputStream);
                        }

                        // Optional: output some metadata for verification.
                        Console.WriteLine($"Extracted '{attachmentName}' from '{pdfPath}' to '{outputPath}'.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files.
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}