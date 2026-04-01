using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_stamps.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document document = new Document(inputPath))
            {
                int pageCount = document.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    Page page = document.Pages[pageIndex];

                    // Create a text stamp for the current page
                    TextStamp stamp = new TextStamp($"Page {pageIndex}");
                    stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                    stamp.TextState.FontSize = 12;
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Calculate rotation angle based on page index (e.g., 30° per page)
                    double rotationAngle = (pageIndex * 30) % 360;
                    // Use RotateAngle (float) instead of non‑existent Rotation property
                    stamp.RotateAngle = (float)rotationAngle;

                    // Optional: centre the stamp on the page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment = VerticalAlignment.Center;

                    // Add the stamp to the current page
                    page.AddStamp(stamp);
                }

                document.Save(outputPath);
                Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
