using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string backgroundImage = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(backgroundImage))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImage}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Create an ImageStamp for the background image
                ImageStamp imgStamp = new ImageStamp(backgroundImage)
                {
                    Background = true,               // draw behind existing page content
                    Opacity = 0.5,                  // subtle shading effect (BlendMode not available)
                    Width = doc.Pages[1].PageInfo.Width,
                    Height = doc.Pages[1].PageInfo.Height,
                    XIndent = 0,
                    YIndent = 0
                };

                // Apply the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Background image added and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
