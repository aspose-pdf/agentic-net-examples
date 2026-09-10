using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "HtmlOutput";

        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(pdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(pdfPath);
        }

        // Make sure the output directory exists.
        Directory.CreateDirectory(outputFolder);
        string htmlPath = Path.Combine(outputFolder, "document.html");

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Configure HTML conversion options.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                SplitIntoPages = true,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
            };

            // Store each page's images in its own sub‑folder.
            htmlOpts.CustomResourceSavingStrategy = resourceInfo =>
            {
                var imgInfo = resourceInfo as HtmlSaveOptions.HtmlImageSavingInfo;
                if (imgInfo == null)
                    return null; // Not an image – let the default handler take over.

                string pageFolder = Path.Combine(outputFolder, $"Page_{imgInfo.HtmlHostPageNumber}");
                Directory.CreateDirectory(pageFolder);

                string fileName = imgInfo.SupposedFileName;
                if (string.IsNullOrEmpty(fileName))
                    fileName = $"image_{Guid.NewGuid():N}.png";

                string filePath = Path.Combine(pageFolder, fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    imgInfo.ContentStream.CopyTo(fs);
                }

                // Return the full path so Aspose.Pdf knows where the resource was saved.
                return filePath;
            };

            // Perform the conversion.
            pdfDoc.Save(htmlPath, htmlOpts);
        }

        Console.WriteLine($"PDF converted to HTML. Files are located in '{outputFolder}'.");
    }
}
