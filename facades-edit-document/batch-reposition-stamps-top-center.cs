using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchStampReposition
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(inputPath) + "_repositioned.pdf");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Facade for editing existing stamps
                    using (PdfContentEditor editor = new PdfContentEditor())
                    {
                        editor.BindPdf(doc);

                        // Iterate through all pages (1‑based indexing)
                        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                        {
                            // Retrieve all stamps on the current page
                            StampInfo[] stamps = editor.GetStamps(pageNum);
                            if (stamps == null || stamps.Length == 0)
                                continue; // No stamps on this page

                            // Page dimensions
                            double pageWidth  = doc.Pages[pageNum].PageInfo.Width;
                            double pageHeight = doc.Pages[pageNum].PageInfo.Height;

                            // Desired vertical position: a small margin (20 units) below the top edge
                            double newY = pageHeight - 20;

                            // Desired horizontal position: centre of the page
                            double newX = pageWidth / 2;

                            // Move each stamp to the new coordinates
                            // StampInfo does not expose an index, so we use the loop counter (1‑based)
                            for (int idx = 0; idx < stamps.Length; idx++)
                            {
                                // MoveStamp expects 1‑based stamp index
                                editor.MoveStamp(pageNum, idx + 1, newX, newY);
                            }
                        }

                        // Save the modified document
                        doc.Save(outputPath);
                    }
                }

                Console.WriteLine($"Repositioned stamps saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}