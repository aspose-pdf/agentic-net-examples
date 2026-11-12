using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Create a footer TextStamp with page‑number placeholders
        TextStamp footer = new TextStamp("Page {page_number} of {page_count}")
        {
            BottomMargin = 10,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            TextState = {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 12,
                ForegroundColor = Color.Black
            }
        };

        // Apply the stamp to every page in the document
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(footer);
        }

        // Save the stamped PDF
        pdfDoc.Save(outputPath);
        Console.WriteLine($"Footer stamp added: {outputPath}");
    }
}
