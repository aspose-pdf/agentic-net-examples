using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Absorb all text fragments from the whole document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            pdfDoc.Pages.Accept(absorber);

            // Prepare the output text file
            using (StreamWriter writer = new StreamWriter(outputTxtPath))
            {
                // Iterate through each page (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Gather all text segments that belong to the current page
                    List<TextSegment> pageSegments = new List<TextSegment>();
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        if (fragment.Page?.Number != pageIndex) continue;

                        foreach (TextSegment segment in fragment.Segments)
                        {
                            pageSegments.Add(segment);
                        }
                    }

                    if (pageSegments.Count == 0) continue;

                    // Compute average font size and baseline position for the page
                    double avgFontSize   = pageSegments.Average(s => s.TextState.FontSize);
                    // Use the lower‑left Y (LLY) of the segment rectangle as baseline Y
                    double avgBaselineY  = pageSegments.Average(s => s.Rectangle.LLY);

                    // Analyze each segment to decide if it is superscript, subscript or normal
                    foreach (TextSegment segment in pageSegments)
                    {
                        string classification = "Normal";

                        // Heuristic: smaller font size indicates possible super/subscript
                        if (segment.TextState.FontSize < avgFontSize * 0.8)
                        {
                            double deltaY = segment.Rectangle.LLY - avgBaselineY;

                            // Positive delta => higher than baseline => superscript
                            // Negative delta => lower than baseline => subscript
                            if (deltaY > 2)          classification = "Superscript";
                            else if (deltaY < -2)    classification = "Subscript";
                        }

                        // Write the result to the output file
                        writer.WriteLine($"Page {pageIndex}: \"{segment.Text}\" - {classification}");
                    }
                }
            }

            Console.WriteLine($"Extraction completed. Results saved to '{outputTxtPath}'.");
        }
    }
}
