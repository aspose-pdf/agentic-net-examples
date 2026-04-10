using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            // Add a footer to each page that will display "Page X of Y"
            foreach (Page page in doc.Pages)
            {
                // Create a new footer container
                HeaderFooter footer = new HeaderFooter();

                // Build the footer text fragment
                TextFragment tf = new TextFragment($"Page {page.Number} of {totalPages}")
                {
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                // Optional styling (font, size, color)
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 10;
                tf.TextState.ForegroundColor = Color.Black;

                // Add the fragment to the footer's paragraph collection
                footer.Paragraphs.Add(tf);

                // Attach the footer to the page
                page.Footer = footer;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer: {outputPath}");
    }
}
