using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            Document tempDoc = new Document();
            // Add a single blank page.
            tempDoc.Pages.Add();
            tempDoc.Save(inputPath);
        }

        // Bind the existing (or newly created) PDF file to the editor.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the clickable rectangle (x, y, width, height).
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // JavaScript code that shows an alert dialog.
            string jsCode = "app.alert('Hello from Aspose.Pdf!');";

            // Create a JavaScript link on page 1 with a visible red border.
            editor.CreateJavaScriptLink(jsCode, rect, 1, System.Drawing.Color.Red);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript link added and saved to '{outputPath}'.");
    }
}
