using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfComparer
{
    static void Main()
    {
        const string pdfPath1 = "original.pdf";
        const string pdfPath2 = "modified.pdf";
        const string diffPath = "diff.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Load the two PDFs to be compared
        using (Aspose.Pdf.Document doc1 = new Aspose.Pdf.Document(pdfPath1))
        using (Aspose.Pdf.Document doc2 = new Aspose.Pdf.Document(pdfPath2))
        // Create a new PDF that will contain the comparison results
        using (Aspose.Pdf.Document diffDoc = new Aspose.Pdf.Document())
        {
            // ------------------------------------------------------------
            // Preserve original metadata (copy from the first document)
            // ------------------------------------------------------------
            diffDoc.Info.Title = doc1.Info.Title;
            diffDoc.Info.Author = doc1.Info.Author;
            diffDoc.Info.Subject = doc1.Info.Subject;
            diffDoc.Info.Keywords = doc1.Info.Keywords;
            diffDoc.Info.Creator = doc1.Info.Creator;
            diffDoc.Info.Producer = doc1.Info.Producer;
            diffDoc.Info.CreationDate = doc1.Info.CreationDate;
            // Use the correct property name for modification date
            diffDoc.Info.ModDate = doc1.Info.ModDate;

            // ------------------------------------------------------------
            // Compare page counts
            // ------------------------------------------------------------
            int count1 = doc1.Pages.Count;
            int count2 = doc2.Pages.Count;

            if (count1 != count2)
            {
                Aspose.Pdf.Page summaryPage = diffDoc.Pages.Add();
                Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(
                    $"Page count differs: Original={count1}, Modified={count2}");
                tf.Position = new Aspose.Pdf.Text.Position(50, 750);
                tf.TextState.FontSize = 14;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                summaryPage.Paragraphs.Add(tf);
            }

            // ------------------------------------------------------------
            // Compare each page's textual content
            // ------------------------------------------------------------
            int maxPages = Math.Max(count1, count2);
            for (int i = 1; i <= maxPages; i++)
            {
                bool hasPage1 = i <= count1;
                bool hasPage2 = i <= count2;

                if (!hasPage1 || !hasPage2)
                {
                    // One document lacks this page – already noted in page‑count diff
                    continue;
                }

                // Extract text from page i of both documents
                Aspose.Pdf.Text.TextAbsorber absorber1 = new Aspose.Pdf.Text.TextAbsorber();
                doc1.Pages[i].Accept(absorber1);
                string text1 = absorber1.Text ?? string.Empty;

                Aspose.Pdf.Text.TextAbsorber absorber2 = new Aspose.Pdf.Text.TextAbsorber();
                doc2.Pages[i].Accept(absorber2);
                string text2 = absorber2.Text ?? string.Empty;

                if (!string.Equals(text1, text2, StringComparison.Ordinal))
                {
                    // Text differs – create a diff page describing the change
                    Aspose.Pdf.Page diffPage = diffDoc.Pages.Add();

                    // Header
                    Aspose.Pdf.Text.TextFragment header = new Aspose.Pdf.Text.TextFragment(
                        $"Differences found on page {i}");
                    header.Position = new Aspose.Pdf.Text.Position(50, 770);
                    header.TextState.FontSize = 16;
                    header.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                    diffPage.Paragraphs.Add(header);

                    // Original text
                    Aspose.Pdf.Text.TextFragment original = new Aspose.Pdf.Text.TextFragment("Original:");
                    original.Position = new Aspose.Pdf.Text.Position(50, 730);
                    original.TextState.FontSize = 12;
                    original.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                    diffPage.Paragraphs.Add(original);

                    Aspose.Pdf.Text.TextFragment origContent = new Aspose.Pdf.Text.TextFragment(text1);
                    origContent.Position = new Aspose.Pdf.Text.Position(50, 710);
                    origContent.TextState.FontSize = 10;
                    origContent.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                    diffPage.Paragraphs.Add(origContent);

                    // Modified text
                    Aspose.Pdf.Text.TextFragment modified = new Aspose.Pdf.Text.TextFragment("Modified:");
                    modified.Position = new Aspose.Pdf.Text.Position(50, 650);
                    modified.TextState.FontSize = 12;
                    modified.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                    diffPage.Paragraphs.Add(modified);

                    Aspose.Pdf.Text.TextFragment modContent = new Aspose.Pdf.Text.TextFragment(text2);
                    modContent.Position = new Aspose.Pdf.Text.Position(50, 630);
                    modContent.TextState.FontSize = 10;
                    modContent.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                    diffPage.Paragraphs.Add(modContent);
                }
            }

            // Save the diff PDF
            diffDoc.Save(diffPath);
            Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPath}'.");
        }
    }
}
