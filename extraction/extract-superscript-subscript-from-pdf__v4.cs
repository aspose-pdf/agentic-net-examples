using System;
using System.IO;
using System.Text;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Get page 2 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[2];

            // Use TextFragmentAbsorber to obtain fragments with formatting information
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            page.Accept(absorber);

            // First, determine a reference font size (average of all fragments on the page)
            double totalSize = 0;
            int count = 0;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.TextState != null)
                {
                    totalSize += fragment.TextState.FontSize;
                    count++;
                }
            }
            double averageSize = count > 0 ? totalSize / count : 0;

            // Heuristic: treat fragments whose font size is noticeably smaller than the average as superscript/subscript
            const double sizeThresholdFactor = 0.8; // 20% smaller than average

            StringBuilder sb = new StringBuilder();
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.TextState != null && fragment.TextState.FontSize < averageSize * sizeThresholdFactor)
                {
                    sb.AppendLine(fragment.Text);
                }
            }

            // Write the collected text to a .txt file
            File.WriteAllText(outputPath, sb.ToString());

            Console.WriteLine($"Extraction complete. Output saved to '{outputPath}'.");
        }
    }
}
