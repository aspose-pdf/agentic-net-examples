using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "highlight"; // text to locate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find the first occurrence of the target text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            Rectangle targetRect = null;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (!string.IsNullOrEmpty(fragment.Text) && fragment.Text.Contains(searchText))
                {
                    targetRect = fragment.Rectangle; // rectangle of the found text fragment
                    break;
                }
            }

            if (targetRect != null)
            {
                // Create a line annotation that spans the rectangle of the found text
                var lineAnnot = new LineAnnotation(
                    doc.Pages[1],
                    new Rectangle(
                        targetRect.LLX,
                        targetRect.LLY,
                        targetRect.URX,
                        targetRect.URY),
                    new Point(targetRect.LLX, targetRect.LLY),
                    new Point(targetRect.URX, targetRect.URY));

                // Set custom appearance: red color and 2‑point thickness
                lineAnnot.Color = Color.FromRgb(1.0, 0.0, 0.0); // red
                lineAnnot.Width = 2; // line thickness

                // (Optional) Explicitly set the start and end points – they are already set by the constructor
                lineAnnot.Starting = new Point(targetRect.LLX, targetRect.LLY);
                lineAnnot.Ending   = new Point(targetRect.URX, targetRect.URY);

                // Add the annotation to the page
                doc.Pages[1].Annotations.Add(lineAnnot);
            }
            else
            {
                Console.WriteLine($"Text \"{searchText}\" not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}
