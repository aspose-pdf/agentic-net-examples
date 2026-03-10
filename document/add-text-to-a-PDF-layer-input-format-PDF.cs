using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_text.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the text will be added (1‑based index)
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("Text added to PDF layer");
            // Position the text on the page (coordinates are in points)
            fragment.Position = new Position(100, 600);
            // Optional: set font, size and color
            fragment.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Blue;

            // Use TextBuilder to append the text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified document using PdfFileMend (a Facade class)
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the modified document to the facade
                mend.BindPdf(doc);
                // Save the result to a new file
                mend.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with added text: {outputPdf}");
    }
}