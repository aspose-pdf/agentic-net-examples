using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be analyzed
        const string pdfPath = "input.pdf";

        // Desired font type: "TTF" for TrueType or "OTF" for OpenType
        const string desiredFontType = "TTF";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Create a FontAbsorber to collect all fonts used in the document
            FontAbsorber fontAbsorber = new FontAbsorber();

            // Perform the search over the whole document
            fontAbsorber.Visit(doc);

            // Iterate through the collected fonts
            foreach (Font font in fontAbsorber.Fonts)
            {
                // FontName may contain the original file name (e.g., "Arial.ttf")
                string fontName = font.FontName ?? string.Empty;

                bool matchesDesiredType = false;

                // Simple heuristic: check the file extension in the font name
                if (desiredFontType.Equals("TTF", StringComparison.OrdinalIgnoreCase) &&
                    fontName.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase))
                {
                    matchesDesiredType = true;
                }
                else if (desiredFontType.Equals("OTF", StringComparison.OrdinalIgnoreCase) &&
                         fontName.EndsWith(".otf", StringComparison.OrdinalIgnoreCase))
                {
                    matchesDesiredType = true;
                }

                if (matchesDesiredType)
                {
                    Console.WriteLine($"Font: {fontName}");
                    Console.WriteLine($"  Embedded: {font.IsEmbedded}");
                    Console.WriteLine($"  Subset:   {font.IsSubset}");
                    Console.WriteLine($"  Accessible: {font.IsAccessible}");
                    Console.WriteLine();
                }
            }
        }
    }
}