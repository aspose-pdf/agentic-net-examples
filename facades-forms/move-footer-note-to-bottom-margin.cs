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
        const string newFieldName = "FooterNoteMoved";
        const float bottomMargin = 20f; // points from bottom edge

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF to obtain page dimensions.
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded Document (no destination in ctor).
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Iterate over all pages (1‑based indexing).
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Calculate X coordinate (center of the page) – cast to float because Width is double.
                    float x = (float)(page.PageInfo.Width / 2.0);

                    // Y coordinate is the desired bottom margin.
                    float y = bottomMargin;

                    // Copy the existing field to the new location on the same page.
                    formEditor.CopyInnerField(fieldName, newFieldName, i, x, y);
                }

                // Save the modified PDF to the specified output path.
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field '{fieldName}' moved to bottom margin on each page. Output saved to '{outputPath}'.");
    }
}
