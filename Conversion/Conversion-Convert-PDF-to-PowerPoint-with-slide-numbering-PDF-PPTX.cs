using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Add slide numbers as footer text on each page before conversion
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Create a text fragment containing the slide number
                TextFragment tf = new TextFragment($"Slide {i}");

                // Position the text near the bottom center of the page
                // Adjust coordinates as needed for your layout
                tf.Position = new Position(page.PageInfo.Width / 2 - 30, 20);
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the text fragment to the page's paragraph collection
                page.Paragraphs.Add(tf);
            }

            // Convert the PDF to PPTX using explicit PptxSaveOptions
            PptxSaveOptions saveOpts = new PptxSaveOptions();
            pdfDoc.Save(outputPptx, saveOpts);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with slide numbers: {outputPptx}");
    }
}