using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";

        // Directory where the HTML file and the CSS file will be written.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(htmlPath));

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Custom CSS that should style the generated HTML.
        const string customCss = @"
body { font-family: Arial, sans-serif; background-color: #f9f9f9; }
.my_prefix_1 { color: #ff0000; font-weight: bold; }
";

        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options.
                HtmlSaveOptions saveOptions = new HtmlSaveOptions();

                // Optional: set a prefix for generated CSS class names.
                saveOptions.CssClassNamesPrefix = "my_prefix_";

                // Provide a custom strategy for saving the CSS file.
                saveOptions.CustomCssSavingStrategy = new HtmlSaveOptions.CssSavingStrategy(info =>
                {
                    string cssFilePath = Path.Combine(outputDir, info.SupposedURL);
                    File.WriteAllText(cssFilePath, customCss);
                });

                // Perform the conversion.
                pdfDoc.Save(htmlPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
            Console.WriteLine("Custom CSS applied.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
