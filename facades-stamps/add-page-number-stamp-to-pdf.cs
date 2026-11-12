using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF using Document (preferred over PdfStampFacade which may be unavailable in newer versions)
        Document pdfDocument = new Document(inputPath);

        // Create a text stamp that uses the {page_number} placeholder
        TextStamp textStamp = new TextStamp("Page {page_number}");
        textStamp.TextState.Font = FontRepository.FindFont("Arial");
        textStamp.TextState.FontSize = 12;
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;
        textStamp.VerticalAlignment = VerticalAlignment.Bottom;
        textStamp.BottomMargin = 10; // distance from bottom edge

        // Apply the stamp to every page. The {page_number} placeholder will be replaced automatically.
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(textStamp);
        }

        // Save the resulting PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
