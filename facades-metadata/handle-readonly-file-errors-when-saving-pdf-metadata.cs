using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF file information using Facades API
        PdfFileInfo info = new PdfFileInfo(pdfPath);

        // Example modification: update the document title
        info.Title = "Updated Title";

        // Attempt to save the new information; handle read‑only attribute issues
        try
        {
            // SaveNewInfo requires the output file path (can be the same file to overwrite)
            info.SaveNewInfo(pdfPath);
            Console.WriteLine("Metadata saved successfully.");
        }
        catch (IOException _ ) when (IsReadOnlyAttributeIssue(pdfPath))
        {
            // Clear the read‑only attribute
            FileInfo fi = new FileInfo(pdfPath);
            fi.IsReadOnly = false;

            // Retry saving after attribute change
            try
            {
                info.SaveNewInfo(pdfPath);
                Console.WriteLine("Metadata saved after clearing read‑only attribute.");
            }
            catch (Exception retryEx)
            {
                Console.Error.WriteLine($"Retry failed: {retryEx.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving metadata: {ex.Message}");
        }
    }

    // Determines whether the IOException is likely caused by a read‑only file attribute
    static bool IsReadOnlyAttributeIssue(string path)
    {
        return (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
    }
}
