using System;
using System.IO;
using Aspose.Pdf.Facades;

class MergeImagesToPdf
{
    static void Main(string[] args)
    {
        // Example usage:
        // args[0] = output PDF path
        // args[1..n] = image file paths to merge
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: MergeImagesToPdf <output.pdf> <image1> [<image2> ...]");
            return;
        }

        string outputPdfPath = args[0];
        string[] imagePaths = new string[args.Length - 1];
        Array.Copy(args, 1, imagePaths, 0, imagePaths.Length);

        // Validate all image files exist before proceeding
        foreach (string imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Error: Image file not found – {imgPath}");
                return;
            }
        }

        try
        {
            // PdfFileMend creates an empty PDF document internally
            using (PdfFileMend mend = new PdfFileMend())
            {
                // The underlying Document starts with a single empty page
                // We'll add one page per image (the first page is already present)
                int pageNumber = 1;

                foreach (string imgPath in imagePaths)
                {
                    // Ensure the document has enough pages
                    if (pageNumber > mend.Document.Pages.Count)
                    {
                        mend.Document.Pages.Add();
                    }

                    // Get page dimensions (in points)
                    var page = mend.Document.Pages[pageNumber];
                    float llx = 0f;
                    float lly = 0f;
                    float urx = (float)page.PageInfo.Width;
                    float ury = (float)page.PageInfo.Height;

                    // Add the image to the current page, filling the whole page
                    mend.AddImage(imgPath, pageNumber, llx, lly, urx, ury);

                    pageNumber++;
                }

                // Save the resulting PDF
                mend.Save(outputPdfPath);
                Console.WriteLine($"Successfully merged {imagePaths.Length} image(s) into '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while merging images: {ex.Message}");
        }
    }
}