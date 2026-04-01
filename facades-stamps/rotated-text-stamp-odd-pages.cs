using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText = "ROTATED";
        const float rotationAngle = 30f;
        const float leftIndent = 0f;   // left margin
        const float topIndent = 100f; // distance from bottom of page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    if (pageIndex % 2 == 1) // odd‑numbered pages
                    {
                        Page page = doc.Pages[pageIndex];

                        // Create a text stamp (TextStamp, not Facades.Stamp)
                        TextStamp stamp = new TextStamp(stampText);
                        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                        stamp.TextState.FontSize = 24;
                        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                        // Correct property for rotation
                        stamp.RotateAngle = rotationAngle; // rotate 30 degrees
                        stamp.XIndent = leftIndent;      // left margin
                        stamp.YIndent = topIndent;       // distance from bottom
                        // Correct property for background/foreground
                        stamp.Background = false;        // foreground stamp

                        page.AddStamp(stamp);
                    }
                }
                doc.Save(outputPath);
                Console.WriteLine($"Rotated stamp applied. Output saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
