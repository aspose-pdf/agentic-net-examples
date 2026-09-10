using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF file information using the Facade API
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Example modification: set a new title
                pdfInfo.Title = "Updated Title";

                // Save the updated information, handling read‑only file issues
                SaveWithRetry(pdfInfo, outputPath);
            }

            Console.WriteLine($"Metadata saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Attempts to save the PDF info; if the target file is read‑only, clears the attribute and retries.
    static void SaveWithRetry(PdfFileInfo pdfInfo, string outputFile)
    {
        const int maxAttempts = 2;
        int attempt = 0;

        while (true)
        {
            try
            {
                // SaveNewInfo returns true on success; false indicates failure.
                bool saved = pdfInfo.SaveNewInfo(outputFile);
                if (!saved)
                    throw new IOException("SaveNewInfo returned false.");

                break; // Success, exit loop
            }
            catch (IOException ioEx) when (IsReadOnlyCause(ioEx, outputFile) && attempt < maxAttempts - 1)
            {
                // Remove the read‑only attribute and retry
                File.SetAttributes(outputFile, FileAttributes.Normal);
                attempt++;
            }
        }
    }

    // Determines whether the IOException is likely due to a read‑only file attribute.
    static bool IsReadOnlyCause(IOException ex, string path)
    {
        return File.Exists(path) && (File.GetAttributes(path) & FileAttributes.ReadOnly) != 0;
    }
}