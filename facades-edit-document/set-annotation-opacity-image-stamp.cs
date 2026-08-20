using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.png"; // any image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the standard Document lifecycle rule
        using (Document doc = new Document(inputPdf))
        {
            // Create a stamp via the Facades API
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind an image to the stamp (any image can be used)
            stamp.BindImage(stampImage);

            // Set the desired opacity (0.75 = 75% opaque). Use float literal.
            stamp.Opacity = 0.75F;

            // NOTE: Aspose.Pdf.Facades.Stamp does NOT expose a BlendMode property.
            // If a blend mode is required, use Aspose.Pdf.PdfPageStamp (from the Aspose.Pdf namespace)
            // which supports the BlendMode property. For this example we keep only opacity.

            // Use PdfFileStamp to apply the stamp to all pages
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(doc);
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Annotation opacity set to 0.75. Saved as '{outputPdf}'.");
    }
}
