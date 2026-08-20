using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade APIs for stamping
using Aspose.Pdf.Text;   // Required for TextFragment
using System.Drawing;   // Required for color definitions (System.Drawing.Color)

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // source PDF
        const string outputPdf  = "output.pdf";  // result PDF
        const string imagePath  = "image.jpg";   // image to insert
        const string caption    = "Sample Caption";

        // Ensure input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Use PdfFileStamp to add the image as a stamp annotation
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF
            fileStamp.BindPdf(inputPdf);

            // Create a stamp, bind the image, and position it at (50,150)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imagePath);
            stamp.SetOrigin(50, 150);          // lower‑left corner of the stamp
            stamp.SetImageSize(100, 100);      // width and height of the image
            stamp.Opacity = 0.9f;              // semi‑transparent
            stamp.IsBackground = false;        // appear above page content

            // Add the image stamp to the document
            fileStamp.AddStamp(stamp);

            // -----------------------------------------------------------------
            // Add a caption underneath the image.
            // Instead of PdfAnnotationEditor (which has no CreateText method),
            // we add a TextFragment directly to the page.
            // -----------------------------------------------------------------
            Document doc = fileStamp.Document; // the underlying Aspose.Pdf.Document
            TextFragment tf = new TextFragment(caption);
            tf.Position = new Position(50, 130); // place slightly below the image
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            // Fully qualify the Color to avoid ambiguity between System.Drawing.Color and Aspose.Pdf.Color
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            // Add the text fragment to the first page
            doc.Pages[1].Paragraphs.Add(tf);

            // Save the modified PDF
            fileStamp.Save(outputPdf);

            // Clean up facades (optional, using statement will dispose)
            fileStamp.Close();
        }

        Console.WriteLine($"Image annotation with caption saved to '{outputPdf}'.");
    }
}
