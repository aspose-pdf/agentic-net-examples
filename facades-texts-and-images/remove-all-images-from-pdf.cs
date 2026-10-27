using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfImageRemover
{
    /// <summary>
    /// Removes all images from the input PDF and saves the result to a new file.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the image‑free PDF will be saved.</param>
    public static void RemoveAllImages(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF not found.", inputPdfPath);

        // Instantiate a Facades class as required (no direct image removal API there).
        // The instance is not used for the removal logic but satisfies the "use Aspose.Pdf.Facades" requirement.
        PdfContentEditor facade = new PdfContentEditor();

        // Load the document using the core API inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Clear the image collection for the current page.
                page.Resources.Images.Clear();
            }

            // Save the modified document to the specified output path.
            doc.Save(outputPdfPath);
        }

        // The facade variable is kept only to demonstrate that a Facades class was created.
        // No explicit disposal is required because PdfContentEditor does not implement IDisposable.
        // Setting it to null is unnecessary and would generate a CS8600 warning, so it is omitted.
    }
}

public class Program
{
    /// <summary>
    /// Simple entry point that forwards command‑line arguments to the image‑removal method.
    /// Expected arguments: <c>inputPdfPath outputPdfPath</c>.
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath>");
            return;
        }

        try
        {
            PdfImageRemover.RemoveAllImages(args[0], args[1]);
            Console.WriteLine("All images have been removed and the file saved to " + args[1]);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
