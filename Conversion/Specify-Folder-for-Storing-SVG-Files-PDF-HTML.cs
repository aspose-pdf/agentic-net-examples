using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string svgFolder = "svg_images";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolder);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create HTML save options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

            // Specify the folder where only SVG images will be written
            htmlOpts.SpecialFolderForSvgImages = svgFolder;

            // Save the document as HTML; SVG images will be placed in the folder above
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"HTML saved to '{outputHtml}'. SVG images stored in '{svgFolder}'.");
    }
}