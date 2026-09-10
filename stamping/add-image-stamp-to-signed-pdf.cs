using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_input.pdf";   // digitally signed PDF
        const string outputPdf = "signed_with_stamp.pdf";
        const string stampImagePath = "logo.png";      // image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the signed PDF (no special load options needed)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                Opacity             = 0.5,   // semi‑transparent
                Background          = false // stamp on top of page content
            };

            // Apply the stamp to the first page (or any page you need)
            Page page = doc.Pages[1];
            page.AddStamp(imgStamp);

            // Save using incremental (append) update to preserve existing digital signatures.
            // In recent Aspose.PDF versions the incremental‑update behaviour is enabled by default
            // when the document already contains signatures. Therefore we can simply save the
            // document without specifying any special option.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}
