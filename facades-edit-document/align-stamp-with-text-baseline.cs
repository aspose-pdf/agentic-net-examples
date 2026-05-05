using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string stampImage = "stamp.png";          // image to use as stamp
        const string tempPdf    = "temp_with_stamp.pdf";
        const string outputPdf  = "output_aligned.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {stampImage}");
            return;
        }

        // -------------------------------------------------
        // 1. Add a stamp to the document (initial position is (0,0))
        // -------------------------------------------------
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);                     // load source PDF

            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();                       // create a generic stamp
            stamp.BindImage(stampImage);                     // use an image as stamp content
            stamp.SetOrigin(0, 0);                           // temporary origin
            stamp.IsBackground = false;                      // stamp on top of content

            fileStamp.AddStamp(stamp);                       // add the stamp
            fileStamp.Save(tempPdf);                         // save intermediate PDF
            fileStamp.Close();                               // finalize
        }

        // -------------------------------------------------
        // 2. Determine the baseline Y coordinate of the first text fragment on page 5
        // -------------------------------------------------
        double baselineY;
        double baselineX; // we keep the original X (left) position of the fragment
        using (Document doc = new Document(tempPdf))
        {
            // Extract text fragments from page 5
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[5].Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("No text fragments found on page 5.");
                return;
            }

            // Take the first fragment as reference
            TextFragment fragment = absorber.TextFragments[1];
            baselineY = fragment.Position.YIndent;   // baseline Y (from bottom)
            baselineX = fragment.Position.XIndent;   // baseline X (left)
        }

        // -------------------------------------------------
        // 3. Reposition the previously added stamp so its baseline aligns with the text baseline
        // -------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(tempPdf);                     // load the PDF that already contains the stamp

            // The stamp we added earlier is the first (and only) stamp on page 5, so its index is 1.
            // MoveStamp expects the new X and Y coordinates for the stamp origin.
            // We'll align the stamp's baseline (Y) with the text baseline and keep the X as the original.
            editor.MoveStamp(pageNumber: 5, stampIndex: 1, x: baselineX, y: baselineY);

            editor.Save(outputPdf);                      // save the final PDF with the aligned stamp
        }

        // Clean up the temporary file
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp repositioned and saved to '{outputPdf}'.");
    }
}