using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF with logo stamp
        const string logoPath  = "logo.png";    // logo image file

        // Ensure the input files exist before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPdf);

        // Use ImageStamp for stamping an image (logo) onto pages.
        // ImageStamp exposes HorizontalAlignment, Opacity and other layout properties.
        ImageStamp stamp = new ImageStamp(logoPath)
        {
            HorizontalAlignment = HorizontalAlignment.Right, // align to the right margin
            Opacity = 0.8f
            // Note: ImageStamp does not have an IsBackground property. The stamp is placed on top of page content by default.
        };

        // Apply the stamp to every page in the document
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the modified PDF
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Logo stamp added with right alignment. Output saved to '{outputPdf}'.");
    }
}
