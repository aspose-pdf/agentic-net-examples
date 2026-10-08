using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "portfolio.pdf";          // input PDF Portfolio
        const string outputRoot = "ExtractedFiles";      // root folder for extracted files

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output root directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Aspose.Pdf exposes embedded files as a collection of FileSpecification objects.
                // The Name property may contain a relative path (e.g., "Folder1/SubFolder/file.txt").
                // We recreate that hierarchy under the outputRoot folder.
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    SaveFileSpecification(fileSpec, outputRoot);
                }
            }

            Console.WriteLine($"All embedded files have been extracted to '{outputRoot}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Saves a single FileSpecification (embedded file) to the appropriate location under basePath.
    private static void SaveFileSpecification(FileSpecification fileSpec, string basePath)
    {
        if (fileSpec == null) return;

        // The Name may contain folder separators – use it to build the target path.
        string relativePath = fileSpec.Name ?? "unnamed";
        string targetPath = Path.Combine(basePath, relativePath);

        // Ensure the directory for the file exists.
        string directory = Path.GetDirectoryName(targetPath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Write the file's content to disk.
        using (FileStream outStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
        using (Stream content = fileSpec.Contents)
        {
            content?.CopyTo(outStream);
        }
    }
}
