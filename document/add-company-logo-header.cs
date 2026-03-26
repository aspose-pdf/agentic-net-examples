using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string logoImagePath = "logo.png";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        try
        {
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Create an ImageStamp for the logo
                ImageStamp logoStamp = new ImageStamp(logoImagePath);
                // Position the stamp at the top center of the page (header area)
                logoStamp.Background = false;
                logoStamp.Opacity = 1.0f;
                logoStamp.HorizontalAlignment = HorizontalAlignment.Center;
                logoStamp.VerticalAlignment = VerticalAlignment.Top;
                // Optional offsets from the page edges (in points)
                logoStamp.XIndent = 0;
                logoStamp.YIndent = 20; // 20 points below the top edge

                // Apply the stamp to every page
                foreach (Page page in pdfDocument.Pages)
                {
                    page.AddStamp(logoStamp);
                }

                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"Header with logo added successfully. Saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}