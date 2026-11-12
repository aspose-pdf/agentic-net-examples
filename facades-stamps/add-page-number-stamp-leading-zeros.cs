using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Format page number with leading zeros (e.g., 001, 002, …)
                string pageNumberText = i.ToString("D3");

                // Create a text stamp for the current page number
                TextStamp stamp = new TextStamp(pageNumberText);

                // Position the stamp at the bottom‑right corner
                stamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;
                stamp.VerticalAlignment   = Aspose.Pdf.VerticalAlignment.Bottom;
                stamp.RightMargin         = 20;   // distance from right edge
                stamp.BottomMargin        = 20;   // distance from bottom edge

                // Set visual appearance of the stamp
                stamp.TextState.Font      = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize  = 12;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Apply the stamp to the current page
                Page page = doc.Pages[i];
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page‑numbered PDF saved to '{outputPath}'.");
    }
}