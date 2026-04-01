using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs and the logo image file
        string inputFolder = "input";
        string outputFolder = "output";
        string logoPath = "logo.png";

        // Ensure folders exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Verify logo file exists
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFilePath);

            // Load the PDF document
            using (Document pdfDocument = new Document(pdfFilePath))
            {
                // Create an ImageStamp for the logo
                ImageStamp logoStamp = new ImageStamp(logoPath);
                logoStamp.Background = false;
                logoStamp.HorizontalAlignment = HorizontalAlignment.Right;
                logoStamp.VerticalAlignment = VerticalAlignment.Top;
                logoStamp.XIndent = 10f; // margin from the right edge
                logoStamp.YIndent = 10f; // margin from the top edge
                logoStamp.Opacity = 1.0f;

                // Apply the stamp to every page
                foreach (Page page in pdfDocument.Pages)
                {
                    page.AddStamp(logoStamp);
                }

                // Change current directory to the output folder and save with a simple file name
                string originalDirectory = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(outputFolder);
                pdfDocument.Save(fileName);
                Directory.SetCurrentDirectory(originalDirectory);
            }

            Console.WriteLine($"Processed and saved: {fileName}");
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}