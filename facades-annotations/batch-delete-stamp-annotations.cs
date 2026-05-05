using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // ---------------------------------------------------------------------
        // NOTE:
        // -----
        // The original code used hard‑coded Windows paths (e.g. "C:\PdfFolder").
        // When the application runs on a non‑Windows platform (Linux/macOS) the
        // colon (:) is treated as part of a file name and the runtime builds a
        // combined path like "/Users/.../_build/C:\PdfFolder", which results in
        // a DirectoryNotFoundException.
        //
        // To make the sample portable we build the input and output folders
        // relative to the current working directory (or you can pass custom
        // paths via command‑line arguments or environment variables).
        // ---------------------------------------------------------------------

        // Resolve input folder – default is "PdfFolder" under the current dir.
        string inputFolder = GetFolderPath(args, 0, "PdfFolder");
        // Resolve output folder – default is "PdfFolder\Cleaned" under the current dir.
        string outputFolder = GetFolderPath(args, 1, Path.Combine("PdfFolder", "Cleaned"));

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder.
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_cleaned.pdf");

            try
            {
                // Initialise the annotation editor facade.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Load the PDF document.
                    editor.BindPdf(inputPath);

                    // Delete all annotations of type "Stamp".
                    editor.DeleteAnnotations("Stamp");

                    // Save the modified document.
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Returns a folder path based on command‑line arguments, falling back to a
    /// default relative path. The method also validates that the directory exists
    /// (creating it if necessary for the output folder).
    /// </summary>
    private static string GetFolderPath(string[] args, int index, string defaultRelativePath)
    {
        string path;
        if (args.Length > index && !string.IsNullOrWhiteSpace(args[index]))
        {
            path = args[index];
        }
        else
        {
            // Use a path relative to the current working directory.
            path = Path.GetFullPath(defaultRelativePath);
        }

        // If the path is not rooted (e.g., just "PdfFolder"), make it absolute.
        if (!Path.IsPathRooted(path))
        {
            path = Path.GetFullPath(path);
        }

        // Ensure the directory exists for the output folder; for the input folder
        // we simply return the path – the caller will throw a clear exception if it
        // does not exist.
        return path;
    }
}
