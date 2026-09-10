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

        // Create a simple HTML file if it does not exist – makes the example self‑contained.
        if (!File.Exists(htmlPath))
        {
            string htmlContent = "<html><body><h1>Hello Aspose PDF</h1><p>This PDF was generated from HTML.</p></body></html>";
            File.WriteAllText(htmlPath, htmlContent);
        }

        // Configuration switch – set to true in test environments to disable BaseUrl injection.
        bool disableBaseUrlInjection = true;

        // Choose HtmlLoadOptions based on the switch.
        HtmlLoadOptions loadOptions = disableBaseUrlInjection
            ? new HtmlLoadOptions() // No BaseUrl injected.
            : new HtmlLoadOptions(Path.GetDirectoryName(Path.GetFullPath(htmlPath)) ?? string.Empty);

        // Load the HTML and generate the PDF.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Example usage of a Facade class (PdfViewer) – demonstrates the Facade namespace usage.
            PdfViewer viewer = new PdfViewer();
            try
            {
                viewer.BindPdf(pdfDocument);
                // Additional facade operations could be performed here (e.g., printing).

                // Save the PDF *before* closing/disposing the viewer to avoid ObjectDisposedException.
                pdfDocument.Save(pdfPath);
            }
            finally
            {
                viewer.Close();
            }
        }

        Console.WriteLine($"PDF generated at '{pdfPath}'. BaseUrl injection disabled: {disableBaseUrlInjection}");
    }
}
