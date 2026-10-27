using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Load the PDF to obtain page dimensions (required for positioning the stamps)
        using (Document doc = new Document(inputPath))
        {
            // Assume we watermark the first page; adjust as needed
            int pageNumber = 1;
            Page page = doc.Pages[pageNumber];
            double pageHeight = page.PageInfo.Height;
            double pageWidth = page.PageInfo.Width;

            // Watermark configuration
            string[] watermarkLines = { "CONFIDENTIAL", "DO NOT DISTRIBUTE", "AUTHORIZED PERSONNEL ONLY" };
            double marginTop = 100;               // distance from top edge
            double lineHeight = 30;               // custom line spacing between lines
            double startY = pageHeight - marginTop; // Y coordinate of the first line (from bottom)
            // X position is handled by HorizontalAlignment = Center, so XIndent stays 0

            // Add each line as a separate TextStamp with calculated Y position
            for (int i = 0; i < watermarkLines.Length; i++)
            {
                TextStamp stamp = new TextStamp(watermarkLines[i])
                {
                    // Center the text horizontally
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Position the stamp; YIndent is measured from the bottom of the page
                    YIndent = startY - i * lineHeight,
                    // Optional: keep XIndent at 0 because we use Center alignment
                    XIndent = 0
                };

                // Optional: style the watermark text
                stamp.TextState.Font = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize = 36;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // red
                stamp.Opacity = 0.5; // semi‑transparent

                // Apply the stamp to the specified page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
