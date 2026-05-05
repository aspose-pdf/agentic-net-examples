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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a footer that will be applied to every page
            HeaderFooter footer = new HeaderFooter();

            // TextFragment with placeholders:
            // $p – current page number, $P – total page count
            TextFragment tf = new TextFragment("Page $p of $P");
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            tf.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the fragment to the footer
            footer.Paragraphs.Add(tf);

            // Assign the same footer to all pages
            foreach (Page page in doc.Pages)
            {
                page.Footer = footer;
            }

            // Update pagination placeholders in all footers
            doc.Pages.UpdatePagination();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer: '{outputPath}'");
    }
}