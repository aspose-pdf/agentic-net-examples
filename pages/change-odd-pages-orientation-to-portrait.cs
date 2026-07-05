using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate over odd‑numbered pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i += 2)
                {
                    Page page = doc.Pages[i];
                    Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                    double width = mediaBox.URX - mediaBox.LLX;
                    double height = mediaBox.URY - mediaBox.LLY;

                    // If the page is landscape, swap width and height
                    if (width > height)
                    {
                        // Preserve the lower‑left corner, swap dimensions
                        Aspose.Pdf.Rectangle newBox = new Aspose.Pdf.Rectangle(
                            mediaBox.LLX,
                            mediaBox.LLY,
                            mediaBox.LLX + height,
                            mediaBox.LLY + width);

                        page.MediaBox = newBox;

                        // Update the PageInfo flag if present
                        if (page.PageInfo != null)
                        {
                            page.PageInfo.IsLandscape = false;
                        }
                    }
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}