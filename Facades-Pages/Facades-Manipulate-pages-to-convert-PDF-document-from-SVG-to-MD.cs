using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using System.Drawing.Imaging;

class SvgToMarkdownConverter
{
    static void Main()
    {
        // Input SVG file (treated as a PDF source)
        const string svgInputPath = "input.svg";

        // Output markdown file
        const string markdownOutputPath = "output.md";

        // Folder to store generated page images
        const string imagesFolder = "PageImages";

        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"SVG input file not found: {svgInputPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        // Load the SVG file as a PDF document using SvgLoadOptions
        using (Document pdfDoc = new Document(svgInputPath, new SvgLoadOptions()))
        {
            // -----------------------------------------------------------------
            // Step 1: Convert each PDF page (originating from the SVG) to PNG
            // -----------------------------------------------------------------
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(pdfDoc);
            converter.DoConvert();

            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string imagePath = Path.Combine(imagesFolder, $"page_{pageNumber}.png");
                // Save the current page as PNG
                converter.GetNextImage(imagePath, ImageFormat.Png);
                pageNumber++;
            }

            // -----------------------------------------------------------------
            // Step 2: Extract textual content (if any) from the PDF
            // -----------------------------------------------------------------
            TextAbsorber absorber = new TextAbsorber();
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // -----------------------------------------------------------------
            // Step 3: Build Markdown content
            // -----------------------------------------------------------------
            using (StreamWriter mdWriter = new StreamWriter(markdownOutputPath, false))
            {
                // Write extracted text (if present)
                if (!string.IsNullOrWhiteSpace(extractedText))
                {
                    mdWriter.WriteLine(extractedText);
                    mdWriter.WriteLine(); // blank line before images
                }

                // Insert image references for each page
                for (int i = 1; i < pageNumber; i++)
                {
                    string relativeImagePath = Path.Combine(imagesFolder, $"page_{i}.png");
                    mdWriter.WriteLine($"![Page {i}]({relativeImagePath})");
                    mdWriter.WriteLine(); // separate images with a blank line
                }
            }

            // Cleanup the converter (optional, as it does not implement IDisposable)
            converter.Close();
        }

        Console.WriteLine($"Markdown file generated at '{markdownOutputPath}'.");
    }
}