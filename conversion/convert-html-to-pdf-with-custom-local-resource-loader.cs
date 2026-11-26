using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Input HTML file and desired PDF output
        string htmlPath = "input.html";
        string outputPdf = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Folder that contains the HTML file – used to resolve relative resources
        string baseFolder = Path.GetDirectoryName(Path.GetFullPath(htmlPath));

        // Configure HTML loading with a custom loader that serves only local assets
        Aspose.Pdf.HtmlLoadOptions loadOptions = new Aspose.Pdf.HtmlLoadOptions();
        loadOptions.CustomLoaderOfExternalResources = (string uri) =>
        {
            // Resolve the URI to a local file path
            string localPath = Path.Combine(baseFolder, uri.Replace('/', Path.DirectorySeparatorChar));

            // If the path does not exist, try handling absolute file URIs (file://)
            if (!File.Exists(localPath))
            {
                try
                {
                    Uri u = new Uri(uri);
                    if (u.IsFile)
                    {
                        localPath = u.LocalPath;
                    }
                }
                catch
                {
                    // Ignore malformed URIs – resource will be considered missing
                }
            }

            // If the resource exists, read its bytes and return a loading result
            if (File.Exists(localPath))
            {
                byte[] data = File.ReadAllBytes(localPath);
                // ResourceLoadingResult expects the raw byte array
                return new Aspose.Pdf.LoadOptions.ResourceLoadingResult(data);
            }

            // Resource not found – return null so Aspose.Pdf will skip it
            return null;
        };

        // Load the HTML document with the custom loader and save as PDF
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(htmlPath, loadOptions))
        {
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"HTML converted to PDF: {outputPdf}");
    }
}