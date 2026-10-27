using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a bold, uppercase header text fragment
                TextFragment header = new TextFragment("SECTION HEADING");
                header.TextState.Font = FontRepository.FindFont("Arial");
                header.TextState.FontSize = 14;
                header.TextState.FontStyle = FontStyles.Bold;
                header.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the header near the top of the page (20 points margin)
                double marginTop = 20;
                double pageHeight = page.PageInfo.Height;
                header.Position = new Position(0, pageHeight - marginTop);

                // Center the header horizontally
                header.HorizontalAlignment = HorizontalAlignment.Center;

                // Add the header to the page's paragraph collection
                page.Paragraphs.Add(header);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}