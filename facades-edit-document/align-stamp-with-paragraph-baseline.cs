using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Locate the baseline of the first paragraph on page 5
            // -------------------------------------------------
            const int targetPageNumber = 5;
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[targetPageNumber].Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("No text found on page 5.");
                return;
            }

            // Use the first fragment as reference (index 0 in the collection)
            TextFragment fragment = absorber.TextFragments[0];
            double baselineY = fragment.Position.YIndent;   // baseline Y coordinate
            double baselineX = fragment.Position.XIndent;   // baseline X coordinate (left edge)

            // -------------------------------------------------
            // 2. Create a stamp (text) and position it on the baseline
            // -------------------------------------------------
            // Facade stamp – requires FormattedText for the visual content
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            // Use the overload that does NOT require a FontStyle argument
            FormattedText ft = new FormattedText(
                "Sample Stamp",
                System.Drawing.Color.Blue,               // fully‑qualified System.Drawing.Color
                "Helvetica",
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,
                12f);
            stamp.BindLogo(ft);
            // Set the origin to the baseline coordinates (cast to float as required)
            stamp.SetOrigin((float)baselineX, (float)baselineY);
            stamp.IsBackground = false; // place on top of page content
            stamp.Opacity = 0.8f;

            // -------------------------------------------------
            // 3. Apply the stamp to the document and save
            // -------------------------------------------------
            using (PdfFileStamp fileStamp = new PdfFileStamp(doc))
            {
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPath);
            }

            Console.WriteLine($"Stamp added and saved to '{outputPath}'.");
        }
    }
}
