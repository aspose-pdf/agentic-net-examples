using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchStampReposition
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // First, read page dimensions using Document (read‑only)
                double[] pageWidths;
                double[] pageHeights;
                int pageCount;

                using (Document doc = new Document(inputPath))
                {
                    pageCount = doc.Pages.Count;
                    pageWidths = new double[pageCount + 1];   // 1‑based indexing
                    pageHeights = new double[pageCount + 1];

                    for (int i = 1; i <= pageCount; i++)
                    {
                        pageWidths[i] = doc.Pages[i].PageInfo.Width;
                        pageHeights[i] = doc.Pages[i].PageInfo.Height;
                    }
                }

                // Now reposition existing stamps using PdfContentEditor
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    editor.BindPdf(inputPath);

                    for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                    {
                        // Retrieve all stamps on the current page
                        StampInfo[] stamps = editor.GetStamps(pageNum);
                        if (stamps == null) continue;

                        // Move each stamp to the top‑center of the page
                        for (int idx = 0; idx < stamps.Length; idx++)
                        {
                            // StampInfo does not expose size; we place the stamp
                            // at horizontal centre (pageWidth / 2) and a small offset
                            // from the top edge (pageHeight - 10 points).
                            double newX = pageWidths[pageNum] / 2.0;
                            double newY = pageHeights[pageNum] - 10.0; // 10 points margin from top

                            // MoveStamp expects 1‑based stamp index
                            int stampIndex = idx + 1;
                            editor.MoveStamp(pageNum, stampIndex, newX, newY);
                        }
                    }

                    // Save the modified PDF to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(inputPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch stamp repositioning completed.");
    }
}