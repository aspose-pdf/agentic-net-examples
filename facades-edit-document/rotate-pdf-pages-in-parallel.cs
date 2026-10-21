using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchPdfProcessor
{
    static void Main()
    {
        // Input directory containing PDFs to process
        const string inputDir = @"C:\PdfBatch\Input";
        // Output directory for edited PDFs
        const string outputDir = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Collect all PDF file paths
        List<string> pdfFiles = new List<string>(Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly));

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, inputPath =>
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDir, $"{fileName}_edited.pdf");

                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Create a text stamp to be added to every page
                    TextStamp stamp = new TextStamp("Confidential")
                    {
                        TextState =
                        {
                            FontSize = 12,
                            FontStyle = FontStyles.Bold,
                            // Use ForegroundColor instead of the non‑existent FontColor
                            ForegroundColor = Color.FromRgb(1, 0, 0)
                        },
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        BottomMargin = 10
                    };

                    // Apply the stamp to each page of the document
                    foreach (Page page in doc.Pages)
                    {
                        page.AddStamp(stamp);
                    }

                    // Save the modified document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch processing completed.");
    }
}
