using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextStamp, TextState, FontStyles

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Create a TextStamp that will serve as a full‑page watermark
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    Background = true,                     // place behind existing content
                    Opacity = 0.2,                         // semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Configure the TextState (read‑only property, modify its members directly)
                stamp.TextState.FontSize = 120;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Apply the stamp to each page, resizing it to cover the whole page
                foreach (Page page in doc.Pages)
                {
                    stamp.Width = page.PageInfo.Width;
                    stamp.Height = page.PageInfo.Height;
                    page.AddStamp(stamp);
                }

                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
