using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        var pdfFiles = new List<string>
        {
            "file1.pdf",
            "file2.pdf",
            "file3.pdf"
        };

        // Root folder where extracted attachments will be saved
        const string outputRoot = "ExtractedAttachments";

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Thread‑safe collection for any errors that occur during processing
        var errors = new ConcurrentBag<string>();

        // Process each PDF file in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                if (!File.Exists(pdfPath))
                {
                    errors.Add($"File not found: {pdfPath}");
                    return;
                }

                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Use the EmbeddedFiles collection (Attachments property does not exist)
                    if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                        return;

                    // Create a sub‑directory for this PDF's embedded files
                    string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                    string pdfOutputDir = Path.Combine(outputRoot, pdfName);
                    Directory.CreateDirectory(pdfOutputDir);

                    // Iterate over each embedded file and save it to disk
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        // Use the original file name; fall back to a GUID if missing
                        string attachmentName = string.IsNullOrWhiteSpace(fileSpec.Name)
                            ? Guid.NewGuid().ToString()
                            : fileSpec.Name;

                        string attachmentPath = Path.Combine(pdfOutputDir, attachmentName);

                        // Write the file's content stream to disk (FileSpecification.Save does not exist)
                        using (FileStream outStream = new FileStream(attachmentPath, FileMode.Create, FileAccess.Write))
                        {
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;
                            fileSpec.Contents.CopyTo(outStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        // Output any errors that were captured
        foreach (var err in errors)
        {
            Console.Error.WriteLine(err);
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}
