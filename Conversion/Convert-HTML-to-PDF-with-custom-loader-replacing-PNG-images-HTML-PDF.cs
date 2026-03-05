using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Custom loader that replaces every PNG image with a predefined replacement image.
    // The delegate matches Aspose.Pdf.LoadOptions.ResourceLoadingStrategy:
    // it receives the URI of the requested resource and must return a ResourceLoadingResult.
    static LoadOptions.ResourceLoadingResult CustomResourceLoader(string uri)
    {
        // If the requested resource is a PNG, load the replacement image.
        if (uri.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
        {
            const string replacementPath = "replacement.png";
            if (File.Exists(replacementPath))
            {
                // Load the replacement image bytes and return a result constructed with the data.
                byte[] data = File.ReadAllBytes(replacementPath);
                return new LoadOptions.ResourceLoadingResult(data);
            }
        }
        else
        {
            // For non‑PNG resources, try to load the original file if it exists on disk.
            if (File.Exists(uri))
            {
                byte[] data = File.ReadAllBytes(uri);
                return new LoadOptions.ResourceLoadingResult(data);
            }
        }

        // Return null to let Aspose.Pdf use its default loading mechanism when we cannot provide data.
        return null;
    }

    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure HTML load options and assign the custom resource loader.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        loadOptions.CustomLoaderOfExternalResources = new LoadOptions.ResourceLoadingStrategy(CustomResourceLoader);

        // Load the HTML document using the custom loader and save it as PDF.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath); // No SaveOptions needed for PDF output.
        }

        Console.WriteLine($"HTML converted to PDF successfully: '{pdfPath}'");
    }
}
