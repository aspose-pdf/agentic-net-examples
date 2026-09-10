using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bidi.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Arabic text (right‑to‑left). Unicode characters are used directly.
            string arabicText = "\u0645\u0631\u062D\u0628\u0627 \u0627\u0644\u0639\u0627\u0644\u0645"; // "مرحبا العالم"

            // Create a TextFragment containing the Arabic string
            Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment(arabicText);

            // Position the fragment on the page (baseline coordinates)
            textFragment.Position = new Aspose.Pdf.Text.Position(100, 520);

            // Configure the text appearance via the fragment's TextState
            // (TextState is read‑only; we modify its properties directly)
            textFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial Unicode MS");
            textFragment.TextState.FontSize = 14;
            textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(textFragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bidirectional text added and saved to '{outputPath}'.");
    }
}