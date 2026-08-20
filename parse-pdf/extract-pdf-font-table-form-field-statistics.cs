using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // List of PDF files to analyze
        string[] pdfFiles = { "document1.pdf", "document2.pdf", "document3.pdf" };

        foreach (string filePath in pdfFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Open the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(filePath))
            {
                // ----- Count distinct fonts used in the document -----
                var fontNames = new HashSet<string>();
                foreach (Page page in doc.Pages)
                {
                    if (page.Resources?.Fonts != null)
                    {
                        foreach (var fontInfo in page.Resources.Fonts)
                        {
                            // FontInfo.FontName gives the name of the font
                            fontNames.Add(fontInfo.FontName);
                        }
                    }
                }
                int fontCount = fontNames.Count;

                // ----- Count tables present in the document -----
                int tableCount = 0;
                foreach (Page page in doc.Pages)
                {
                    foreach (var element in page.Paragraphs)
                    {
                        if (element is Table)
                            tableCount++;
                    }
                }

                // ----- Count form fields (AcroForm fields) if any -----
                int formFieldCount = 0;
                if (doc.Form?.Fields != null)
                {
                    // Fields implements IEnumerable, use Count() extension method
                    formFieldCount = doc.Form.Fields.Count();
                }

                // Output the statistics
                Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Fonts: {fontCount}");
                Console.WriteLine($"  Tables: {tableCount}");
                Console.WriteLine($"  Form fields: {formFieldCount}");
                Console.WriteLine();
            }
        }
    }
}
