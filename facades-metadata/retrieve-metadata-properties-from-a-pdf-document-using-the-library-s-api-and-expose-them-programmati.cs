using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class PdfMetadataExtractor
{
    // Retrieves standard and custom metadata from a PDF file.
    // Returns a dictionary where key = metadata name, value = metadata value.
    public static Dictionary<string, string> GetMetadata(string pdfPath, string password = null)
    {
        var metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Use PdfFileInfo facade to access document information.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo info = password == null
                                   ? new PdfFileInfo(pdfPath)
                                   : new PdfFileInfo(pdfPath, password))
        {
            // Standard properties (all have getters and setters).
            metadata["Title"]        = info.Title        ?? string.Empty;
            metadata["Author"]       = info.Author       ?? string.Empty;
            metadata["Subject"]      = info.Subject      ?? string.Empty;
            metadata["Keywords"]     = info.Keywords     ?? string.Empty;
            metadata["Creator"]      = info.Creator      ?? string.Empty;
            metadata["Producer"]     = info.Producer     ?? string.Empty;
            metadata["CreationDate"] = info.CreationDate ?? string.Empty; // already a string
            metadata["ModDate"]      = info.ModDate      ?? string.Empty; // already a string
            metadata["NumberOfPages"] = info.NumberOfPages.ToString();
            metadata["IsEncrypted"]    = info.IsEncrypted.ToString();
            metadata["HasOpenPassword"] = info.HasOpenPassword.ToString();
            metadata["HasEditPassword"] = info.HasEditPassword.ToString();

            // Custom metadata can be accessed via GetMetaInfo.
            // Example: retrieve a custom field named "Company".
            string customCompany = info.GetMetaInfo("Company");
            if (!string.IsNullOrEmpty(customCompany))
                metadata["Company"] = customCompany;

            // Enumerate all custom entries present in the Header dictionary.
            // Header is a Dictionary<string,string> where each entry is a custom metadata pair.
            if (info.Header != null && info.Header.Count > 0)
            {
                foreach (var kv in info.Header)
                {
                    // Avoid overwriting standard keys.
                    if (!metadata.ContainsKey(kv.Key))
                        metadata[kv.Key] = kv.Value;
                }
            }
        }

        return metadata;
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Retrieve metadata (no password in this example).
        Dictionary<string, string> meta = GetMetadata(inputPdf);

        // Output the collected metadata.
        Console.WriteLine("PDF Metadata:");
        foreach (var kvp in meta)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}
