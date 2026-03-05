using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor with file path)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (page indexing is 1‑based)
            foreach (Page page in doc.Pages)
            {
                // ---------- Header ----------
                HeaderFooter header = new HeaderFooter();
                TextFragment headerTf = new TextFragment("Sample Header");
                headerTf.TextState.Font = FontRepository.FindFont("Helvetica");
                headerTf.TextState.FontSize = 12;
                headerTf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                header.Paragraphs.Add(headerTf);
                page.Header = header;

                // ---------- Footer ----------
                HeaderFooter footer = new HeaderFooter();
                TextFragment footerTf = new TextFragment($"Page {page.Number}");
                footerTf.TextState.Font = FontRepository.FindFont("Helvetica");
                footerTf.TextState.FontSize = 12;
                footerTf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                footer.Paragraphs.Add(footerTf);
                page.Footer = footer;

                // ---------- Watermark ----------
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.Text = "CONFIDENTIAL";
                // Use TextState to set font, size and color (correct API for WatermarkArtifact)
                watermark.TextState.Font = FontRepository.FindFont("Helvetica");
                watermark.TextState.FontSize = 72; // large size for visibility
                watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                // Correct property name for opacity
                watermark.Opacity = 0.3; // 30% opacity
                watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                watermark.ArtifactVerticalAlignment = VerticalAlignment.Center;
                page.Artifacts.Add(watermark);

                // ---------- Custom Margins ----------
                // Define a uniform margin (e.g., 0.5 inch = 36 points)
                const double margin = 36;
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                // Adjust the CropBox to create visible margins
                page.CropBox = new Aspose.Pdf.Rectangle(
                    mediaBox.LLX + margin,
                    mediaBox.LLY + margin,
                    mediaBox.URX - margin,
                    mediaBox.URY - margin);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
