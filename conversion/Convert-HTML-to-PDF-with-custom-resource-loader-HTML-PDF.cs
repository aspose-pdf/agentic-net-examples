using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Custom loader that reads the requested external resource (e.g., image or CSS) from the file system.
    // The method receives the URI of the resource and must return a ResourceLoadingResult containing the raw bytes.
    private static LoadOptions.ResourceLoadingResult CustomResourceLoader(string uri)
    {
        // Resolve the URI to a physical file path. In a real scenario you might map URLs,
        // fetch from a database, cloud storage, etc.
        string filePath = uri;

        // If the file does not exist, return an empty result to let the converter handle it.
        if (!File.Exists(filePath))
            return new LoadOptions.ResourceLoadingResult(Array.Empty<byte>());

        // Read the resource bytes.
        byte[] data = File.ReadAllBytes(filePath);

        // Create the result object. The constructor requires the byte array.
        // Content type can be inferred by the converter, so we do not set it explicitly.
        return new LoadOptions.ResourceLoadingResult(data);
    }

    static void Main()
    {
        // Paths to the input HTML file and the desired PDF output.
        const string htmlFilePath = "input.html";
        const string pdfOutputPath = "output.pdf";

        // Ensure the input file exists.
        if (!File.Exists(htmlFilePath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlFilePath}");
            return;
        }

        // Initialize HtmlLoadOptions with a base path so that relative URLs in the HTML are resolved correctly.
        // Here we use the directory of the HTML file as the base path.
        string basePath = Path.GetDirectoryName(Path.GetFullPath(htmlFilePath));
        HtmlLoadOptions htmlLoadOptions = new HtmlLoadOptions(basePath);

        // Assign the custom resource loader delegate.
        htmlLoadOptions.CustomLoaderOfExternalResources =
            new LoadOptions.ResourceLoadingStrategy(CustomResourceLoader);

        // Load the HTML and convert it to PDF.
        using (Document pdfDocument = new Document(htmlFilePath, htmlLoadOptions))
        {
            pdfDocument.Save(pdfOutputPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfOutputPath}");
    }
}