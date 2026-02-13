using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Validate input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Customize page margins for all pages
            MarginInfo margins = new MarginInfo
            {
                Top = 50,
                Bottom = 50,
                Left = 40,
                Right = 40
            };
            foreach (Page page in pdfDocument.Pages)
            {
                page.PageInfo.Margin = margins;
            }

            // Create a semi‑transparent text watermark
            TextStamp watermark = new TextStamp("CONFIDENTIAL")
            {
                // Place watermark at the center of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                // Render behind page content
                Background = true,
                // Slightly transparent
                Opacity = 0.3,
                // Rotate 45 degrees (arbitrary angle)
                RotateAngle = 45
            };
            // Configure text appearance
            watermark.TextState.Font = FontRepository.FindFont("Arial");
            watermark.TextState.FontSize = 72;
            watermark.TextState.ForegroundColor = Color.Gray;

            // Create a header stamp
            TextStamp header = new TextStamp("My Company")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                TopMargin = 20,
                Background = false,
                Opacity = 0.5
            };
            header.TextState.Font = FontRepository.FindFont("Arial");
            header.TextState.FontSize = 14;
            header.TextState.ForegroundColor = Color.DarkBlue;

            // Create a footer with page numbers
            PageNumberStamp footer = new PageNumberStamp("Page # of " + pdfDocument.Pages.Count)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                BottomMargin = 20,
                Background = false,
                Opacity = 0.5
            };
            footer.TextState.Font = FontRepository.FindFont("Arial");
            footer.TextState.FontSize = 12;
            footer.TextState.ForegroundColor = Color.DarkGray;

            // Apply stamps to every page
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(watermark);
                page.AddStamp(header);
                page.AddStamp(footer);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Document saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}