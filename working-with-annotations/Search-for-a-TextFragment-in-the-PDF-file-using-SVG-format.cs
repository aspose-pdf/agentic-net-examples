using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputSvgPath = "output.svg";
        const string searchPhrase = "hello world";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Create a TextFragmentAbsorber to search for the specified phrase
                Aspose.Pdf.Text.TextFragmentAbsorber absorber = new Aspose.Pdf.Text.TextFragmentAbsorber(searchPhrase);

                // Perform the search on the first page (1‑based indexing)
                doc.Pages[1].Accept(absorber);

                // Output the found fragments
                foreach (Aspose.Pdf.Text.TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"Found fragment: \"{fragment.Text}\"");
                }

                // Save the document as SVG. SVG conversion requires GDI+ on Windows,
                // so wrap it in a try‑catch to handle non‑Windows platforms gracefully.
                Aspose.Pdf.SvgSaveOptions svgOptions = new Aspose.Pdf.SvgSaveOptions();
                try
                {
                    doc.Save(outputSvgPath, svgOptions);
                    Console.WriteLine($"SVG saved to '{outputSvgPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("SVG conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}