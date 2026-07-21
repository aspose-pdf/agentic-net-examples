using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputRoot = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the root folder for extracted images exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the signed PDF (Document implements IDisposable)
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    string pageFolder = Path.Combine(outputRoot, $"Page_{i}");
                    Directory.CreateDirectory(pageFolder);

                    int imageIndex = 1;
                    // Iterate over images on the current page
                    foreach (XImage img in page.Resources.Images)
                    {
                        string ext = GetImageExtension(); // default to .png
                        string filePath = Path.Combine(pageFolder, $"Image_{imageIndex}{ext}");

                        // XImage.Save expects a Stream, not a file path
                        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        imageIndex++;
                    }
                }
            }

            Console.WriteLine("All images have been extracted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }

    // Aspose.Pdf.XImage does not expose MIME information; default to PNG
    static string GetImageExtension()
    {
        return ".png";
    }
}
