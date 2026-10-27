using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF with a form field
        const string outputPdfPath = "output.pdf";        // PDF after applying background image
        const string fieldName = "myButtonField";        // name of the target form field
        const string backgroundImg = "brand_logo.png";   // image to use as background

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundImg))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Retrieve the form field by name and ensure it is a button field
            if (pdfDoc.Form[fieldName] is ButtonField buttonField)
            {
                // Determine the page that contains the button. ButtonField does not expose a PageNumber
                // property, so we locate the page by checking which page rectangle fully contains the field's rectangle.
                int pageNumber = FindPageContainingRect(pdfDoc, buttonField.Rect);
                Page page = pdfDoc.Pages[pageNumber];

                // Use the button's rectangle to position the background image
                Aspose.Pdf.Rectangle btnRect = buttonField.Rect;
                double btnWidth = btnRect.URX - btnRect.LLX;
                double btnHeight = btnRect.URY - btnRect.LLY;

                // Create an ImageStamp for the background image
                ImageStamp imgStamp = new ImageStamp(backgroundImg)
                {
                    Width = btnWidth,
                    Height = btnHeight,
                    XIndent = btnRect.LLX,
                    YIndent = btnRect.LLY
                };

                // Add the stamp to the page (adding before the button keeps the image underneath)
                page.AddStamp(imgStamp);

                // Optionally clear the button's caption so only the image is visible
                // buttonField.NormalCaption = string.Empty;
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or not a ButtonField.");
                return;
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image applied to field '{fieldName}'. Saved as '{outputPdfPath}'.");
    }

    /// <summary>
    /// Finds the page index (1‑based) that fully contains the specified rectangle.
    /// If no page fully contains the rectangle, the method returns 1 as a fallback.
    /// </summary>
    private static int FindPageContainingRect(Document doc, Aspose.Pdf.Rectangle rect)
    {
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page pg = doc.Pages[i];
            // Compare the field rectangle with the page's media box (pg.Rect)
            if (rect.LLX >= pg.Rect.LLX && rect.LLY >= pg.Rect.LLY &&
                rect.URX <= pg.Rect.URX && rect.URY <= pg.Rect.URY)
            {
                return i;
            }
        }
        // Fallback – assume first page
        return 1;
    }
}
