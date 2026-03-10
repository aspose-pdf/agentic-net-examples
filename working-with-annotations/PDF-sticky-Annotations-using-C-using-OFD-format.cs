using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input OFD file (OFd is input‑only, we load it as a PDF document)
        const string ofdPath = "input.ofd";
        // Output PDF file with sticky annotations added
        const string pdfPath = "output.pdf";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load the OFD document using the OFD load options
        using (Document doc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Create a sticky (rubber‑stamp) annotation on the first page
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // StampAnnotation can display text or an image; here we use text
            StampAnnotation stamp = new StampAnnotation(doc.Pages[1], rect)
            {
                // The text shown inside the stamp
                Contents = "Sticky Note",
                // Optional visual properties
                Color = Aspose.Pdf.Color.Yellow,
                Opacity = 0.8,
                // Title appears in the popup window title bar
                Title = "Author",
                // Popup text (shown when the annotation is opened)
                Subject = "This is a sticky annotation added to an OFD‑derived PDF."
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(stamp);

            // Save the modified document as PDF (OFD cannot be saved)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Sticky annotation added and PDF saved to '{pdfPath}'.");
    }
}