using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Extract formatted text using TextAbsorber (Pure mode)
            // ------------------------------------------------------------
            TextAbsorber textAbsorber = new TextAbsorber();
            textAbsorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(textAbsorber); // accept absorber for all pages
            string formattedText = textAbsorber.Text;

            Console.WriteLine("=== Formatted Text (Pure mode) ===");
            Console.WriteLine(formattedText);

            // ------------------------------------------------------------
            // 2. Extract detailed text fragments (position, font, size, color)
            // ------------------------------------------------------------
            TextFragmentAbsorber fragmentAbsorber = new TextFragmentAbsorber();
            fragmentAbsorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(fragmentAbsorber); // accept absorber for all pages

            Console.WriteLine("\n=== Text Fragments with Position and Style ===");
            int fragmentIndex = 1;
            foreach (TextFragment fragment in fragmentAbsorber.TextFragments)
            {
                // Position of the fragment (baseline coordinates)
                Aspose.Pdf.Text.Position pos = fragment.Position;

                // Font information
                Aspose.Pdf.Text.Font font = fragment.TextState.Font;
                double fontSize = fragment.TextState.FontSize;
                Aspose.Pdf.Color color = fragment.TextState.ForegroundColor;

                Console.WriteLine($"Fragment {fragmentIndex}:");
                Console.WriteLine($"  Text   : {fragment.Text}");
                Console.WriteLine($"  X      : {pos.XIndent}, Y : {pos.YIndent}");
                Console.WriteLine($"  Font   : {font?.FontName ?? "unknown"}");
                Console.WriteLine($"  Size   : {fontSize}");
                Console.WriteLine($"  Color  : {color}");
                fragmentIndex++;
            }

            // ------------------------------------------------------------
            // 3. Optional: Use PdfExtractor (Facades) to get raw text
            // ------------------------------------------------------------
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPath);
            extractor.ExtractTextMode = 0; // 0 = pure text mode
            extractor.ExtractText();

            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms))
                {
                    string rawText = reader.ReadToEnd();
                    Console.WriteLine("\n=== Raw Text via PdfExtractor ===");
                    Console.WriteLine(rawText);
                }
            }
        }
    }
}