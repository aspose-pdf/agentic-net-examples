using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main(string[] args)
    {
        // Resolve the input PDF path relative to the executable directory (or use first argument if supplied)
        string inputPath = args.Length > 0 ? args[0] : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.pdf");
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.pdf");

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document document = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= document.Pages.Count; pageIndex++)
                {
                    Page page = document.Pages[pageIndex];
                    OperatorCollection originalOps = page.Contents;

                    // Build a new operator list where the target text is replaced.
                    List<Operator> updatedOps = new List<Operator>();
                    foreach (Operator op in originalOps)
                    {
                        if (op is ShowText showText && !string.IsNullOrEmpty(showText.Text) && showText.Text.Contains("Hello"))
                        {
                            // Replace the occurrence of "Hello" with "Hi"
                            string newText = showText.Text.Replace("Hello", "Hi");
                            ShowText replacement = new ShowText(newText);
                            // Preserve the original operator index (optional – keeps ordering consistent)
                            replacement.Index = showText.Index;
                            updatedOps.Add(replacement);
                        }
                        else
                        {
                            // Keep the original operator unchanged
                            updatedOps.Add(op);
                        }
                    }

                    // Apply the new operator list to the page.
                    originalOps.Clear();
                    foreach (Operator op in updatedOps)
                    {
                        originalOps.Add(op);
                    }
                }

                // Save the modified PDF
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to: {outputPath}");
            }
        }
        catch (Aspose.Pdf.InvalidPdfFileFormatException ex)
        {
            Console.WriteLine($"Failed to load PDF – the file may be corrupted or not a valid PDF. Details: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
