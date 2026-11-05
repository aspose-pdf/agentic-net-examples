using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based; iterate accordingly
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a TextStamp containing the page number
                    TextStamp stamp = new TextStamp($"Page {i}");

                    // Position the stamp at the bottom center of the page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    // Use XIndent/YIndent instead of the non‑existent Margin property
                    stamp.YIndent = 20; // distance from the bottom edge (points)

                    // Optional styling for readability
                    stamp.TextState.FontSize        = 12;
                    stamp.TextState.Font            = FontRepository.FindFont("Arial");
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                    // Apply the stamp to the current page
                    page.AddStamp(stamp);
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Page numbers added successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
