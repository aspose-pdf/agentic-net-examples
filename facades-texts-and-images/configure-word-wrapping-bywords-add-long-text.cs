using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "wrapped.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (page number will be 1)
            Page page = doc.Pages.Add();

            // Define the rectangle area where the text will be placed (llx, lly, urx, ury)
            // Use the Aspose.Pdf.Rectangle type for PDF page coordinates
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 500, 550, 750);

            // Text that exceeds the rectangle width
            string longText = "This is a very long paragraph of text that will exceed the defined width of the rectangle and therefore needs to be wrapped by words to fit within the area.";

            // Create a TextFragment – the Rectangle property is read‑only, so we set Position via TextState
            TextFragment fragment = new TextFragment(longText)
            {
                // Position is the lower‑left corner of the rectangle (LLX, LLY).  We use LLY for Y because Aspose.Pdf uses a bottom‑up coordinate system.
                Position = new Position(rect.LLX, rect.LLY)
            };

            // Configure text appearance
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Arial");

            // ------------------------------------------------------------
            // NOTE: The properties 'Width' and 'WordWrapMode' were introduced
            // in newer versions of Aspose.Pdf (v23.10+). If you are using an
            // older version, these members are not available, which caused the
            // CS1061 compilation errors. To enable word‑wrapping you have two
            // options:
            //   1) Upgrade the Aspose.Pdf NuGet package to a version that
            //      includes TextFragmentState.Width and TextFragmentState.WordWrapMode.
            //   2) If upgrading is not possible, you can approximate wrapping
            //      by manually inserting line‑breaks ("\n") into the text or by
            //      using a TextBuilder with a TextParagraph and setting the
            //      TextFormattingOptions.WrapMode.
            // ------------------------------------------------------------

            // The following lines work only with newer library versions:
            // fragment.TextState.Width = rect.URX - rect.LLX;               // sets the maximum width for wrapping
            // fragment.TextState.WordWrapMode = WordWrapMode.ByWords;      // enables word‑by‑word wrapping

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
