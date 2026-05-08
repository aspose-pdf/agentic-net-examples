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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages and add a header to each page
            foreach (Page page in doc.Pages)
            {
                // Create a TextFragment that will act as the header
                TextFragment header = new TextFragment("SECTION HEADING")
                {
                    // Position the header near the top of the page (Y coordinate measured from bottom)
                    Position = new Position(0, page.PageInfo.Height - 20),
                    // Center the text horizontally
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Modify the existing TextState (the property is read‑only, but the returned object is mutable)
                header.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
                header.TextState.FontSize = 12;
                header.TextState.FontStyle = FontStyles.Bold;
                header.TextState.ForegroundColor = Color.Black;

                // Add the header fragment to the page's paragraph collection
                page.Paragraphs.Add(header);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}
