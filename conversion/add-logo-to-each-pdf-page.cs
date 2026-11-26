using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "logo.gif";
        const string outputPath = "logoEachPage.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure it starts empty
        using (Document doc = new Document())
        {
            doc.Pages.Clear(); // remove the default page if present

            // Add four blank pages
            for (int i = 0; i < 4; i++)
            {
                doc.Pages.Add();
            }

            // Prepare an ImageStamp for the logo
            ImageStamp logoStamp = new ImageStamp(imagePath)
            {
                // Center the image on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Keep original size; set other properties as needed
                Background = false,
                Opacity    = 1.0
            };

            // Place the logo on every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with logo on each page: {outputPath}");
    }
}