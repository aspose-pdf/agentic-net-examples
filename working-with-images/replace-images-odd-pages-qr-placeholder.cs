using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // for TextBoxField and barcode support

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over odd pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum += 2)
                {
                    Page page = doc.Pages[pageNum];

                    // Collect images first because we will modify the collection
                    var images = new List<XImage>();
                    foreach (XImage img in page.Resources.Images)
                        images.Add(img);

                    foreach (XImage img in images)
                    {
                        // Build a placeholder URL that points to the original image.
                        string url = $"https://example.com/original/{pageNum}/{Guid.NewGuid()}";

                        // Create a QR‑code placeholder using a TextBoxField.
                        // Position it in the top‑left corner (100x100 points).
                        var rect = new Aspose.Pdf.Rectangle(0, page.PageInfo.Height - 100, 100, page.PageInfo.Height);
                        var qrField = new TextBoxField(page, rect)
                        {
                            PartialName = $"qr_{pageNum}_{Guid.NewGuid()}"
                        };

                        // Add the field to the document form (associate it with the current page).
                        doc.Form.Add(qrField, pageNum);

                        // Draw a QR code (barcode) into the field.
                        // AddBarcode automatically creates a QR code when the content is a URL.
                        qrField.AddBarcode(url);

                        // OPTIONAL: remove the original image from the page resources.
                        // If the XImageCollection in your version supports Delete, uncomment the line below.
                        // page.Resources.Images.Delete(img);
                    }
                }

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
