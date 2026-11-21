using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        string outputFolder = "OutputPdfs";
        // Path to the company logo PNG
        string logoPath = "logo.png";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Create an ImageStamp for the logo using the Facades API
                ImageStamp logoStamp = new ImageStamp(logoPath)
                {
                    Width = 100,          // Desired logo width (points)
                    Height = 50,          // Desired logo height (points)
                    Background = false,   // Place logo in front of page content
                    Opacity = 1.0         // Fully opaque
                };

                // Add the stamp to every page, positioning it at the top‑right corner
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Margin from the page edges
                    double margin = 10;

                    // Compute coordinates so the stamp sits near the top‑right
                    logoStamp.XIndent = page.PageInfo.Width - logoStamp.Width - margin;
                    logoStamp.YIndent = page.PageInfo.Height - logoStamp.Height - margin;

                    page.AddStamp(logoStamp);
                }

                // Save the modified PDF to the output folder
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}