using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string originalPath  = "original.pdf";
        const string sanitizedPath = "sanitized.pdf";

        // -------------------------------------------------
        // Step 1: Create a PDF with headings and extra content
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page (required for tagged content)
            doc.Pages.Add();

            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Headings");

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Heading 1
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("1. Introduction");
            root.AppendChild(h1);

            // Paragraph under heading 1 (will be removed during sanitization)
            ParagraphElement p1 = tagged.CreateParagraphElement();
            p1.SetText("This paragraph contains introductory text that will be stripped out.");
            root.AppendChild(p1);

            // Heading 2 (subsection)
            HeaderElement h2 = tagged.CreateHeaderElement(2);
            h2.SetText("1.1 Overview");
            root.AppendChild(h2);

            // Another paragraph
            ParagraphElement p2 = tagged.CreateParagraphElement();
            p2.SetText("Details about the overview go here.");
            root.AppendChild(p2);

            // Heading 1 again
            HeaderElement h3 = tagged.CreateHeaderElement(1);
            h3.SetText("2. Conclusion");
            root.AppendChild(h3);

            // Save the original document
            doc.Save(originalPath);
        }

        // -------------------------------------------------
        // Step 2: Sanitize – keep only heading elements and their numbering
        // -------------------------------------------------
        using (Document srcDoc = new Document(originalPath))
        {
            // Retrieve all heading elements (recursive search)
            StructureElement srcRoot = srcDoc.TaggedContent.RootElement;
            var headings = srcRoot.FindElements<HeaderElement>(true);

            // Create a new empty PDF to hold the sanitized content
            using (Document sanitizedDoc = new Document())
            {
                // At least one page is required
                sanitizedDoc.Pages.Add();

                // Prepare tagged content for the new document
                ITaggedContent destTagged = sanitizedDoc.TaggedContent;
                destTagged.SetLanguage("en-US");
                destTagged.SetTitle("Sanitized Document");

                StructureElement destRoot = destTagged.RootElement;

                // Copy each heading preserving its level and text
                foreach (HeaderElement srcHeader in headings)
                {
                    // ---- Fixed: obtain heading level via reflection ----
                    int level = 1; // default level if reflection fails
                    var levelProp = srcHeader.GetType().GetProperty("Level");
                    if (levelProp != null && levelProp.PropertyType == typeof(int))
                    {
                        level = (int)levelProp.GetValue(srcHeader);
                    }

                    // Create a new header with the same level
                    HeaderElement newHeader = destTagged.CreateHeaderElement(level);
                    // Preserve the original heading text (including numbering)
                    newHeader.SetText(srcHeader.ActualText ?? string.Empty);
                    destRoot.AppendChild(newHeader);
                }

                // Save the sanitized PDF
                sanitizedDoc.Save(sanitizedPath);
            }
        }

        Console.WriteLine($"Original PDF saved to '{originalPath}'.");
        Console.WriteLine($"Sanitized PDF saved to '{sanitizedPath}'.");
    }
}
