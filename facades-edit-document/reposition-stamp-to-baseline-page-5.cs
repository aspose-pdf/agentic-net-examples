using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.png";

        if (!File.Exists(inputPdf) || !File.Exists(stampImage))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF to obtain the baseline Y coordinate of the first text fragment on page 5
        double baselineY = 0;
        using (Document doc = new Document(inputPdf))
        {
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("PDF has less than 5 pages.");
                return;
            }

            // Extract text fragments from page 5
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[5].Accept(absorber);

            if (absorber.TextFragments.Count > 0)
            {
                // Use the rectangle of the first fragment to get the lower‑left Y (baseline) coordinate
                // TextFragmentAbsorber returns a collection that is zero‑based.
                baselineY = absorber.TextFragments[0].Rectangle.LLY;
            }
            else
            {
                Console.Error.WriteLine("No text found on page 5.");
                return;
            }
        }

        // Add an image stamp to the PDF (initial position will be adjusted later)
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampImage);
        stamp.SetOrigin(0, 0);          // temporary origin – will be moved later
        stamp.IsBackground = false;    // stamp on top of content
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        // Reposition the stamp on page 5 to align with the text baseline
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(outputPdf);
        // MoveStamp(pageNumber, stampIndex, x, y)
        // The stamp we added is the first (and only) stamp on the page – index is 0‑based.
        double desiredX = 50; // horizontal offset from the left edge
        editor.MoveStamp(5, 0, desiredX, baselineY);
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Stamp repositioned and saved to '{outputPdf}'.");
    }
}
