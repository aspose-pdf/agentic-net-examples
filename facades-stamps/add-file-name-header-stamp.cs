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
        Document pdfDocument = new Document(inputPath);

        // Create a TextStamp that uses the {FileName} placeholder.
        // Aspose.Pdf replaces {FileName} with the actual file name when the stamp is applied.
        TextStamp headerStamp = new TextStamp("{FileName}");
        headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        headerStamp.TextState.FontSize = 12;
        headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
        headerStamp.VerticalAlignment   = VerticalAlignment.Top;
        headerStamp.YIndent = 20f; // top margin in points

        // Apply the stamp to every page
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(headerStamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
