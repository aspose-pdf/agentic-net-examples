using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputMdPath = "output.md";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use TextFragmentAbsorber to get individual text fragments with position info.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Extract fragments from all pages.
            pdfDoc.Pages.Accept(absorber);
            TextFragmentCollection fragments = absorber.TextFragments;

            if (fragments == null || fragments.Count == 0)
            {
                Console.WriteLine("No text found in the document.");
                return;
            }

            // Determine the minimum X coordinate (leftmost position) to calculate indentation.
            double minX = double.MaxValue;
            foreach (TextFragment fragment in fragments)
            {
                // Use the fragment's bounding rectangle to obtain the left (LLX) coordinate.
                if (fragment.Rectangle != null && fragment.Rectangle.LLX < minX)
                    minX = fragment.Rectangle.LLX;
            }

            // Define how many points correspond to one indentation level.
            // Adjust this value based on the PDF's layout (e.g., 5 points ≈ one space).
            const double pointsPerSpace = 5.0;

            StringBuilder markdownBuilder = new StringBuilder();

            // Build markdown lines preserving indentation.
            foreach (TextFragment fragment in fragments)
            {
                // Skip empty fragments.
                if (string.IsNullOrWhiteSpace(fragment.Text))
                    continue;

                // Calculate indentation based on horizontal offset.
                int indentSpaces = 0;
                if (fragment.Rectangle != null)
                {
                    double offset = fragment.Rectangle.LLX - minX;
                    if (offset > 0)
                        indentSpaces = (int)Math.Round(offset / pointsPerSpace);
                }

                // Append spaces for indentation, then the fragment text.
                markdownBuilder.Append(' ', indentSpaces);
                markdownBuilder.AppendLine(fragment.Text.TrimEnd());
            }

            // Write the resulting markdown to a file.
            File.WriteAllText(outputMdPath, markdownBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"Markdown file created: {outputMdPath}");
        }
    }
}
