using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPageDeletion
{
    static void Main()
    {
        // Directory containing the source PDF files
        // Use OS‑agnostic paths – on non‑Windows platforms a Windows style path (e.g., "C:\PdfInput") is treated as a relative path and causes a DirectoryNotFoundException.
        // Replace with a proper absolute path or let the user supply one via arguments/environment variables.
        string inputDirectory = GetAbsolutePath(@"C:\PdfInput");
        // Directory where the processed PDFs will be saved
        string outputDirectory = GetAbsolutePath(@"C:\PdfOutput");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Define the pages to delete (1‑based indexing). Example: delete pages 2 and 3.
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the input directory actually exists before enumerating files.
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        // Iterate over all PDF files in the input directory
        foreach (string inputFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Build the output file path (same file name, different folder)
                string outputFilePath = Path.Combine(outputDirectory, Path.GetFileName(inputFilePath));

                // Create the PdfFileEditor facade (does NOT implement IDisposable)
                PdfFileEditor editor = new PdfFileEditor();

                // Delete the specified pages and save the result
                bool success = editor.Delete(inputFilePath, pagesToDelete, outputFilePath);

                if (success)
                {
                    Console.WriteLine($"Deleted pages from '{Path.GetFileName(inputFilePath)}' -> '{outputFilePath}'");
                }
                else
                {
                    Console.WriteLine($"Failed to delete pages from '{Path.GetFileName(inputFilePath)}'");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(inputFilePath)}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Returns an absolute path that works on any platform.
    /// If the supplied path is already rooted (e.g., starts with a drive letter on Windows), it is returned unchanged.
    /// Otherwise, it is combined with the current working directory.
    /// </summary>
    private static string GetAbsolutePath(string path)
    {
        if (Path.IsPathRooted(path))
        {
            // On non‑Windows platforms a Windows style root ("C:\") is not considered rooted.
            // Detect such cases and treat them as relative.
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                // Convert to a relative path under the current directory to avoid the DirectoryNotFoundException.
                return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), path.Replace('\\', Path.DirectorySeparatorChar)));
            }
            return path;
        }
        return Path.GetFullPath(path);
    }
}
