using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a footer that shows "Page X of Y" on each page
            foreach (Page page in doc.Pages)
            {
                // TextStamp supports the placeholders {page} and {pages}
                TextStamp stamp = new TextStamp("Page {page} of {pages}")
                {
                    BottomMargin = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                // Configure the appearance via the read‑only TextState object
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 12;
                stamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer: {outputPath}");
    }
}
