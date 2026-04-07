using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
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
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("ar");

            // Arabic text – wrapped with Unicode Right‑to‑Left Embedding marks
            string arabicText = "\u202Bمثال على طباعة عربية\u202C"; // "Example Arabic stamp"

            // Create a TextStamp with Arabic text
            TextStamp stamp = new TextStamp(arabicText);

            // Configure the text appearance (no IsRightToLeft property – direction is handled by Unicode marks)
            stamp.TextState.Font = FontRepository.FindFont("Arial"); // Font that supports Arabic
            stamp.TextState.FontSize = 14;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the stamp (centered at the bottom of each page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;
            stamp.BottomMargin        = 20; // distance from bottom edge

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with RTL text stamp saved to '{outputPath}'.");
    }
}
