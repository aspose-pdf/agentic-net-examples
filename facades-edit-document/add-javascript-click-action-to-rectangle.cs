using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a Facades class) to edit the PDF.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file into the editor.
            editor.BindPdf(inputPath);

            // JavaScript code that will be executed when the rectangle is clicked.
            string jsCode = "app.alert('Hello from Aspose.Pdf!');";

            // Define the clickable rectangle (x, y, width, height).
            Rectangle rect = new Rectangle(100, 500, 200, 50);

            // Page numbers are 1‑based in Aspose.Pdf.
            int pageNumber = 1;

            // Color of the rectangle (visible when the link is active).
            Color linkColor = Color.Red;

            // Attach the JavaScript action to the rectangle on the specified page.
            editor.CreateJavaScriptLink(jsCode, rect, pageNumber, linkColor);

            // Save the modified PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript link added. Output saved to '{outputPath}'.");
    }
}