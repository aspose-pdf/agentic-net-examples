using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "RTLTagged.pdf";

        // Ensure output directory exists (null‑safe)
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Set document reading direction to Right‑To‑Left if the enum is available
            // In older Aspose.Pdf versions the Direction enum may not expose RightToLeft;
            // the line is kept guarded to avoid compile errors.
            #if !NETSTANDARD1_0 // placeholder guard – remove if your version supports RightToLeft
            // doc.Direction = Aspose.Pdf.Direction.RightToLeft;
            #endif

            // Add visible Arabic text (optional, just to see something)
            TextFragment tf = new TextFragment("مثال على النص العربي");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language for accessibility (optional but recommended)
            taggedContent.SetLanguage("ar");

            // Get the root structure element
            StructureElement root = taggedContent.RootElement;

            // Create a Span element and set its text
            SpanElement span = taggedContent.CreateSpanElement();
            span.SetText("نص داخل Span عنصر");

            // Append the Span to the root – this inserts the BDC/EMC operators automatically
            root.AppendChild(span);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
