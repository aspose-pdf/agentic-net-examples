using System;
using System.IO;
using System.Collections;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfSummaryReport
{
    static void Main()
    {
        // Folder containing PDF files to analyze
        const string inputFolder = "PdfFiles";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Load each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                int imageCount = 0;
                int graphicCount = 0; // Count of XObject resources (vector graphics, forms, etc.)

                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Images are stored in the page's Resources.Images collection
                    imageCount += page.Resources.Images.Count;

                    // XObjects (FormXObject, ImageXObject, etc.) are stored in Resources.XObjects.
                    // Some older Aspose.Pdf versions expose this collection via reflection only.
                    // Use reflection to stay compatible with all supported versions.
                    var xObjProp = page.Resources.GetType().GetProperty("XObjects");
                    if (xObjProp != null)
                    {
                        var xObjColl = xObjProp.GetValue(page.Resources) as ICollection;
                        if (xObjColl != null)
                            graphicCount += xObjColl.Count;
                    }
                }

                // Extract all text using TextAbsorber
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                int totalTextLength = absorber.Text?.Length ?? 0;

                // Output the summary for the current PDF
                Console.WriteLine($"--- Summary for: {Path.GetFileName(pdfPath)} ---");
                Console.WriteLine($"Images extracted          : {imageCount}");
                Console.WriteLine($"Graphics (XObjects) extracted: {graphicCount}");
                Console.WriteLine($"Total text length (characters): {totalTextLength}");
                Console.WriteLine();
            }
        }
    }
}
