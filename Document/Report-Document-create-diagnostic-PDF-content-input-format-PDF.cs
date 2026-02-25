using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string reportPath = "diagnostic_report.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document src = new Document(inputPath))
            {
                // Collect basic diagnostics
                int pageCount = src.Pages.Count;
                string title = src.Info.Title ?? "(none)";
                string author = src.Info.Author ?? "(none)";
                string subject = src.Info.Subject ?? "(none)";
                string keywords = src.Info.Keywords ?? "(none)";

                // Create a new PDF to hold the diagnostic report
                using (Document report = new Document())
                {
                    // Add the first page of the report
                    Page reportPage = report.Pages.Add();

                    // Helper to add a line of text at a given Y coordinate
                    void AddLine(string text, double y)
                    {
                        TextFragment tf = new TextFragment(text);
                        tf.Position = new Position(50, y);
                        tf.TextState.FontSize = 12;
                        reportPage.Paragraphs.Add(tf);
                    }

                    double yPos = 800;

                    // Header
                    AddLine("Diagnostic Report", yPos);
                    yPos -= 30;

                    // Basic file info
                    AddLine($"Source file: {Path.GetFileName(inputPath)}", yPos);
                    yPos -= 20;
                    AddLine($"Pages: {pageCount}", yPos);
                    yPos -= 20;
                    AddLine($"Title: {title}", yPos);
                    yPos -= 20;
                    AddLine($"Author: {author}", yPos);
                    yPos -= 20;
                    AddLine($"Subject: {subject}", yPos);
                    yPos -= 20;
                    AddLine($"Keywords: {keywords}", yPos);
                    yPos -= 30;

                    // Extract a short text snippet from each page
                    for (int i = 1; i <= pageCount; i++) // 1‑based indexing
                    {
                        TextAbsorber absorber = new TextAbsorber();
                        src.Pages[i].Accept(absorber);
                        string snippet = absorber.Text;
                        if (snippet.Length > 200)
                            snippet = snippet.Substring(0, 200) + "...";

                        AddLine($"Page {i} snippet: {snippet}", yPos);
                        yPos -= 20;

                        // Add a new page to the report if we run out of space
                        if (yPos < 50)
                        {
                            reportPage = report.Pages.Add();
                            yPos = 800;
                        }
                    }

                    // Save the diagnostic PDF
                    report.Save(reportPath);
                }
            }

            Console.WriteLine($"Diagnostic report saved to '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}