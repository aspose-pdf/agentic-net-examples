using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class BatchPdfProcessor
{
    static void Main()
    {
        // Input directory containing PDFs to process
        const string inputDir = @"C:\PdfBatch\Input";
        // Output directory for edited PDFs
        const string outputDir = @"C:\PdfBatch\Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Gather all PDF files from the input directory
        List<string> pdfFiles = new List<string>(Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly));

        // Process each PDF in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Determine output file path
                string outputPath = Path.Combine(outputDir,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_edited.pdf");

                // Load the PDF using the high‑level Document API (thread‑safe per instance)
                using (Document doc = new Document(pdfPath))
                {
                    // Create a text stamp to indicate processing
                    TextStamp stamp = new TextStamp("Processed");
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Center;
                    stamp.TextState.FontSize = 36;
                    stamp.TextState.FontStyle = FontStyles.Bold;
                    stamp.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // Red

                    // Add the stamp to the first page (pages are 1‑based)
                    doc.Pages[1].AddStamp(stamp);

                    // Save the edited PDF
                    doc.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                // Log any errors for this file
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch processing completed.");
    }
}
