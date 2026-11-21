using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Aspose.Pdf.Page page = doc.Pages[pageIndex];

                    // Iterate over the image resources on the page
                    foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                    {
                        // Width and Height are expressed in points (1 point = 1/72 inch)
                        // For reporting we output these values directly
                        Console.WriteLine(
                            $"Page {pageIndex}: Image - Width = {img.Width} pt, Height = {img.Height} pt");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}