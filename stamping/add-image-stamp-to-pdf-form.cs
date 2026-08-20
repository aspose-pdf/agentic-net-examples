using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "form_input.pdf";
        const string stampImg  = "logo.png";
        const string outputPdf = "form_with_stamp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF (contains form fields) and keep it in a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over pages (1‑based indexing) and add the image stamp
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(stampImg)
                {
                    // Position the stamp – here we place it 50 points from left and bottom
                    XIndent = 50,
                    YIndent = 50,
                    // Optional: set opacity so the stamp does not obscure form fields
                    Opacity = 0.5f,
                    // Optional: alignments can be used instead of XIndent/YIndent
                    // HorizontalAlignment = HorizontalAlignment.Left,
                    // VerticalAlignment   = VerticalAlignment.Bottom,
                };

                // Add the stamp to the current page; this does NOT flatten or alter form fields
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; fields remain functional
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}