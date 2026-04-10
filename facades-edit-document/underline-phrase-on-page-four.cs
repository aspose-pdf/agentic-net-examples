using System;
using System.IO;
using System.Drawing; // System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf; // Document, Page, Aspose.Pdf.Rectangle, Aspose.Pdf.Color
using Aspose.Pdf.Text; // TextFragmentAbsorber, TextFragment, TextSearchOptions
using Aspose.Pdf.Facades; // PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string phrase = "specific phrase"; // phrase to underline

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 4 pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("Document has fewer than 4 pages.");
                doc.Save(outputPath);
                return;
            }

            // Extract text fragments from page 4 using TextFragmentAbsorber
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.TextSearchOptions = new TextSearchOptions(true); // case‑insensitive
            doc.Pages[4].Accept(absorber);

            // Locate the fragment that contains the target phrase
            TextFragment targetFragment = null;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.Text != null && fragment.Text.Contains(phrase))
                {
                    targetFragment = fragment;
                    break;
                }
            }

            if (targetFragment == null)
            {
                Console.WriteLine("Phrase not found on page 4.");
                // Save unchanged document
                doc.Save(outputPath);
                return;
            }

            // Get the rectangle of the found fragment (Aspose.Pdf.Rectangle)
            Aspose.Pdf.Rectangle aspRect = targetFragment.Rectangle;

            // Convert to System.Drawing.Rectangle required by PdfContentEditor.CreateMarkup
            System.Drawing.Rectangle markupRect = new System.Drawing.Rectangle(
                (int)aspRect.LLX,
                (int)aspRect.LLY,
                (int)(aspRect.URX - aspRect.LLX),
                (int)(aspRect.URY - aspRect.LLY));

            // Create the markup (underline) using the Facades API
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc); // bind the in‑memory document
                // markupType = 1 (Underline), page = 4, color = Red (System.Drawing.Color)
                editor.CreateMarkup(markupRect, string.Empty, 1, 4, System.Drawing.Color.Red);
                editor.Save(outputPath); // persist changes
            }
        }

        Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
    }
}
