using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Desired output HTML file path
        const string outputHtmlPath = "output.html";

        // Folder where generated SVG images will be saved
        const string svgImagesFolder = "svg_images";

        // Ensure the SVG images folder exists
        Directory.CreateDirectory(svgImagesFolder);

        // ---------------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a minimal sample PDF
        // so the conversion can proceed without a FileNotFoundException.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input file '{inputPdfPath}' not found. Creating a sample PDF.");
            using (Document sampleDoc = new Document())
            {
                // Add a single page with a simple text fragment
                Page page = sampleDoc.Pages.Add();
                TextFragment tf = new TextFragment("This is a sample PDF generated because the original input file was missing.");
                tf.Position = new Position(50, 750);
                page.Paragraphs.Add(tf);

                // Save the sample PDF to the expected path
                sampleDoc.Save(inputPdfPath);
            }
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Specify the folder for SVG images generated during conversion
            htmlOptions.SpecialFolderForSvgImages = svgImagesFolder;

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML. SVG images are stored in '{svgImagesFolder}'.");
    }
}
