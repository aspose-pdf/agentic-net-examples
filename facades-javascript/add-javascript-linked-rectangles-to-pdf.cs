using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF to obtain the page count.
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Bind the PDF file to the content editor.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Helper to create a rectangle, draw it, and attach a JavaScript action.
            void AddJsRectangle(int page, System.Drawing.Rectangle rect, string js, System.Drawing.Color borderColor)
            {
                // Draw a visible rectangle (optional – you can set any text as the content).
                editor.CreateSquareCircle(rect, "Box", borderColor, true, page, 2);
                // Attach the JavaScript action to the same rectangle area.
                editor.CreateJavaScriptLink(js, rect, page, borderColor);
            }

            // Rectangle 1 on page 1.
            AddJsRectangle(
                page: 1,
                rect: new System.Drawing.Rectangle(100, 700, 100, 50), // x, y, width, height
                js: "app.alert('Rectangle 1 clicked');",
                borderColor: System.Drawing.Color.Red);

            // Rectangle 2 on page 1.
            AddJsRectangle(
                page: 1,
                rect: new System.Drawing.Rectangle(250, 700, 100, 50),
                js: "app.alert('Rectangle 2 clicked');",
                borderColor: System.Drawing.Color.Green);

            // Rectangle 3 on page 2 (if it exists).
            if (pageCount >= 2)
            {
                AddJsRectangle(
                    page: 2,
                    rect: new System.Drawing.Rectangle(100, 500, 200, 50),
                    js: "app.alert('Rectangle 3 on page 2 clicked');",
                    borderColor: System.Drawing.Color.Blue);
            }

            // Save the modified PDF.
            editor.Save(outputPath);
            Console.WriteLine($"PDF with JavaScript‑linked rectangles saved to '{outputPath}'.");
        }
    }
}
