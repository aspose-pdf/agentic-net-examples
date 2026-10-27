using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file (could be any existing PDF)
        const string sourcePdfPath = "source.pdf";

        // Desired output path for the booklet with custom page size
        const string outputPdfPath = "booklet_custom_size.pdf";

        // Ensure the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Open the source PDF as a stream
        using (FileStream sourceStream = new FileStream(sourcePdfPath, FileMode.Open, FileAccess.Read))
        {
            // Create the booklet and save it to the desired output path
            CreateBookletFromStream(sourceStream, outputPdfPath);
        }

        Console.WriteLine($"Booklet created successfully: {outputPdfPath}");
    }

    /// <summary>
    /// Creates a booklet from a PDF stream, sets each page to 5.5×8.5 inches,
    /// and saves the result to the specified output file.
    /// </summary>
    /// <param name="pdfStream">Input PDF stream.</param>
    /// <param name="outputPath">Path where the final PDF will be saved.</param>
    static void CreateBookletFromStream(Stream pdfStream, string outputPath)
    {
        // Temporary files are needed because PdfFileEditor works with file paths.
        string tempInputPath  = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        string tempBookletPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        // Save the incoming stream to a temporary file.
        using (FileStream tempInputFile = new FileStream(tempInputPath, FileMode.Create, FileAccess.Write))
        {
            pdfStream.CopyTo(tempInputFile);
        }

        // Create the booklet using the overload that does NOT require a PageSize argument.
        PdfFileEditor editor = new PdfFileEditor();
        bool bookletCreated = editor.MakeBooklet(tempInputPath, tempBookletPath);
        if (!bookletCreated)
        {
            // Clean up temporary files before throwing.
            File.Delete(tempInputPath);
            throw new InvalidOperationException("Failed to create booklet.");
        }

        // Load the booklet PDF and adjust each page to the custom size (5.5×8.5 inches).
        using (Document doc = new Document(tempBookletPath))
        {
            // 1 inch = 72 points. Convert inches to points.
            const double widthPoints  = 5.5 * 72; // 396 points
            const double heightPoints = 8.5 * 72; // 612 points

            // Pages are 1‑based in Aspose.Pdf.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.SetPageSize(widthPoints, heightPoints);
            }

            // Save the final booklet with the custom page size.
            doc.Save(outputPath);
        }

        // Delete temporary files.
        File.Delete(tempInputPath);
        File.Delete(tempBookletPath);
    }
}