using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Custom loader that reads external resources (e.g., images, CSS) from the file system.
    // The delegate must return a ResourceLoadingResult constructed with the resource bytes.
    private static LoadOptions.ResourceLoadingResult CustomResourceLoader(string resourceUri)
    {
        // Resolve the absolute path of the requested resource.
        // In a real scenario you might fetch the resource from a database, cloud storage, etc.
        string absolutePath = Path.GetFullPath(resourceUri);

        // If the resource does not exist, return an empty result to let the converter handle it.
        if (!File.Exists(absolutePath))
            return new LoadOptions.ResourceLoadingResult(Array.Empty<byte>());

        // Read the resource bytes and create the result object.
        byte[] data = File.ReadAllBytes(absolutePath);
        return new LoadOptions.ResourceLoadingResult(data);
    }

    static void Main()
    {
        const string htmlPath = "input.html";   // Path to the source HTML file
        const string pdfPath  = "output.pdf";   // Desired output PDF file

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure HTML loading options.
        // - Set base path so that relative URIs inside the HTML are resolved correctly.
        // - Force rendering of the whole document onto a single PDF page (optional).
        // - Assign the custom resource loader to handle external resources.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions(Path.GetDirectoryName(htmlPath));
        loadOptions.IsRenderToSinglePage = true;
        loadOptions.CustomLoaderOfExternalResources = new LoadOptions.ResourceLoadingStrategy(CustomResourceLoader);

        // Load the HTML and convert it to PDF.
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
    }
}