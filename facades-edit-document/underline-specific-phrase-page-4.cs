using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_underline.pdf";
        const string phrase = "specific phrase"; // phrase to underline

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to locate the phrase on page 4
        using (Document doc = new Document(inputPath))
        {
            // Use TextFragmentAbsorber to get text fragments (not TextAbsorber)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.TextSearchOptions = new TextSearchOptions(true); // case‑insensitive
            doc.Pages[4].Accept(absorber);

            // Find the fragment that contains the target phrase
            TextFragment targetFragment = null;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.Text != null &&
                    fragment.Text.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    targetFragment = fragment;
                    break;
                }
            }

            if (targetFragment == null)
            {
                Console.Error.WriteLine($"Phrase \"{phrase}\" not found on page 4.");
                return;
            }

            // Get the rectangle of the fragment (Aspose.Pdf.Rectangle)
            Aspose.Pdf.Rectangle pdfRect = targetFragment.Rectangle;

            // Convert to System.Drawing.Rectangle for the facade method
            System.Drawing.Rectangle drawRect = new System.Drawing.Rectangle(
                (int)pdfRect.LLX,
                (int)pdfRect.LLY,
                (int)(pdfRect.URX - pdfRect.LLX),
                (int)(pdfRect.URY - pdfRect.LLY));

            // Create the underline annotation using PdfContentEditor (Facade)
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPath);
                // markupType = 1 (Underline), page = 4, color = System.Drawing.Color.Red
                editor.CreateMarkup(drawRect, "", 1, 4, System.Drawing.Color.Red);
                editor.Save(outputPath);
            }

            Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
        }
    }
}
