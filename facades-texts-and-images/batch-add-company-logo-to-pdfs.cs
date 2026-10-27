using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be written
        const string outputFolder = "OutputPdfs";
        // Path to the company logo PNG
        const string logoPath = "logo.png";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFilePath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFilePath))
                {
                    // ImageStamp lives in the Aspose.Pdf namespace (not Aspose.Pdf.Drawing)
                    ImageStamp logoStamp = new ImageStamp(logoPath);

                    // Position the stamp at the top‑right corner of each page
                    logoStamp.HorizontalAlignment = HorizontalAlignment.Right;
                    logoStamp.VerticalAlignment   = VerticalAlignment.Top;

                    // Optional margins from the page edges (points)
                    logoStamp.TopMargin   = 10;
                    logoStamp.RightMargin = 10;

                    // Apply the stamp to every page in the document
                    foreach (Page page in doc.Pages)
                    {
                        page.AddStamp(logoStamp);
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}
