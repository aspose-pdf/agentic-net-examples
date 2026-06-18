using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // for ImageStamp
using Aspose.Pdf.Annotations; // optional, not needed here

class AddHeaderLogo
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string logoImage = "company_logo.png";
        const string outputPdf = "output_with_header.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Load the existing PDF (lifecycle: using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp that will serve as the header logo
            ImageStamp logoStamp = new ImageStamp(logoImage)
            {
                // Position the stamp at the top center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top,
                // Adjust margins as needed (e.g., 20 points from the top edge)
                TopMargin = 20,
                // Ensure the stamp is drawn over the page content (not as background)
                Background = false,
                // Optional: set opacity if a semi‑transparent logo is desired
                Opacity = 1.0f,
                // Scale the logo to fit within a reasonable width while preserving aspect ratio
                // Width and Height can be set directly or left to auto‑scale; here we limit width
                Width = 150   // points; adjust to your logo size
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF (lifecycle: save within the using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header logo added and saved to '{outputPdf}'.");
    }
}