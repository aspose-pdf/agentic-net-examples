using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages and add a header
            foreach (Page page in doc.Pages)
            {
                // Create a HeaderFooter object for the page header
                HeaderFooter header = new HeaderFooter();

                // Create a TextFragment with bold, uppercase text
                TextFragment tf = new TextFragment("SECTION HEADING");
                // Use a bold font (Helvetica-Bold) and set size/color
                tf.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
                tf.TextState.FontSize = 12;
                tf.TextState.FontStyle = FontStyles.Bold; // redundant when using a bold font, but safe
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the text fragment to the header
                header.Paragraphs.Add(tf);

                // Assign the header to the current page
                page.Header = header;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with header: '{outputPath}'");
    }
}