using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "FooterNote";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source document to obtain page dimensions.
        using (Document srcDoc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded Document instance.
            using (FormEditor formEditor = new FormEditor(srcDoc))
            {
                // Iterate through all pages (1‑based indexing).
                for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
                {
                    Page page = srcDoc.Pages[pageNum];

                    // Retrieve page size (points; 1 inch = 72 points).
                    float pageWidth = (float)page.PageInfo.Width;
                    float pageHeight = (float)page.PageInfo.Height;

                    // Define desired field size.
                    const float fieldWidth = 120f;
                    const float fieldHeight = 20f; // retained for possible future use

                    // Calculate coordinates: centered horizontally, 20 points above bottom edge.
                    float left = (pageWidth - fieldWidth) / 2f;
                    float bottom = 20f; // distance from bottom edge

                    // Move (copy) the field to the new location on the same page.
                    formEditor.CopyInnerField(fieldName, fieldName, pageNum, left, bottom);
                }

                // Persist changes to the output PDF.
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field \"{fieldName}\" moved to bottom margin of each page. Output saved to '{outputPath}'.");
    }
}
