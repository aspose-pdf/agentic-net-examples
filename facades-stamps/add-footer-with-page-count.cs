using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a footer text stamp with placeholders for page number and total page count
            TextStamp footer = new TextStamp("Page {page_number} of {page_count}");
            footer.BottomMargin          = 10;                                   // distance from bottom edge
            footer.HorizontalAlignment   = HorizontalAlignment.Center;            // center horizontally
            footer.VerticalAlignment     = VerticalAlignment.Bottom;              // place at bottom
            footer.TextState.FontSize    = 12;
            footer.TextState.Font        = FontRepository.FindFont("Arial");
            footer.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;            // use Aspose.Pdf.Color to avoid System.Drawing

            // Apply the stamp to every page
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(footer);
            }

            // Save the stamped PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
