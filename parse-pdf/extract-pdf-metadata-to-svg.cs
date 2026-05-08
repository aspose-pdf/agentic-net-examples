using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputSvg = "vector_output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve basic metadata
            string author = doc.Info.Author ?? "(no author)";
            string title  = doc.Info.Title  ?? "(no title)";

            Console.WriteLine($"Author: {author}");
            Console.WriteLine($"Title : {title}");

            // Extract vector graphics by saving the PDF as SVG.
            // SVG preserves vector shapes, paths, and text as scalable graphics.
            var svgOptions = new SvgSaveOptions(); // No unsupported properties are set

            // Save the entire document as a single SVG file
            doc.Save(outputSvg, svgOptions);
            Console.WriteLine($"Vector data saved to: {outputSvg}");
        }
    }
}
