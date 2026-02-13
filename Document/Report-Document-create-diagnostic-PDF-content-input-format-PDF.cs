using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the diagnostic report PDF
        const string inputPath = "input.pdf";
        const string outputPath = "diagnostic_report.pdf";

        try
        {
            // Verify that the source PDF exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
                return;
            }

            // Load the existing PDF (load rule)
            Document sourceDoc = new Document(inputPath);

            // Create a new PDF document for the diagnostic report
            Document reportDoc = new Document();

            // Add a single page to the report
            Page reportPage = reportDoc.Pages.Add();

            // Build diagnostic information about the source PDF
            string reportText = $"Diagnostic Report for \"{Path.GetFileName(inputPath)}\"\n" +
                                $"Generated on: {DateTime.Now}\n" +
                                $"Total pages: {sourceDoc.Pages.Count}\n";

            // Append per‑page details
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                Page srcPage = sourceDoc.Pages[i];
                reportText += $"\nPage {i}:\n" +
                              $"  Size (points): {srcPage.PageInfo.Width} × {srcPage.PageInfo.Height}\n" +
                              $"  Annotations: {srcPage.Annotations.Count}\n";
            }

            // Create a TextFragment with the diagnostic text
            TextFragment tf = new TextFragment(reportText)
            {
                // Position the text at the top‑left corner of the page (0,0 is bottom‑left, so we offset by the page height)
                Position = new Position(0, reportPage.PageInfo.Height),
                // Add margins for better readability
                Margin = new MarginInfo { Top = 20, Bottom = 20, Left = 20, Right = 20 }
            };

            // Add the TextFragment to the page
            reportPage.Paragraphs.Add(tf);

            // Save the diagnostic PDF (save rule)
            reportDoc.Save(outputPath);
            Console.WriteLine($"Diagnostic report saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
            // Optionally, for debugging purposes you could output the stack trace:
            // Console.Error.WriteLine(ex.StackTrace);
        }
    }
}
