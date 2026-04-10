using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the pages on which the stamp should appear
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        int[] selectedPages = new int[] { 1, 3, 5 };   // example: pages 1, 3 and 5

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source document (use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextStamp – it supports opacity directly
            foreach (int pageNumber in selectedPages)
            {
                // Guard against out‑of‑range page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    Opacity = 0.7f,               // 70% opacity
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Configure visual appearance of the stamp (TextState is read‑only, modify its members)
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 48;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red; // fully qualified to avoid ambiguity
                stamp.TextState.FontStyle = FontStyles.Bold;

                // Apply the stamp to the selected page
                doc.Pages[pageNumber].AddStamp(stamp);
            }

            // Save the result
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Transparent text stamp applied. Output saved to '{outputPdf}'.");
    }
}