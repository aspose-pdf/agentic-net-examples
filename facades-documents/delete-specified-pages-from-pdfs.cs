using System;
using System.IO;
using Aspose.Pdf.Facades;

/// <summary>
/// Provides PDF page removal utilities using Aspose.Pdf.Facades.
/// </summary>
public static class PdfCleaner
{
    /// <summary>
    /// Deletes the specified pages from each PDF file in <paramref name="inputFiles"/>
    /// and writes the cleaned PDFs to <paramref name="outputFolder"/>.
    /// The cleaned files are named "&lt;originalName&gt;_cleaned.pdf".
    /// </summary>
    /// <param name="inputFiles">Array of full paths to source PDF files.</param>
    /// <param name="pagesToDelete">
    /// Zero‑based page numbers to delete from each document.
    /// The array follows Aspose.Pdf's 1‑based indexing, so values should start at 1.
    /// </param>
    /// <param name="outputFolder">Folder where cleaned PDFs will be saved.</param>
    /// <returns>Array of full paths to the cleaned PDF files.</returns>
    public static string[] DeletePagesFromPdfs(string[] inputFiles, int[] pagesToDelete, string outputFolder)
    {
        if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
        if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));
        if (string.IsNullOrWhiteSpace(outputFolder)) throw new ArgumentException("Output folder must be provided.", nameof(outputFolder));

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        string[] cleanedPaths = new string[inputFiles.Length];

        // Aspose.Pdf.Facades.PdfFileEditor does NOT implement IDisposable,
        // so we instantiate it once and reuse for all files.
        PdfFileEditor editor = new PdfFileEditor();

        for (int i = 0; i < inputFiles.Length; i++)
        {
            string inputPath = inputFiles[i];

            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPath}");

            // Build the output file name.
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.pdf");

            // Use the Delete method defined in the Aspose.Pdf.Facades API.
            // This method deletes the pages specified in the int[] and writes the result to outputPath.
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            if (!success)
                throw new InvalidOperationException($"Failed to delete pages from '{inputPath}'.");

            cleanedPaths[i] = outputPath;
        }

        // No need to call Close() on PdfFileEditor; it does not hold unmanaged resources.
        return cleanedPaths;
    }
}

/// <summary>
/// Minimal entry point required for a console‑type project.
/// The method does not perform any work; it only satisfies the compiler's requirement for a static Main.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank.
    }
}