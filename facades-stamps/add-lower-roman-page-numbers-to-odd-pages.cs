using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Apply a lower‑roman page number stamp to odd pages only.
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber += 2)
        {
            // "#" is a placeholder that will be replaced by the actual page number.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("#")
            {
                NumberingStyle = NumberingStyle.NumeralsRomanLowercase,
                // Optional appearance settings (uncomment and adjust as needed):
                // TextState = new TextState
                // {
                //     Font = FontRepository.FindFont("Helvetica"),
                //     FontSize = 12,
                //     ForegroundColor = Aspose.Pdf.Color.Black
                // },
                // HorizontalAlignment = HorizontalAlignment.Center,
                // VerticalAlignment = VerticalAlignment.Bottom,
                // YIndent = 20f
            };

            // Add the stamp to the current odd page.
            doc.Pages[pageNumber].AddStamp(pageNumberStamp);
        }

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Page numbers added (lower‑roman) to odd pages: {outputPath}");
    }
}
