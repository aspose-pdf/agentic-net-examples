using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string customCssPath = "custom.css";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        using (Document pdfDoc = new Document(pdfPath))
        {
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();
            // Prefix generated CSS class names (optional)
            htmlOptions.CssClassNamesPrefix = "custom_";
            // Provide a custom CSS file via the delegate
            htmlOptions.CustomCssSavingStrategy = (info) =>
            {
                // Write your custom CSS content to the desired file
                string cssContent = "/* Custom CSS */\n" +
                                    "body { background-color: #f0f0f0; }\n" +
                                    ".custom_paragraph { color: #003366; font-size: 14px; }";
                File.WriteAllText(customCssPath, cssContent);
            };

            try
            {
                pdfDoc.Save(htmlPath, htmlOptions);
                Console.WriteLine($"HTML saved to '{htmlPath}'. Custom CSS written to '{customCssPath}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and works only on Windows
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped.");
            }
        }
    }
}