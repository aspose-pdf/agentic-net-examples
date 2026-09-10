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
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a header/footer container
                HeaderFooter header = new HeaderFooter();

                // Create a text fragment for the header (bold, uppercase)
                TextFragment tf = new TextFragment("Section Heading".ToUpper());

                // Modify the existing TextState (do NOT assign a new TextState object)
                tf.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                tf.TextState.FontStyle = FontStyles.Bold; // optional, redundant with bold font

                // Add the text fragment to the header
                header.Paragraphs.Add(tf);

                // Assign the header to the page
                page.Header = header;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with headers to '{outputPath}'.");
    }
}
