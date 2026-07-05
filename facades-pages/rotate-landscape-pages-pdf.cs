using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that works directly with a PDF file.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Get total number of pages (1‑based indexing).
            int pageCount = editor.GetPages();

            // Iterate through each page and rotate if it is landscape.
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Retrieve the size of the current page.
                // PageSize.Width and Height are in points (1/72 inch).
                PageSize size = editor.GetPageSize(pageNum);

                // Determine orientation: landscape if width > height.
                if (size.Width > size.Height)
                {
                    // Set the rotation angle (must be 0, 90, 180 or 270).
                    editor.Rotation = 90;

                    // Specify which page to apply the change to.
                    editor.ProcessPages = new int[] { pageNum };

                    // Apply the rotation to the selected page.
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF to the output path.
            editor.Save(outputPath);
            // Close releases any resources held by the facade.
            editor.Close();
        }

        Console.WriteLine($"Pages rotated as needed. Output saved to '{outputPath}'.");
    }
}