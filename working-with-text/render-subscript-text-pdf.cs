using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string outputPath = "subscript_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Normal text fragment (e.g., "H")
            TextFragment normalFragment = new TextFragment("H");
            normalFragment.Position = new Position(100, 700); // place near top-left

            // Subscript fragment (e.g., "2")
            TextFragment subscriptFragment = new TextFragment("2");
            // Set subscript by applying a negative rise value.
            // The Subscript property internally sets a negative rise,
            // but we can also set the rise explicitly if needed.
            subscriptFragment.TextState.Subscript = true; // equivalent to negative rise
            // Position the subscript slightly lower and to the right of the normal text.
            // Here we offset the X coordinate and lower the Y coordinate.
            subscriptFragment.Position = new Position(115, 690);

            // Add both fragments to the page using TextBuilder (preserves order)
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(normalFragment);
            builder.AppendText(subscriptFragment);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with subscript text saved to '{outputPath}'.");
    }
}