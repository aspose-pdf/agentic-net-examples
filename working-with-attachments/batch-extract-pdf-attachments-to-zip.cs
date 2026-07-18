using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class BatchAttachmentExtractor
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = @"C:\InputPdfs";
        // Path for the consolidated ZIP archive that will hold all extracted attachments
        const string outputZipPath = @"C:\Output\attachments.zip";

        // Gather all PDF files from the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found in the specified directory.");
            return;
        }

        // Create the ZIP archive (will be overwritten if it already exists)
        using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
        using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
        {
            // Process each PDF file
            foreach (string pdfPath in pdfFiles)
            {
                // Load the PDF document inside a using block (ensures proper disposal)
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // The EmbeddedFiles collection holds file attachments inside the PDF
                    if (pdfDocument.EmbeddedFiles != null && pdfDocument.EmbeddedFiles.Count > 0)
                    {
                        // Iterate over each attached file using reflection to avoid direct dependency on the EmbeddedFile type
                        foreach (var attachment in pdfDocument.EmbeddedFiles)
                        {
                            // Retrieve the attachment name via reflection
                            var nameProp = attachment.GetType().GetProperty("Name");
                            string attachmentName = nameProp?.GetValue(attachment) as string ?? "unknown";

                            // Build a ZIP entry name that includes the source PDF name as a folder
                            string entryName = $"{Path.GetFileNameWithoutExtension(pdfPath)}/{attachmentName}";
                            ZipArchiveEntry zipEntry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);

                            // Write the attachment's content directly into the ZIP entry using the Save(Stream) method via reflection
                            using (Stream entryStream = zipEntry.Open())
                            {
                                var saveMethod = attachment.GetType().GetMethod("Save", new[] { typeof(Stream) });
                                if (saveMethod != null)
                                {
                                    saveMethod.Invoke(attachment, new object[] { entryStream });
                                }
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"All attachments have been extracted to: {outputZipPath}");
    }
}
