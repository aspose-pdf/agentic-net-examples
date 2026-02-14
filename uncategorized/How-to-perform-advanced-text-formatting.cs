using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        try
        {
            // Create a new PDF document
            Document pdfDoc = new Document();

            // Add a blank page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a TextStamp with supported formatting options
            TextStamp stamp = new TextStamp(
                "This is a sample text demonstrating advanced formatting options such as word wrap, line spacing, hyphenation, justification, and automatic font size adjustment.")
            {
                // Position the stamp on the page
                XIndent = 50,          // distance from the left edge
                YIndent = 700,         // distance from the bottom edge
                Width = 500,           // desired width of the stamp rectangle
                Height = 200,          // desired height of the stamp rectangle

                // Render the stamp as text (not as graphic operators)
                Draw = true,

                // Supported text formatting settings
                Justify = true,                               // justify text (both left and right edges)
                AutoAdjustFontSizeToFitStampRectangle = true // shrink font if text exceeds rectangle
            };

            // Add the formatted stamp to the page
            page.AddStamp(stamp);

            // Save the PDF using a generic filename
            string outputPath = "output.pdf";
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}