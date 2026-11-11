using System;
using System.IO;
using System.Text;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string reportPath = "report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Capture page properties before any edits
            StringBuilder beforeInfo = new StringBuilder();
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                beforeInfo.AppendLine(
                    $"Page {i}: Size={page.PageInfo.Width}x{page.PageInfo.Height}, " +
                    $"Rotation={page.Rotate}, Zoom=1.0"); // default zoom is 1.0
            }

            // ---- EDIT PAGE PROPERTIES -------------------------------------------------
            // 1. Change size and rotation directly on the Document pages (PdfPageEditor has no SetPageSize/SetPageRotation).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Set A4 size (595x842 points)
                page.PageInfo.Width = 595;
                page.PageInfo.Height = 842;
                // Rotate 180 degrees
                page.Rotate = Rotation.on180;
            }

            // 2. Apply a zoom factor using PdfPageEditor (the editor works on the bound document).
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                // Target all pages – ProcessPages expects a 1‑based int array.
                editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
                editor.Zoom = 1.5f; // 150% magnification
                editor.Save(outputPath);
            }
            // ---------------------------------------------------------------------------

            // Capture page properties after edits
            StringBuilder afterInfo = new StringBuilder();
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                afterInfo.AppendLine(
                    $"Page {i}: Size={page.PageInfo.Width}x{page.PageInfo.Height}, " +
                    $"Rotation={page.Rotate}, Zoom=1.5"); // zoom applied via editor
            }

            // Build the report
            StringBuilder report = new StringBuilder();
            report.AppendLine("=== Page Properties Report ===");
            report.AppendLine();
            report.AppendLine("Before Edits:");
            report.Append(beforeInfo);
            report.AppendLine();
            report.AppendLine("After Edits:");
            report.Append(afterInfo);

            // Write the report to a text file
            File.WriteAllText(reportPath, report.ToString());
        }

        Console.WriteLine("Processing completed. Report saved to 'report.txt'.");
    }
}
