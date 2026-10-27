using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Network share containing the PDFs
        const string networkFolder = @"\\server\share\pdfs";

        // Verify the folder exists
        if (!Directory.Exists(networkFolder))
        {
            Console.Error.WriteLine($"Folder not found: {networkFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(networkFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    // Create a text stamp that will be reused for every page
                    TextStamp stamp = new TextStamp("CONFIDENTIAL");
                    stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                    stamp.TextState.FontSize = 24;
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

                    // Rotate the stamp 45 degrees
                    stamp.RotateAngle = 45; // degrees

                    // Center the stamp on each page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Center;

                    // Apply the stamp to every page in the document
                    foreach (Page page in doc.Pages)
                    {
                        page.AddStamp(stamp);
                    }

                    // Build output file name (original name + "_stamped")
                    string directory = Path.GetDirectoryName(inputPath);
                    string fileName  = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(directory, $"{fileName}_stamped.pdf");

                    // Save the modified PDF
                    doc.Save(outputPath);
                    Console.WriteLine($"Stamped PDF saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}