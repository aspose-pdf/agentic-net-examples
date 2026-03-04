using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string newImage = "newImage.jpg";
        const string outputPdf = "output.pdf";
        const int pageNumber = 1;   // 1‑based page index
        const int imageIndex = 1;   // 1‑based image index on the page

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(newImage))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImage}");
            return;
        }

        try
        {
            // Load the PDF document; using ensures proper disposal.
            using (Document doc = new Document(inputPdf))
            {
                // PdfContentEditor can edit page content such as images.
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Bind the loaded document to the editor.
                    editor.BindPdf(doc);

                    // Replace the specified image on the given page.
                    editor.ReplaceImage(pageNumber, imageIndex, newImage);

                    // Save the modified PDF.
                    editor.Save(outputPdf);
                }
            }

            Console.WriteLine($"Image successfully replaced. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}