using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where watermarked PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the document to obtain the page count
                Document doc = new Document(inputPath);
                int pageCount = doc.Pages.Count; // total number of pages

                // Determine opacity based on page count (0.05 per page, max 0.5)
                double opacity = Math.Min(0.5, pageCount * 0.05);

                // Configure the text stamp that will act as the watermark
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    Opacity = (float)opacity,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                stamp.TextState.FontSize = 72;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8f, 0.8f, 0.8f);

                // Apply the stamp to every page
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the watermarked document
                doc.Save(outputPath);

                Console.WriteLine($"Watermarked '{fileName}' (pages: {pageCount}, opacity: {opacity:F2}) -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
