using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve the input directory. If the expected folder does not exist, fall back to the current working directory.
        string inputDirectory = ResolveInputDirectory("InputPdfs");

        // Path for the consolidated ZIP archive that will hold all extracted attachments
        string zipArchivePath = Path.Combine(Directory.GetCurrentDirectory(), "AttachmentsArchive.zip");

        // Retrieve all PDF files from the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in directory: {inputDirectory}");
            return;
        }

        try
        {
            // Create (or overwrite) the ZIP archive and keep it open for the duration of the extraction
            using (FileStream zipFileStream = new FileStream(zipArchivePath, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Update))
            {
                // Process each PDF file individually
                foreach (string pdfPath in pdfFiles)
                {
                    if (!File.Exists(pdfPath))
                    {
                        Console.WriteLine($"File not found: {pdfPath}");
                        continue;
                    }

                    // Load the PDF document inside a using block to ensure deterministic disposal
                    using (Document pdfDocument = new Document(pdfPath))
                    {
                        // If the document has no embedded files, skip it
                        if (pdfDocument.EmbeddedFiles == null || pdfDocument.EmbeddedFiles.Count == 0)
                        {
                            Console.WriteLine($"No attachments in: {Path.GetFileName(pdfPath)}");
                            continue;
                        }

                        // Aspose collections are 1‑based; iterate accordingly
                        for (int i = 1; i <= pdfDocument.EmbeddedFiles.Count; i++)
                        {
                            var embeddedFile = pdfDocument.EmbeddedFiles[i];

                            // Construct a unique entry name: original PDF name + embedded file name
                            string entryName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_{embeddedFile.Name}";

                            // Create a new entry in the ZIP archive
                            ZipArchiveEntry zipEntry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);

                            // Copy the embedded file's content into the ZIP entry
                            using (Stream entryStream = zipEntry.Open())
                            using (Stream embeddedStream = embeddedFile.Contents)
                            {
                                if (embeddedStream.CanSeek)
                                    embeddedStream.Position = 0;
                                embeddedStream.CopyTo(entryStream);
                            }

                            Console.WriteLine($"Extracted: {entryName}");
                        }
                    }
                }
            }

            Console.WriteLine($"All attachments have been saved to '{zipArchivePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[Error] {ex.Message}");
        }
    }

    /// <summary>
    /// Returns a valid directory path for the given relative folder name.
    /// If the folder does not exist, it is created under the current working directory.
    /// </summary>
    private static string ResolveInputDirectory(string relativeFolderName)
    {
        string baseDir = Directory.GetCurrentDirectory();
        string fullPath = Path.Combine(baseDir, relativeFolderName);
        if (!Directory.Exists(fullPath))
        {
            Console.WriteLine($"Input directory '{fullPath}' not found. Creating it.");
            Directory.CreateDirectory(fullPath);
        }
        return fullPath;
    }
}
