using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a header to every page
            foreach (Page page in doc.Pages)
            {
                // Create a TextFragment that will act as the header
                TextFragment header = new TextFragment("SECTION HEADING")
                {
                    // Position the header near the top centre of the page
                    Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height - 20)
                };

                // Apply bold, uppercase styling and centre alignment
                header.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
                header.TextState.FontSize = 12;
                header.TextState.FontStyle = FontStyles.Bold;
                header.TextState.HorizontalAlignment = HorizontalAlignment.Center;

                // Add the header to the page's paragraph collection
                page.Paragraphs.Add(header);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with header: '{outputPath}'");
    }
}
