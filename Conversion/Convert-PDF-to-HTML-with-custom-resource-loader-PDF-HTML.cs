using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required by the task specification

class Program
{
    static void Main()
    {
        // Input PDF file and desired HTML output file
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        // Folder where extracted resources (images, fonts, etc.) will be saved
        const string resourcesFolder = "Resources";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the resources folder exists
        Directory.CreateDirectory(resourcesFolder);

        // Configure HTML save options with a custom resource‑saving strategy
        HtmlSaveOptions htmlSaveOptions = new HtmlSaveOptions();

        // The delegate receives information about each external resource that the converter
        // wants to write (image, font, CSS, …). We write the resource to our folder and
        // return a URL that will be placed in the generated HTML.
        htmlSaveOptions.CustomResourceSavingStrategy = new HtmlSaveOptions.ResourceSavingStrategy(resourceInfo =>
        {
            // Use the suggested file name from the converter
            string fileName = resourceInfo.SupposedFileName;

            // Build a full path inside the resources folder
            string filePath = Path.Combine(resourcesFolder, fileName);

            // Persist the resource stream to disk
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                resourceInfo.ContentStream.CopyTo(fs);
            }

            // Return a relative URL (using forward slashes) that the HTML will reference
            return Path.Combine(resourcesFolder, fileName).Replace('\\', '/');
        });

        // Example: embed all resources directly into the HTML file (optional)
        // htmlSaveOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

        // Load the PDF and perform the conversion inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            pdfDocument.Save(outputHtmlPath, htmlSaveOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
    }
}