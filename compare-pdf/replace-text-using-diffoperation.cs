using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "merged_output.pdf";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document docFirst = new Document(firstPdfPath))
        using (Document docSecond = new Document(secondPdfPath))
        {
            // Determine the number of pages to process (use the smaller count)
            int pageCount = Math.Min(docFirst.Pages.Count, docSecond.Pages.Count);

            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int i = 1; i <= pageCount; i++)
            {
                // Extract text from the corresponding page of the first PDF
                Aspose.Pdf.Text.TextAbsorber absorber = new Aspose.Pdf.Text.TextAbsorber();
                absorber.ExtractionOptions = new Aspose.Pdf.Text.TextExtractionOptions(
                    Aspose.Pdf.Text.TextExtractionOptions.TextFormattingMode.Pure);
                docFirst.Pages[i].Accept(absorber);
                string firstPageText = absorber.Text;

                // Clear existing content on the second PDF page
                docSecond.Pages[i].Paragraphs.Clear();

                // Insert the extracted text from the first PDF into the second PDF page
                if (!string.IsNullOrEmpty(firstPageText))
                {
                    Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment(firstPageText);
                    // Optional: set basic text state (font, size, color) if needed
                    // fragment.TextState.Font = FontRepository.FindFont("Arial");
                    // fragment.TextState.FontSize = 12;
                    docSecond.Pages[i].Paragraphs.Add(fragment);
                }
            }

            // Save the modified second PDF (now containing the text from the first PDF where differences existed)
            docSecond.Save(outputPdfPath);
        }

        Console.WriteLine($"Comparison complete. Result saved to '{outputPdfPath}'.");
    }
}