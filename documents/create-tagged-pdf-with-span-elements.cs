using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.LogicalStructure; // for structure elements like SpanElement
using Aspose.Pdf.Tagged;           // for ITaggedContent

class Program
{
    static void Main()
    {
        const string outputPath = "ParagraphSpanned.pdf";

        // Create a new PDF document
        using (var doc = new Document())
        {
            // Add a new page (first page is index 1)
            Page page = doc.Pages.Add();

            // Add sample paragraphs to the page – use TextFragment (the concrete paragraph type)
            for (int i = 1; i <= 3; i++)
            {
                TextFragment tf = new TextFragment($"Paragraph {i}");
                page.Paragraphs.Add(tf);
            }

            // ----- Tagged‑PDF part -------------------------------------------------
            // Obtain the tagged‑content interface (no Document.Tagged property exists)
            ITaggedContent tagged = doc.TaggedContent;
            // Set basic document properties for a tagged PDF (optional but recommended)
            tagged.SetTitle("Paragraph Spanned Sample");
            tagged.SetLanguage("en-US");

            // Root element of the logical structure tree
            var root = tagged.RootElement;

            // Iterate over each paragraph (TextFragment) and create a Span element
            // that contains the same text. The Span is appended to the root element.
            foreach (BaseParagraph para in page.Paragraphs)
            {
                if (para is TextFragment tf)
                {
                    // Create a Span logical‑structure element
                    SpanElement span = tagged.CreateSpanElement();
                    // Set the visible text of the span – this also links the span to the
                    // visual content of the TextFragment.
                    span.SetText(tf.Text);
                    // Append the span to the root of the structure tree.
                    root.AppendChild(span);
                }
            }
            // ----------------------------------------------------------------------

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
