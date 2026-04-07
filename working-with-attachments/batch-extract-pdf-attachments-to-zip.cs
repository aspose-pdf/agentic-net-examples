using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "pdfs";

        // Path of the consolidated ZIP archive that will hold all extracted attachments
        const string outputZipPath = "attachments.zip";

        // Ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Create (or overwrite) the ZIP archive
        using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
        using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            // Iterate over every PDF file in the input folder
            foreach (string pdfFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document pdfDoc = new Document(pdfFilePath))
                {
                    // The EmbeddedFiles collection holds all embedded files in the PDF
                    foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
                    {
                        // Build a unique entry name: <pdf‑name>/<embedded‑file‑name>
                        string entryName = $"{Path.GetFileNameWithoutExtension(pdfFilePath)}/{fileSpec.Name}";

                        // Create a new entry in the ZIP archive
                        ZipArchiveEntry zipEntry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);

                        // Write the embedded file's binary content into the ZIP entry
                        using (Stream entryStream = zipEntry.Open())
                        {
                            // Ensure the stream is positioned at the beginning before copying
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;
                            fileSpec.Contents.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"All embedded files have been extracted to '{outputZipPath}'.");
    }
}
