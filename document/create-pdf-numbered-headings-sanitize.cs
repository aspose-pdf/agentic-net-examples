using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string sanitizedPath = "sanitized.pdf";

        // -------------------------------------------------
        // 1. Create a PDF with headings (tagged PDF)
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page – required for visual content
            Page page = doc.Pages.Add();

            // Enable tagging and set language/title
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Headings");

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Helper to add a heading both to the page (visual) and to the structure tree
            void AddHeading(string text, int level, int number)
            {
                // Visual representation
                TextFragment tf = new TextFragment($"{number}. {text}");
                tf.TextState.FontSize = 14 + (4 - level) * 2; // larger font for higher level
                tf.TextState.Font = FontRepository.FindFont("Arial");
                page.Paragraphs.Add(tf);

                // Structure element
                HeaderElement header = tagged.CreateHeaderElement(level);
                header.SetText($"{number}. {text}");
                // Store the same text in AlternativeText so we can read it back later (some versions lack GetText())
                header.AlternativeText = $"{number}. {text}";
                root.AppendChild(header);
            }

            // Add sample headings
            AddHeading("Introduction", 1, 1);
            AddHeading("Background", 2, 1);
            AddHeading("Methodology", 1, 2);
            AddHeading("Results", 2, 1);
            AddHeading("Conclusion", 1, 3);

            // Save the original document
            doc.Save(originalPath);
        }

        // -------------------------------------------------
        // 2. Sanitize: keep only heading structure and numbering
        // -------------------------------------------------
        using (Document srcDoc = new Document(originalPath))
        {
            // Access tagged content
            ITaggedContent srcTagged = srcDoc.TaggedContent;

            // Find all header elements (any level) recursively
            StructureElement srcRoot = srcTagged.RootElement;
            var headers = srcRoot.FindElements<HeaderElement>(true);

            // Create a new PDF to hold only the headings
            using (Document cleanDoc = new Document())
            {
                Page cleanPage = cleanDoc.Pages.Add();

                ITaggedContent cleanTagged = cleanDoc.TaggedContent;
                cleanTagged.SetLanguage("en-US");
                cleanTagged.SetTitle("Sanitized Headings");

                StructureElement cleanRoot = cleanTagged.RootElement;

                // Preserve original numbering (already part of the text)
                foreach (HeaderElement hdr in headers)
                {
                    // Retrieve the heading text. Use AlternativeText as a fallback when GetText() is unavailable.
                    string headingText = hdr.AlternativeText ?? string.Empty;

                    // Determine heading level from the stored text when HeaderLevel property is unavailable.
                    int level = DetermineLevelFromText(headingText);

                    // Visual text (already includes numbering)
                    TextFragment tf = new TextFragment(headingText);
                    tf.TextState.FontSize = 14 + (4 - level) * 2;
                    tf.TextState.Font = FontRepository.FindFont("Arial");
                    cleanPage.Paragraphs.Add(tf);

                    // Clone the header element into the new structure tree
                    HeaderElement newHeader = cleanTagged.CreateHeaderElement(level);
                    newHeader.SetText(headingText);
                    // Keep the same AlternativeText for later retrieval if needed
                    newHeader.AlternativeText = headingText;
                    cleanRoot.AppendChild(newHeader);
                }

                // Save the sanitized PDF
                cleanDoc.Save(sanitizedPath);
            }
        }

        Console.WriteLine($"Original PDF saved to '{originalPath}'.");
        Console.WriteLine($"Sanitized PDF saved to '{sanitizedPath}'.");
    }

    /// <summary>
    /// Derives a heading level from the heading text when the HeaderLevel property is not available.
    /// The method counts the number of '.' characters in the numeric prefix (e.g., "1.2.3" => level 3).
    /// If no numeric prefix is found, it defaults to level 1.
    /// </summary>
    private static int DetermineLevelFromText(string headingText)
    {
        if (string.IsNullOrWhiteSpace(headingText))
            return 1;

        // Extract the part before the first space – assumed to be the numbering (e.g., "1.2.3")
        var firstSpace = headingText.IndexOf(' ');
        var numberPart = firstSpace > 0 ? headingText.Substring(0, firstSpace) : headingText;

        // Count '.' characters; level is count + 1 (e.g., "1" => level 1, "1.2" => level 2)
        int dotCount = 0;
        foreach (char c in numberPart)
        {
            if (c == '.') dotCount++;
        }
        return dotCount + 1;
    }
}
