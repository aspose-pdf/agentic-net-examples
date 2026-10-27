using System;
using System.IO;
using Aspose.Pdf;

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
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    double pageWidth = page.PageInfo.Width;
                    double pageHeight = page.PageInfo.Height;

                    // Create an Image object from the PNG file
                    using (FileStream imgStream = File.OpenRead(backgroundImage))
                    {
                        Image img = new Image
                        {
                            ImageStream = imgStream,
                            // Set the image size to cover the whole page
                            FixWidth = pageWidth,
                            FixHeight = pageHeight,
                            // Position at lower‑left corner (0,0)
                            // No need to set coordinates because FixWidth/FixHeight are used
                        };

                        // Insert the image as the first element so it appears behind existing content
                        page.Paragraphs.Insert(0, img);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Background image added to all pages. Saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
