using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs and the logo image
        string inputDir = "input";
        string outputDir = "output";
        string logoPath = "logo.png";

        // Ensure folders exist
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        // Verify the logo file is present
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Gather PDF files before changing the working directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");

        // Switch to the output folder so that Save uses a simple filename
        Directory.SetCurrentDirectory(outputDir);

        // Prepare a reusable stamp for the logo
        ImageStamp logoStamp = new ImageStamp(logoPath);
        logoStamp.Background = false;
        logoStamp.Opacity = 0.5f;
        logoStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;
        logoStamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Top;

        foreach (string pdfFilePath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfFilePath);
            using (Document doc = new Document(pdfFilePath))
            {
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(logoStamp);
                }
                // Save with only the filename (no directory part)
                doc.Save(fileName);
                Console.WriteLine($"Processed: {fileName}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
