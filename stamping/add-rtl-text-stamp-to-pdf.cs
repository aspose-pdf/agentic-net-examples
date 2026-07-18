using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_rtl_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set the document language to Arabic (right‑to‑left)
            doc.TaggedContent.SetLanguage("ar");

            // Create a TextStamp with Arabic text
            TextStamp stamp = new TextStamp("مثال نص عربي")
            {
                // Position the stamp at the bottom‑right corner
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,

                // Align the text inside the stamp to the right (RTL)
                TextAlignment = HorizontalAlignment.Right,

                // Optional margins
                RightMargin = 20,
                BottomMargin = 20,

                // Ensure the stamp is drawn as text (not as graphic operators)
                Draw = false
            };

            // Define the visual appearance of the stamp text
            // TextState is read‑only, but its properties can be modified
            stamp.TextState.Font = FontRepository.FindFont("Arial Unicode MS");
            stamp.TextState.FontSize = 14;
            stamp.TextState.ForegroundColor = Color.Black;

            // Apply the stamp to every page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with RTL text stamp to '{outputPath}'.");
    }
}
