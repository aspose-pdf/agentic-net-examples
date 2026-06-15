using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        const string outputPath = "justified.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a TextFragment with sample text
            TextFragment tf = new TextFragment(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            );

            // Set justification by configuring TextState.HorizontalAlignment to Justify
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Justify;

            // Optional: set font, size, and color (using Aspose.Pdf APIs)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Guard Document.Save on macOS where libgdiplus may be missing
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("libgdiplus is required for PDF creation on macOS. Skipping save.");
            }
            else
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to {outputPath}");
            }
        }
    }
}