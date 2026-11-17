using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class BatchAttachmentExtractor
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = "PdfFiles";
        // Path for the consolidated ZIP archive
        const string outputZipPath = "AllAttachments.zip";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Create (or overwrite) the ZIP archive
        using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
        using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
        {
            // Process each PDF file in the input folder
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);

                try
                {
                    // Open the PDF document inside a using block for deterministic disposal
                    using (Document doc = new Document(pdfPath))
                    {
                        // If the document has no embedded files, continue to next file
                        if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                        {
                            Console.WriteLine($"No attachments found in: {pdfPath}");
                            continue;
                        }

                        // Iterate over all embedded files in the current PDF
                        foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                        {
                            // Build a unique entry name inside the ZIP: <pdfFileName>/<embeddedFileName>
                            string entryName = $"{pdfFileName}/{fileSpec.Name}";

                            // Create a new entry in the ZIP archive
                            ZipArchiveEntry zipEntry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);

                            // Write the embedded file data into the ZIP entry
                            using (Stream entryStream = zipEntry.Open())
                            {
                                Stream content = fileSpec.Contents;
                                if (content == null)
                                    continue; // skip if there is no data

                                if (content.CanSeek)
                                    content.Position = 0;

                                content.CopyTo(entryStream);
                            }

                            Console.WriteLine($"Extracted: {entryName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"All attachments have been consolidated into: {outputZipPath}");
    }
}
