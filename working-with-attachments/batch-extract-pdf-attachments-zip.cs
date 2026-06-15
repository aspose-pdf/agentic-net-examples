using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files – adjust the paths as needed
        string[] pdfFiles = new string[]
        {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Output ZIP archive that will contain all extracted attachments
        const string outputZipPath = "AllAttachments.zip";

        // Create (or overwrite) the ZIP archive
        using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create, FileAccess.Write))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string pdfPath in pdfFiles)
            {
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    continue;
                }

                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // The EmbeddedFiles collection holds the attached files (FileSpecification objects)
                    if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                    {
                        Console.WriteLine($"No attachments found in: {pdfPath}");
                        continue;
                    }

                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        // Ensure the file specification has a name and content stream
                        if (string.IsNullOrEmpty(fileSpec.Name) || fileSpec.Contents == null)
                            continue;

                        // Create a unique entry name to avoid collisions between PDFs
                        string safePdfName = Path.GetFileNameWithoutExtension(pdfPath);
                        string entryName = $"{safePdfName}_{fileSpec.Name}";

                        // Create a new entry in the ZIP archive
                        ZipArchiveEntry zipEntry = archive.CreateEntry(entryName, CompressionLevel.Optimal);

                        // Copy the attachment's content into the ZIP entry
                        using (Stream entryStream = zipEntry.Open())
                        using (Stream attachmentStream = fileSpec.Contents)
                        {
                            // Reset the attachment stream position in case it is not at the beginning
                            if (attachmentStream.CanSeek)
                                attachmentStream.Position = 0;

                            attachmentStream.CopyTo(entryStream);
                        }

                        Console.WriteLine($"Extracted '{fileSpec.Name}' from '{pdfPath}' into archive.");
                    }
                }
            }
        }

        Console.WriteLine($"All attachments have been consolidated into '{outputZipPath}'.");
    }
}