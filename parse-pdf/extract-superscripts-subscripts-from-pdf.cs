using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Use Flatten mode to obtain positioning information for each text segment
            TextExtractionOptions extractionOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Flatten);

            // Create a TextFragmentAbsorber and assign the extraction options
            TextFragmentAbsorber absorber = new TextFragmentAbsorber
            {
                ExtractionOptions = extractionOpts
            };

            // Extract text from all pages (rule: use TextFragmentAbsorber, not Page.ExtractText)
            doc.Pages.Accept(absorber);

            // Compute average baseline Y coordinate and average font size to use as reference
            double totalBaseline = 0;
            double totalFontSize = 0;
            int segmentCount = 0;

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                foreach (TextSegment segment in fragment.Segments)
                {
                    // Use Rectangle.LLY for baseline Y (Position.Y does not exist)
                    totalBaseline += segment.Rectangle.LLY;
                    totalFontSize += segment.TextState.FontSize;
                    segmentCount++;
                }
            }

            double avgBaseline = segmentCount > 0 ? totalBaseline / segmentCount : 0;
            double avgFontSize = segmentCount > 0 ? totalFontSize / segmentCount : 0;

            // Write extracted text to a plain text file, marking superscripts with '^' and subscripts with '_'
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    foreach (TextSegment segment in fragment.Segments)
                    {
                        string text = segment.Text;
                        double fontSize = segment.TextState.FontSize;
                        double baseline = segment.Rectangle.LLY; // baseline Y

                        // Heuristic: smaller font size + higher baseline => superscript
                        //            smaller font size + lower baseline => subscript
                        bool isSuperscript = fontSize < avgFontSize * 0.8 && baseline > avgBaseline;
                        bool isSubscript   = fontSize < avgFontSize * 0.8 && baseline < avgBaseline;

                        if (isSuperscript)
                            writer.Write($"^{text}");
                        else if (isSubscript)
                            writer.Write($"_{text}");
                        else
                            writer.Write(text);
                    }
                    writer.WriteLine(); // separate fragments (roughly corresponds to lines)
                }
            }
        }

        Console.WriteLine($"Extraction completed. Output saved to '{outputPath}'.");
    }
}
