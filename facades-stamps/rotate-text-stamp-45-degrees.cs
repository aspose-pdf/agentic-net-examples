using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal source PDF if it does not already exist.
        // This satisfies the sandbox where no external files are present.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPath);
            }
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Create a text stamp
        TextStamp stamp = new TextStamp("CONFIDENTIAL");
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 36;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red; // fully‑qualified Aspose color
        stamp.RotateAngle = 45f; // rotate 45 degrees around its centre (correct property)
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Center;
        // Optional: adjust position if needed
        // stamp.XIndent = 200; // not required when using centre alignment
        // stamp.YIndent = 400;

        // Apply the stamp to every page (or select specific pages as needed)
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the stamped PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
