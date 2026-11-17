using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "form_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            // Load the PDF that contains form fields
            using (Document doc = new Document(inputPath))
            {
                // Create an image stamp; configure its placement as needed.
                // ImageStamp does not expose a Margin property – use the individual
                // margin properties (TopMargin, RightMargin, etc.) or XIndent/YIndent.
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Top,
                    TopMargin    = 10,   // distance from the top edge of the page
                    RightMargin  = 10    // distance from the right edge of the page
                };

                // Apply the stamp to every page. Adding a stamp does not flatten or
                // disable existing form fields.
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the PDF; form fields remain interactive.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
