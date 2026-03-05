using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";
        const string htmlWithLayersPath = "output_layers.html";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file and convert it to PDF.
        Aspose.Pdf.HtmlLoadOptions htmlLoadOptions = new Aspose.Pdf.HtmlLoadOptions();
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(htmlPath, htmlLoadOptions))
        {
            // Save the document as PDF.
            pdfDoc.Save(pdfPath);
        }

        // Load the generated PDF and convert it to HTML while preserving PDF layers.
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath))
        {
            Aspose.Pdf.HtmlSaveOptions htmlSaveOptions = new Aspose.Pdf.HtmlSaveOptions
            {
                // Export each marked‑content (layer) as a separate HTML div.
                ConvertMarkedContentToLayers = true
                // Uncomment the next line to generate one HTML file per PDF page.
                // SplitIntoPages = true
            };

            pdfDoc.Save(htmlWithLayersPath, htmlSaveOptions);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        Console.WriteLine($"PDF successfully converted to HTML with layers: {htmlWithLayersPath}");
    }
}