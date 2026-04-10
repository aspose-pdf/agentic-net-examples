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

        // Load the PDF file information using the Facade constructor (create/load rule)
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Example modification: change the title metadata
            pdfInfo.Title = "Updated Title";

            // Ensure the target file is writable before attempting to save
            PrepareWritableOutput(outputPath);

            bool saved = false;
            try
            {
                saved = pdfInfo.SaveNewInfo(outputPath);
            }
            // Handle both the generic IOException and the more specific UnauthorizedAccessException
            catch (IOException ioEx) when (IsReadOnlyError(ioEx))
            {
                // If the failure is due to a read‑only attribute, clear it and retry
                ClearReadOnlyAttribute(outputPath);
                saved = pdfInfo.SaveNewInfo(outputPath);
            }
            catch (UnauthorizedAccessException)
            {
                // In case the OS throws UnauthorizedAccessException for a read‑only file
                ClearReadOnlyAttribute(outputPath);
                saved = pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine(saved
                ? $"Metadata saved successfully to '{outputPath}'."
                : $"Failed to save metadata to '{outputPath}'.");
        }
    }

    // Checks whether the IOException is caused by a read‑only file attribute
    private static bool IsReadOnlyError(IOException ex)
    {
        // Platform‑independent heuristic: look for typical messages
        return ex.Message.IndexOf("read-only", StringComparison.OrdinalIgnoreCase) >= 0
               || ex.Message.IndexOf("access to the path", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    // Removes the read‑only flag from the specified file if it exists
    private static void ClearReadOnlyAttribute(string path)
    {
        if (!File.Exists(path))
            return;

        FileInfo fi = new FileInfo(path);
        if (fi.IsReadOnly)
        {
            // Clear the read‑only flag and reset other attributes to Normal
            fi.IsReadOnly = false;
            fi.Attributes = FileAttributes.Normal;
        }
    }

    // Ensures that the output file can be written to (creates an empty file if needed)
    private static void PrepareWritableOutput(string path)
    {
        if (File.Exists(path))
        {
            // If the file exists and is read‑only, clear the attribute now
            ClearReadOnlyAttribute(path);
        }
        else
        {
            // Create an empty file so that SaveNewInfo has a target to overwrite
            using (FileStream fs = File.Create(path)) { }
        }
    }
}
