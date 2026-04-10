using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string imagesRootFolder = "PageImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root folder for images exists
        Directory.CreateDirectory(imagesRootFolder);

        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Generate one HTML file per PDF page
                    SplitIntoPages = true,

                    // Custom strategy to store each page's images in its own subfolder
                    CustomResourceSavingStrategy = resourceInfo =>
                    {
                        // Only handle image resources
                        if (resourceInfo is HtmlSaveOptions.HtmlImageSavingInfo imgInfo)
                        {
                            // Determine subfolder for the current HTML page (which corresponds to a PDF page)
                            string pageFolder = Path.Combine(imagesRootFolder,
                                $"Page_{imgInfo.HtmlHostPageNumber}");
                            Directory.CreateDirectory(pageFolder);

                            // Build full file path for the image on disk
                            string imagePath = Path.Combine(pageFolder, imgInfo.SupposedFileName);

                            // Save the image stream to disk
                            using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                            {
                                imgInfo.ContentStream.CopyTo(fs);
                            }

                            // Tell Aspose.Pdf that we have already processed the resource
                            imgInfo.CustomProcessingCancelled = true;

                            // Return the relative path that will be used in the generated HTML
                            return Path.Combine($"Page_{imgInfo.HtmlHostPageNumber}", imgInfo.SupposedFileName);
                        }

                        // For all other resources let Aspose.Pdf handle them (return null)
                        return null;
                    }
                };

                // Perform the conversion (HTML conversion requires GDI+ on Windows)
                try
                {
                    pdfDoc.Save(outputHtmlPath, htmlOpts);
                    Console.WriteLine($"HTML conversion completed. Output: {outputHtmlPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows GDI+. Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
