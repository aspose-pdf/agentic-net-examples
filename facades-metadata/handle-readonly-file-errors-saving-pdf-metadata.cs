using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load PDF meta‑information using the Facade class
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Update desired metadata fields
                pdfInfo.Title  = "Updated Title";
                pdfInfo.Author = "John Doe";

                bool saved = false;

                try
                {
                    // First attempt to save the updated information
                    saved = pdfInfo.SaveNewInfo(outputPath);
                }
                catch (IOException ioEx) when (IsReadOnlyAttributeError(ioEx) || IsAccessDenied(ioEx))
                {
                    // The target file is read‑only or otherwise inaccessible – clear the attribute and retry
                    TryClearReadOnly(outputPath);
                    try
                    {
                        saved = pdfInfo.SaveNewInfo(outputPath);
                    }
                    catch (Exception retryEx)
                    {
                        Console.Error.WriteLine($"Retry failed: {retryEx.Message}");
                        return;
                    }
                }

                if (saved)
                {
                    Console.WriteLine($"Metadata successfully saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("SaveNewInfo returned false; the operation may have failed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // Helper to detect a read‑only file error from an IOException
    static bool IsReadOnlyAttributeError(IOException ex)
    {
        // Look for the typical Windows message that mentions the read‑only attribute
        return ex.Message.IndexOf("read-only", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    // Helper to detect an access‑denied error (e.g., when the file is locked or read‑only on non‑Windows platforms)
    static bool IsAccessDenied(IOException ex)
    {
        return ex.HResult == unchecked((int)0x80070005); // HRESULT for ACCESS_DENIED
    }

    // Clears the ReadOnly attribute if the file exists
    static void TryClearReadOnly(string path)
    {
        if (File.Exists(path))
        {
            var attrs = File.GetAttributes(path);
            if ((attrs & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                File.SetAttributes(path, attrs & ~FileAttributes.ReadOnly);
            }
        }
    }
}