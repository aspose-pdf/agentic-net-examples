using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Choose the page from which the image will be removed (1‑based index)
                const int pageNumber = 1;
                if (pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine("Requested page does not exist.");
                    return;
                }

                Page page = doc.Pages[pageNumber];

                // Find the target raster image.
                // Here we simply take the first image in the page's resources.
                // In a real scenario you could match by image name, width, etc.
                XImage targetImage = null;
                foreach (XImage img in page.Resources.Images)
                {
                    targetImage = img;
                    break; // take the first one
                }

                if (targetImage == null)
                {
                    Console.WriteLine("No raster images found on the specified page.");
                }
                else
                {
                    string imageName = targetImage.Name; // the name used in the content stream

                    // 1) Remove the image reference from the page's content stream
                    //    The content stream contains Do operators that invoke the image XObject.
                    OperatorCollection ops = page.Contents;
                    for (int i = ops.Count; i >= 1; i--)
                    {
                        if (ops[i] is Do doOp && doOp.Name == imageName)
                        {
                            ops.Delete(i);
                        }
                    }

                    // 2) Remove the image from the page's resource dictionary
                    page.Resources.Images.Remove(targetImage);

                    Console.WriteLine($"Removed image '{imageName}' from page {pageNumber}.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
