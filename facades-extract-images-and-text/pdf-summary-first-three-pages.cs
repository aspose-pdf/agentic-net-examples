using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Extract text from the first three pages of the source PDF.
        StringBuilder extractedText = new StringBuilder();
        using (Document sourceDoc = new Document(inputPath))
        {
            int pagesToProcess = Math.Min(3, sourceDoc.Pages.Count);
            for (int pageIndex = 1; pageIndex <= pagesToProcess; pageIndex++)
            {
                TextAbsorber absorber = new TextAbsorber();
                sourceDoc.Pages[pageIndex].Accept(absorber);
                extractedText.AppendLine(absorber.Text);
            }
        }

        // Create a new PDF that contains the extracted text as a simple summary.
        using (Document summaryDoc = new Document())
        {
            Page summaryPage = summaryDoc.Pages.Add();
            TextFragment fragment = new TextFragment(extractedText.ToString());
            summaryPage.Paragraphs.Add(fragment);
            summaryDoc.Save(outputPath);
        }

        Console.WriteLine($"Summary PDF created: {outputPath}");
    }
}
