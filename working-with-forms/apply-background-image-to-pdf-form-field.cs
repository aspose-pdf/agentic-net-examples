using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF with the form field
        const string outputPdfPath = "output.pdf";        // Resulting PDF
        const string fieldName = "BrandField";           // Name of the target form field
        const string imagePath = "brand_logo.png";       // Background image to apply

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve the widget annotation that represents the form field
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a widget annotation.");
                return;
            }

            // Determine the page that contains the field and its rectangle
            int pageNumber = FindPageNumberContainingAnnotation(doc, widget);
            if (pageNumber == -1)
            {
                Console.Error.WriteLine($"Unable to locate the page for field '{fieldName}'.");
                return;
            }
            var fieldRect = widget.Rect;

            // Create an ImageStamp that matches the field rectangle and set it as background
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                Background = true,
                XIndent = fieldRect.LLX,
                YIndent = fieldRect.LLY,
                Width = fieldRect.Width,
                Height = fieldRect.Height
            };

            // Add the stamp to the same page as the field
            doc.Pages[pageNumber].AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image applied to field '{fieldName}' and saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Finds the page number that contains the specified annotation.
    /// Returns -1 if the annotation is not found on any page.
    /// </summary>
    private static int FindPageNumberContainingAnnotation(Document doc, Annotation annotation)
    {
        foreach (Page page in doc.Pages)
        {
            if (page.Annotations != null && page.Annotations.Contains(annotation))
            {
                return page.Number; // Page numbers are 1‑based in Aspose.Pdf
            }
        }
        return -1;
    }
}
