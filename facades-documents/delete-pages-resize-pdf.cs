using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputFilePath = "input.pdf";
        const string outputFilePath = "resized_output.pdf";

        if (!File.Exists(inputFilePath))
        {
            Console.Error.WriteLine($"Input file not found: {inputFilePath}");
            return;
        }

        // Pages to delete (1‑based indexing). Delete in descending order to keep indices stable.
        int[] pagesToDelete = { 2, 4 }; // example: delete page 2 and page 4

        try
        {
            // Load the PDF with Document – it can also be loaded from a Stream if needed.
            using (Document doc = new Document(inputFilePath))
            {
                // Delete pages one by one in descending order.
                foreach (int pageNum in pagesToDelete.OrderByDescending(p => p))
                {
                    if (pageNum >= 1 && pageNum <= doc.Pages.Count)
                    {
                        doc.Pages.Delete(pageNum);
                    }
                }

                // Resize every remaining page to A4 dimensions (595 x 842 points).
                foreach (Page page in doc.Pages)
                {
                    page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
                }

                // Save the resized document.
                doc.Save(outputFilePath);
            }

            Console.WriteLine($"Pages deleted and document resized. Output saved to '{outputFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
