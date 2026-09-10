using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo implements IDisposable, so use a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Access the underlying Document to obtain all metadata keys.
            Document doc = pdfInfo.Document;

            // Get all metadata keys from the document.
            ICollection<string> allKeys = doc.Metadata.Keys;

            // Filter out predefined keys (Title, Author, etc.) to keep only custom ones.
            List<string> customKeys = allKeys
                .Where(k => !DocumentInfo.IsPredefinedKey(k))
                .ToList();

            // Sort the custom keys alphabetically.
            customKeys.Sort(StringComparer.OrdinalIgnoreCase);

            // Display each custom key with its corresponding value using GetMetaInfo.
            foreach (string key in customKeys)
            {
                string value = pdfInfo.GetMetaInfo(key);
                Console.WriteLine($"{key}: {value}");
            }
        }
    }
}