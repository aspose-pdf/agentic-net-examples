using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bidirectional_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Arabic phrase (right‑to‑left) followed by English (left‑to‑right)
            string rtlText = "\u0645\u0631\u062D\u0628\u0627"; // "مرحبا"
            string ltrText = " Hello";

            // Create a TextFragment containing both scripts
            TextFragment fragment = new TextFragment(rtlText + ltrText);

            // Configure the TextState for the fragment (TextState is read‑only, so we modify its members)
            fragment.TextState.Font = FontRepository.FindFont("Arial"); // Arial supports Arabic glyphs
            fragment.TextState.FontSize = 14;
            fragment.TextState.ForegroundColor = Color.Black;
            // Aspose.PDF automatically handles bidirectional rendering based on Unicode directionality.
            // If an explicit RTL flag is required in a specific version, use:
            // fragment.TextState.IsRightToLeft = true; // (uncomment if the property exists in your version)

            // Position the fragment on the first page
            Page page = doc.Pages[1];
            fragment.Position = new Position(100, 700);
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bidirectional PDF saved to '{outputPath}'.");
    }
}
