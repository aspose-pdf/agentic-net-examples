using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputFolder = "InputPdfs";
        // Path for the consolidated ZIP archive
        const string outputZip = "AllAttachments.zip";

        // Verify that the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs to process.");
            return;
        }

        // Collect all PDF files
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // Temporary folder to store extracted attachments before zipping
        string tempDir = Path.Combine(Path.GetTempPath(), "PdfAttachments");
        Directory.CreateDirectory(tempDir);

        // Keep track of extracted file paths for later archiving
        List<string> extractedFiles = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
                continue;

            // Load each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Embedded files collection may be null if no attachments exist
                EmbeddedFileCollection attachments = doc.EmbeddedFiles;
                if (attachments != null && attachments.Count > 0)
                {
                    // The collection is 1‑based indexed in Aspose.Pdf
                    for (int i = 1; i <= attachments.Count; i++)
                    {
                        FileSpecification spec = attachments[i];
                        if (spec?.Contents == null)
                            continue;

                        // Build a unique file name to avoid collisions
                        string safeName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_{spec.Name}";
                        string outPath = Path.Combine(tempDir, safeName);

                        // Save the embedded file to the temporary location using the Contents stream
                        using (FileStream outStream = File.Create(outPath))
                        {
                            spec.Contents.CopyTo(outStream);
                        }

                        extractedFiles.Add(outPath);
                    }
                }
            }
        }

        // Create the consolidated ZIP archive
        using (FileStream zipStream = new FileStream(outputZip, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            foreach (string filePath in extractedFiles)
            {
                // Add each extracted attachment to the archive
                archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
            }
        }

        // Clean up temporary files and directory
        foreach (string filePath in extractedFiles)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        if (Directory.Exists(tempDir))
            Directory.Delete(tempDir, true);

        Console.WriteLine($"All attachments have been extracted to '{outputZip}'.");
    }
}
