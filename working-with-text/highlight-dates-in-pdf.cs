using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Regular‑expression pattern for dates in MM/DD/YYYY format
        string datePattern = @"\b\d{2}/\d{2}/\d{4}\b";

        // Use TextFragmentAbsorber with regex enabled to find matching fragments
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(datePattern, new TextSearchOptions(true));
        doc.Pages.Accept(absorber);
        TextFragmentCollection fragments = absorber.TextFragments;

        // Highlight each found date (optional action)
        foreach (TextFragment fragment in fragments)
        {
            fragment.TextState.BackgroundColor = Color.Yellow; // Aspose.Pdf.Drawing.Color
        }

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
