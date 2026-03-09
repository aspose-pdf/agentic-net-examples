using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the existing PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Ensure there is at least one page to work with
            Aspose.Pdf.Page page = doc.Pages[1];

            // Define margins (in points). 1 inch = 72 points.
            const float leftMargin   = 50f;
            const float rightMargin  = 50f;
            const float topMargin    = 50f;
            const float bottomMargin = 50f;

            // Calculate the usable width and height on the page
            float pageWidth  = (float)page.PageInfo.Width;
            float pageHeight = (float)page.PageInfo.Height;

            float usableWidth  = pageWidth  - leftMargin - rightMargin;
            float usableHeight = pageHeight - topMargin - bottomMargin;

            // Define a rectangle where the text will be placed
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(
                leftMargin,
                bottomMargin,
                leftMargin + usableWidth,
                bottomMargin + usableHeight);

            // Create a text paragraph that will automatically wrap lines
            Aspose.Pdf.Text.TextParagraph paragraph = new Aspose.Pdf.Text.TextParagraph
            {
                Rectangle = textRect,
                // Enable word‑by‑word wrapping
                FormattingOptions = { WrapMode = Aspose.Pdf.Text.TextFormattingOptions.WordWrapMode.ByWords }
            };

            // Sample long text that needs line breaking
            string longText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                              "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                              "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " +
                              "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla " +
                              "pariatur. Excepteur sint occaecat cupidatat non proident, sunt in " +
                              "culpa qui officia deserunt mollit anim id est laborum.";

            // Append the text as a single line; the paragraph will handle wrapping
            paragraph.AppendLine(longText);

            // Use TextBuilder to add the paragraph to the page
            Aspose.Pdf.Text.TextBuilder builder = new Aspose.Pdf.Text.TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with calculated line breaks saved to '{outputPdfPath}'.");
    }
}