using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Name of the form field to move
        const string fieldName = "Logo";

        // Desired size of the field (points)
        const double fieldWidth = 150; // example width
        const double fieldHeight = 50; // example height

        // ---------------------------------------------------------------------
        // Ensure the input PDF exists. If it does not, create a minimal PDF that
        // contains a form field named "Logo" (a button field works well for a logo).
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using var placeholder = new Document();
            Page page = placeholder.Pages.Add();

            // Create a button field – commonly used for image/logo placeholders.
            // ButtonField requires a Page and a Rectangle in its constructor.
            var logoField = new ButtonField(page, new Aspose.Pdf.Rectangle(0, 0, fieldWidth, fieldHeight))
            {
                PartialName = fieldName
            };
            placeholder.Form.Add(logoField);
            placeholder.Save(inputPdf);
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Get the height of the first page (origin is bottom‑left)
        double pageHeight = doc.Pages[1].PageInfo.Height;

        // ---------------------------------------------------------------------
        // Calculate rectangle for top‑left placement.
        // lower‑left X = 0 (left margin)
        // lower‑left Y = pageHeight - fieldHeight (top margin)
        // upper‑right X = fieldWidth
        // upper‑right Y = pageHeight
        // ---------------------------------------------------------------------
        Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(
            0,                     // lower‑left X
            pageHeight - fieldHeight, // lower‑left Y
            fieldWidth,            // upper‑right X
            pageHeight             // upper‑right Y
        );

        // Retrieve the field from the document's form collection and set the new rectangle
        if (doc.Form != null && doc.Form[fieldName] != null)
        {
            doc.Form[fieldName].Rect = fieldRect;
        }
        else
        {
            Console.WriteLine($"Field '{fieldName}' not found in the PDF form.");
        }

        // Save the modified PDF
        doc.Save(outputPdf);
    }
}
