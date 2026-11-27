using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Height of the footer area (in points). Adjust as needed.
            const double footerHeight = 50.0;

            System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder();

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                double pageWidth = page.PageInfo.Width;

                // Define a rectangle that covers the footer region of the current page
                // (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, footerHeight);

                // Create a TextFragmentAbsorber limited to the footer rectangle
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                absorber.TextSearchOptions = new TextSearchOptions(footerRect);

                // Extract text from the current page's footer
                page.Accept(absorber);

                // Append extracted text to the result
                resultBuilder.AppendLine($"--- Page {i} Footer ---");
                resultBuilder.AppendLine(absorber.Text);
            }

            // Write the collected footer text to a plain‑text file
            File.WriteAllText(outputPath, resultBuilder.ToString());
        }

        Console.WriteLine($"Footer text extracted to '{outputPath}'.");
    }
}
