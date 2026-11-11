using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF file information via Facades API
        PdfFileInfo info = new PdfFileInfo(pdfPath);

        // Example modification: set a new title
        info.Title = "Updated Document Title";

        // Save the updated information to a temporary file first
        string tempPath = Path.Combine(Path.GetDirectoryName(pdfPath) ?? ".", Guid.NewGuid() + ".pdf");

        try
        {
            info.SaveNewInfo(tempPath); // <-- correct overload requires an output path
            ReplaceOriginal(pdfPath, tempPath);
            Console.WriteLine("PDF information saved successfully.");
        }
        catch (IOException ioEx)
        {
            // Check if the failure is due to a read‑only attribute
            FileAttributes attrs = File.GetAttributes(pdfPath);
            if ((attrs & FileAttributes.ReadOnly) != 0)
            {
                Console.WriteLine("File is read‑only. Removing read‑only attribute and retrying...");

                // Remove the read‑only flag
                File.SetAttributes(pdfPath, attrs & ~FileAttributes.ReadOnly);

                // Retry saving the information
                try
                {
                    info.SaveNewInfo(tempPath);
                    ReplaceOriginal(pdfPath, tempPath);
                    Console.WriteLine("PDF information saved after clearing read‑only attribute.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Retry failed: {ex.Message}");
                }
            }
            else
            {
                // Not a read‑only issue; report the IO error
                Console.Error.WriteLine($"IO error: {ioEx.Message}");
            }
        }
        catch (Exception ex)
        {
            // General exception handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Replaces the original PDF with the newly saved file and cleans up the temporary file.
    /// </summary>
    private static void ReplaceOriginal(string originalPath, string tempPath)
    {
        // Ensure the original file can be overwritten (it may still be read‑only after the previous step)
        FileAttributes originalAttrs = File.GetAttributes(originalPath);
        if ((originalAttrs & FileAttributes.ReadOnly) != 0)
        {
            File.SetAttributes(originalPath, originalAttrs & ~FileAttributes.ReadOnly);
        }

        // Overwrite the original file with the temporary one
        File.Copy(tempPath, originalPath, overwrite: true);
        File.Delete(tempPath);
    }
}
