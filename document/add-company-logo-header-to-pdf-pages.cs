using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
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

        // Load the existing PDF inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through each page and add the logo as a stamp positioned in the header area
            foreach (Page page in doc.Pages)
            {
                // Create a fresh ImageStamp for each page (stamp objects are mutable after being added)
                ImageStamp logoStamp = new ImageStamp(logoImagePath)
                {
                    // Center the logo horizontally
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Align the logo to the top of the page (header region)
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Small top margin inside the header
                    TopMargin           = 5,
                    // Desired height (width scales to keep aspect ratio)
                    Height              = 50
                };

                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Custom header with logo added to every page: {outputPdfPath}");
    }
}
