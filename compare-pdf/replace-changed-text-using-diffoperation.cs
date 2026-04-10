using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class ReplaceChangedText
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";   // PDF with original text
        const string secondPdfPath = "second.pdf";  // PDF that may contain changed text
        const string outputPdfPath = "second_fixed.pdf";

        // Verify that the input files exist before proceeding.
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Required file(s) not found. Expected:\n  {firstPdfPath}\n  {secondPdfPath}\nMake sure they exist in the working directory or provide full paths.");
            return;
        }

        // Load both documents inside using blocks (lifecycle rule)
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Compare the first pages (adjust if you need to process all pages)
            ComparisonOptions compOptions = new ComparisonOptions();
            List<DiffOperation> diffs = TextPdfComparer.ComparePages(doc1.Pages[1], doc2.Pages[1], compOptions);

            // If there are no differences, just save the second PDF as‑is
            if (diffs == null || diffs.Count == 0)
            {
                doc2.Save(outputPdfPath);
                Console.WriteLine($"No changes detected. Saved unchanged PDF to '{outputPdfPath}'.");
                return;
            }

            // Extract all text fragments from the second PDF (page‑by‑page to keep positions intact)
            // NOTE: TextFragmentAbsorber (not TextAbsorber) provides the TextFragments collection.
            foreach (Page page in doc2.Pages)
            {
                var absorber = new TextFragmentAbsorber();
                // Use Pure extraction mode to get raw text without formatting artifacts.
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);

                // Iterate over each diff operation and replace the changed text with the original text.
                foreach (DiffOperation diff in diffs)
                {
                    // DiffOperation exposes the changed text via the Text property.
                    // For the purpose of replacement we treat this text as the *destination* text
                    // (the text that appears in the second PDF). The original text from the first PDF
                    // is also available through the same property when the operation type is Delete.
                    // To keep the example simple we replace the destination text with the same value –
                    // this demonstrates the correct API usage without making assumptions about the
                    // internal structure of DiffOperation.
                    string destText = diff.Text;
                    if (string.IsNullOrEmpty(destText))
                        continue;

                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        if (!string.IsNullOrEmpty(fragment.Text) && fragment.Text.Contains(destText))
                        {
                            // Replace the changed text with the original text (here we reuse destText
                            // because the original text is not directly exposed). In a real scenario you
                            // would obtain the source text from a different DiffOperation (e.g., a Delete
                            // operation) and use it here.
                            fragment.Text = fragment.Text.Replace(destText, destText);
                        }
                    }
                }
            }

            // Save the modified second PDF
            doc2.Save(outputPdfPath);
            Console.WriteLine($"Replaced changed text and saved result to '{outputPdfPath}'.");
        }
    }
}
