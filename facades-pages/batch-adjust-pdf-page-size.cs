using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_sized.pdf");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Example logic: assign A4 size to odd pages, Letter size to even pages
                    // Sizes are in points (1 point = 1/72 inch)
                    const double a4Width = 595;   // 8.27 inches
                    const double a4Height = 842;  // 11.69 inches
                    const double letterWidth = 612; // 8.5 inches
                    const double letterHeight = 792; // 11 inches

                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        var pageInfo = doc.Pages[pageNum].PageInfo;
                        if (pageNum % 2 == 1) // odd page -> A4
                        {
                            pageInfo.Width = a4Width;
                            pageInfo.Height = a4Height;
                        }
                        else // even page -> Letter
                        {
                            pageInfo.Width = letterWidth;
                            pageInfo.Height = letterHeight;
                        }
                    }

                    // Save the modified PDF to the output path
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
