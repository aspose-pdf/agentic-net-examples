using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where watermarked PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the document to obtain the page count and later to add the watermark
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count; // 1‑based indexing; Pages.Count is safe
                double opacity = ComputeOpacity(pageCount);

                // Configure the watermark as a TextStamp (core API replacement for WatermarkArtifact)
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    // Font, size, style and colour are set via TextState
                    TextState =
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 72,
                        FontStyle = FontStyles.Bold,
                        ForegroundColor = Color.Gray
                    },
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Opacity = opacity,
                    // Place the stamp behind the page content (optional, can be false for overlay)
                    Background = true
                };

                // Apply the same stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                doc.Save(outputPath);
                Console.WriteLine($"Processed '{fileName}': pages={pageCount}, opacity={opacity:F2}");
            }
        }
    }

    // Simple opacity calculation; adjust as needed
    static double ComputeOpacity(int pageCount)
    {
        double opacity = 0.2 + pageCount * 0.01;
        return opacity > 0.8 ? 0.8 : opacity;
    }
}