using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string stampImg = "stamp.png";
        const string outputPdf = "stamped_output.pdf";

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

        try
        {
            // Load the original PDF. Using block ensures deterministic disposal.
            using (Document doc = new Document(inputPdf))
            {
                // Create an image stamp and configure its appearance.
                ImageStamp imgStamp = new ImageStamp(stampImg)
                {
                    // Position the stamp at the bottom‑right corner of each page.
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Set margins instead of the non‑existent Margin property.
                    RightMargin = 10,
                    BottomMargin = 10,
                    // Keep the original image dimensions (Zoom = 1.0 is the default).
                    Zoom = 1.0
                };

                // Apply the stamp to every page. This does not affect existing JavaScript actions.
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF. JavaScript actions embedded in the original
                // document are preserved automatically.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Image stamp added successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
