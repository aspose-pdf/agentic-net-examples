using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "brochure_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Standard brochure size: A4 landscape (842 x 595 points)
                const double brochureWidth = 842;   // points
                const double brochureHeight = 595;  // points

                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;

                    // Rotate portrait pages to landscape using the Page.Rotate property
                    if (height > width)
                    {
                        page.Rotate = Rotation.on90; // 90° clockwise
                    }

                    // Set page size to brochure dimensions
                    page.PageInfo.Width = brochureWidth;
                    page.PageInfo.Height = brochureHeight;
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Brochure PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
