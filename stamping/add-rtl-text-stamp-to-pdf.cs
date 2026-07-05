using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // needed for FontRepository and TextState
using Aspose.Pdf.Annotations;   // optional if using annotation stamps
using Aspose.Pdf.Tagged;        // for setting document language

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

        // Load the PDF, set its natural language to Arabic (right‑to‑left)
        using (Document doc = new Document(inputPath))
        {
            // Set document language (optional but recommended for RTL PDFs)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("ar");   // "he" for Hebrew

            // Create a TextStamp with Arabic text
            // You can also use Hebrew text here.
            string stampText = "مثال على طباعة من اليمين إلى اليسار"; // Arabic sample
            TextStamp textStamp = new TextStamp(stampText);

            // Configure the visual appearance of the stamp
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            textStamp.BottomMargin        = 20;   // distance from bottom edge
            textStamp.Opacity             = 0.5;  // semi‑transparent

            // Set the text state (font, size, color). Use a font that supports Arabic/Hebrew.
            textStamp.TextState.Font = FontRepository.FindFont("Arial Unicode MS");
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // For right‑to‑left rendering, set the text alignment to Right.
            // Use the HorizontalAlignment enum (TextAlignment enum is obsolete).
            textStamp.TextAlignment = HorizontalAlignment.Right;

            // Add the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with RTL text stamp: '{outputPath}'");
    }
}
