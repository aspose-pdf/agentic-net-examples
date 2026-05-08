using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Example list of PDF files to process.
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        foreach (string path in pdfFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                continue;
            }

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(path))
            {
                // ------------------------------------------------------------
                // 1. Number of distinct fonts used in the document.
                //    Aspose.Pdf.Document no longer exposes a direct Fonts collection.
                //    Instead, iterate over each page's Resources.Fonts collection and
                //    collect unique font names.
                // ------------------------------------------------------------
                var fontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Page page in doc.Pages)
                {
                    if (page.Resources?.Fonts != null)
                    {
                        foreach (var font in page.Resources.Fonts)
                        {
                            // Font objects expose the FontName property which uniquely
                            // identifies the font used on the page.
                            if (font != null && !string.IsNullOrEmpty(font.FontName))
                                fontNames.Add(font.FontName);
                        }
                    }
                }
                int fontCount = fontNames.Count;

                // ------------------------------------------------------------
                // 2. Number of form fields (AcroForm fields) in the document.
                // ------------------------------------------------------------
                int formFieldCount = doc.Form?.Count ?? 0;

                // ------------------------------------------------------------
                // 3. Number of tables discovered via tagged structure (if present).
                // ------------------------------------------------------------
                int tableCount = 0;
                ITaggedContent tagged = doc.TaggedContent;
                if (tagged?.RootElement != null)
                {
                    StructureElement root = tagged.RootElement;
                    // Find all TableElement instances recursively.
                    var tables = root.FindElements<TableElement>(true);
                    tableCount = tables?.Count ?? 0;
                }

                // Log the statistics.
                Console.WriteLine($"Document: {Path.GetFileName(path)}");
                Console.WriteLine($"  Fonts: {fontCount}");
                Console.WriteLine($"  Tables: {tableCount}");
                Console.WriteLine($"  Form fields: {formFieldCount}");
                Console.WriteLine();
            }
        }
    }
}
