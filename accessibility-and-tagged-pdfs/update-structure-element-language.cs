using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string validationPath = "pdfua1.xml"; // PDF/UA validation file (optional)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Find the first paragraph element in the structure tree
            var paragraphs = root.FindElements<ParagraphElement>(true);
            if (paragraphs.Count > 0)
            {
                ParagraphElement target = paragraphs[0];
                // Update language to French (RFC 3066 tag)
                target.Language = "fr-FR";
                Console.WriteLine("Updated language of the first paragraph element to French.");
            }
            else
            {
                // If no paragraph exists, create one for demonstration
                ParagraphElement para = tagged.CreateParagraphElement();
                para.SetText("Sample paragraph added by example.");
                para.Language = "fr-FR";
                root.AppendChild(para);
                Console.WriteLine("No existing paragraph found; created a new one with French language.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");

            // Optional: re‑validate PDF/UA compliance if a validation XML is available
            if (File.Exists(validationPath))
            {
                bool isCompliant = doc.Validate(validationPath, PdfFormat.PDF_UA_1);
                Console.WriteLine($"PDF/UA compliance: {isCompliant}");
            }
            else
            {
                Console.WriteLine("Validation file not found; skipping compliance check.");
            }
        }
    }
}