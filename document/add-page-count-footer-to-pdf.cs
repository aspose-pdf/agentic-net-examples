using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count; // total page count

            // Iterate using 1‑based indexing (Aspose.Pdf pages are 1‑based)
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Create footer text "Page X of Y"
                string footerText = $"Page {i} of {totalPages}";
                TextFragment tf = new TextFragment(footerText);

                // Style the footer (optional)
                tf.TextState.FontSize = 9;
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Position the footer near the bottom of the page
                // y-coordinate is measured from the bottom; adjust as needed
                tf.Position = new Position(0, 20); // 20 points from bottom, centered horizontally

                // Center the text horizontally
                tf.HorizontalAlignment = HorizontalAlignment.Center;

                // Add the footer to the page's paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footers saved to '{outputPath}'.");
    }
}