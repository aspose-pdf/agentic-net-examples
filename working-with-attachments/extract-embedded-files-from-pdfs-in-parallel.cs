using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files – replace with your actual file list or discover them dynamically.
        string[] pdfFiles = new[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Directory where extracted attachments will be saved.
        const string outputDirectory = "ExtractedAttachments";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Parallel processing: each PDF is handled on a separate thread.
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            // Skip missing files.
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                return;
            }

            try
            {
                // Load the PDF document inside a using block for deterministic disposal.
                using (Document doc = new Document(pdfPath))
                {
                    // If the document has no embedded files, nothing to do.
                    if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                    {
                        Console.WriteLine($"No embedded files in: {pdfPath}");
                        return;
                    }

                    // Iterate over each embedded file (the replacement for the old Attachment API).
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        // Build a unique file name to avoid collisions when multiple PDFs contain
                        // embedded files with the same name.
                        string safeFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_" + fileSpec.Name;
                        string destinationPath = Path.Combine(outputDirectory, safeFileName);

                        // Save the embedded file content to disk.
                        using (FileStream fs = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                        {
                            // Ensure the stream is positioned at the beginning.
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;
                            fileSpec.Contents.CopyTo(fs);
                        }

                        Console.WriteLine($"Saved embedded file: {destinationPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Embedded file extraction completed.");
    }
}
