using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string csvPath = "slide_titles.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Convert PDF to PPTX using explicit save options
        using (Document pdfDoc = new Document(pdfPath))
        {
            var pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(pptxPath, pptxOptions);
        }

        // Extract slide titles (first line of each page) and write to CSV
        using (Document pdfDoc = new Document(pdfPath))
        using (var writer = new StreamWriter(csvPath))
        {
            writer.WriteLine("SlideNumber,Title");
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                var absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                pdfDoc.Pages[i].Accept(absorber);
                string pageText = absorber.Text?.Trim() ?? string.Empty;
                string title = pageText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
                title = title.Replace("\"", "\"\""); // escape quotes for CSV
                writer.WriteLine($"{i},\"{title}\"");
            }
        }

        Console.WriteLine($"Conversion to PPTX saved as '{pptxPath}'. Slide titles exported to '{csvPath}'.");
    }
}