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
        const string headerText = "Confidential Document";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Loop through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a new header/footer container
                HeaderFooter header = new HeaderFooter();

                // Create a text fragment for the header
                TextFragment tf = new TextFragment(headerText);
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 12;
                tf.TextState.FontStyle = FontStyles.Bold;
                tf.TextState.ForegroundColor = Color.Black;
                tf.Position = new Position(0, page.PageInfo.Height - 20); // optional positioning

                // Add the fragment to the header's paragraph collection
                header.Paragraphs.Add(tf);

                // Assign the header to the page
                page.Header = header;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added to all pages. Saved as '{outputPath}'.");
    }
}
