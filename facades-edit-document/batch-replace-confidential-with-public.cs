using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class BatchReplace
{
    static void Main()
    {
        // Paths to the input archive (ZIP) and the output archive
        const string inputArchivePath  = "input_archive.zip";
        const string outputArchivePath = "output_archive.zip";

        // Temporary folder for extracting and processing PDFs
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfBatchReplace_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // -----------------------------------------------------------------
            // Extract all entries from the input ZIP archive to the temp folder
            // -----------------------------------------------------------------
            using (FileStream zipToOpen = new FileStream(inputArchivePath, FileMode.Open, FileAccess.Read))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(tempFolder, entry.FullName));

                    // Ensure the destination path is within the temp folder (security)
                    if (!destinationPath.StartsWith(tempFolder, StringComparison.OrdinalIgnoreCase))
                        continue;

                    // Create subdirectories as needed
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    // Extract the entry
                    entry.ExtractToFile(destinationPath, overwrite: true);
                }
            }

            // ---------------------------------------------------------------
            // Process each PDF file: replace "Confidential" with "Public"
            // ---------------------------------------------------------------
            foreach (string pdfPath in Directory.EnumerateFiles(tempFolder, "*.pdf", SearchOption.AllDirectories))
            {
                // PdfContentEditor does NOT implement IDisposable, so no using block needed
                PdfContentEditor editor = new PdfContentEditor();

                // Bind the PDF file
                editor.BindPdf(pdfPath);

                // Perform the text replacement
                editor.ReplaceText("Confidential", "Public");

                // Save changes back to the same file (overwrites original)
                editor.Save(pdfPath);
            }

            // ---------------------------------------------------------------
            // Create the output ZIP archive containing the modified files
            // ---------------------------------------------------------------
            // Delete existing output archive if it exists
            if (File.Exists(outputArchivePath))
                File.Delete(outputArchivePath);

            using (FileStream zipToCreate = new FileStream(outputArchivePath, FileMode.CreateNew))
            using (ZipArchive outputArchive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
            {
                // Add all files from the temp folder preserving relative paths
                foreach (string filePath in Directory.EnumerateFiles(tempFolder, "*", SearchOption.AllDirectories))
                {
                    string entryName = Path.GetRelativePath(tempFolder, filePath).Replace(Path.DirectorySeparatorChar, '/');
                    outputArchive.CreateEntryFromFile(filePath, entryName);
                }
            }

            Console.WriteLine("Batch replacement completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary folder
            if (Directory.Exists(tempFolder))
            {
                try { Directory.Delete(tempFolder, recursive: true); }
                catch { /* ignore cleanup errors */ }
            }
        }
    }
}