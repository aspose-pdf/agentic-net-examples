using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "header_stamped.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Define header lines and their corresponding colors (Aspose.Pdf.Color to avoid ambiguity)
        string[] headerLines = { "Company Confidential", "Prepared for: John Doe", "Date: 2024‑10‑05" };
        Aspose.Pdf.Color[] lineColors = { Aspose.Pdf.Color.Red, Aspose.Pdf.Color.Green, Aspose.Pdf.Color.Blue };

        // Load the PDF document
        Document pdfDoc = new Document(inputPdf);

        // Apply each line as a separate TextStamp with its own color
        for (int i = 0; i < headerLines.Length; i++)
        {
            // Create a TextStamp for the current line
            TextStamp stamp = new TextStamp(headerLines[i])
            {
                // Center the stamp horizontally at the top of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top,
                // Offset each line vertically (TopMargin is measured from the top edge)
                TopMargin = 20 + i * 20
            };

            // Configure the visual appearance of the stamp
            stamp.TextState.FontSize = 14;
            stamp.TextState.ForegroundColor = lineColors[i];

            // Add the stamp to every page of the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(stamp);
            }
        }

        // Save the modified PDF
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Header stamp applied successfully. Output saved to '{outputPdf}'.");
    }
}
