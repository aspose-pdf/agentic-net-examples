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
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable automatic tagging for untagged PDFs
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a TaggedContent object if the PDF was untagged)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // -----------------------------------------------------------------
            // 1. Tag all image resources with alternative text
            // -----------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Provide a generic description; in real scenarios use a meaningful text
                    img.TrySetAlternativeText("Image description", page);
                }
            }

            // -----------------------------------------------------------------
            // 2. Ensure logical structure elements for images have AlternativeText
            // -----------------------------------------------------------------
            StructureElement root = taggedContent.RootElement; // no cast needed

            var figureElements = root.FindElements<FigureElement>(true);
            foreach (FigureElement fig in figureElements)
            {
                if (string.IsNullOrWhiteSpace(fig.AlternativeText))
                {
                    fig.AlternativeText = "Image description";
                }
            }

            // -----------------------------------------------------------------
            // 3. Tag all textual structure elements (Paragraph, Header, Span)
            // -----------------------------------------------------------------
            var paragraphElements = root.FindElements<ParagraphElement>(true);
            foreach (ParagraphElement para in paragraphElements)
            {
                // Example: set language on each paragraph
                para.Language = "en-US";
                // If the paragraph has no text, you could set a placeholder
                if (string.IsNullOrWhiteSpace(para.ActualText))
                {
                    para.SetText("Placeholder text");
                }
            }

            var headerElements = root.FindElements<HeaderElement>(true);
            foreach (HeaderElement hdr in headerElements)
            {
                hdr.Language = "en-US";
                if (string.IsNullOrWhiteSpace(hdr.ActualText))
                {
                    hdr.SetText("Header placeholder");
                }
            }

            var spanElements = root.FindElements<SpanElement>(true);
            foreach (SpanElement span in spanElements)
            {
                span.Language = "en-US";
                // Span elements usually inherit text from their parent; no SetText needed
            }

            // -----------------------------------------------------------------
            // Save the modified PDF (no PreSave required)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}