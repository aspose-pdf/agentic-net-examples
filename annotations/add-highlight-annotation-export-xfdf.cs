using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Add a highlight annotation on the first page
            // Rectangle coordinates: lower‑left x, lower‑left y, upper‑right x, upper‑right y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            HighlightAnnotation highlight = new HighlightAnnotation(pdfDoc.Pages[1], rect)
            {
                Color = Aspose.Pdf.Color.Yellow // set highlight colour
            };
            pdfDoc.Pages[1].Annotations.Add(highlight);

            // Export highlight annotations to XFDF using a MemoryStream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // The ExportAnnotationsToXfdf method writes XFDF for all annotations in the document.
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);

                // Reset stream position to read the XFDF content (optional)
                xfdfStream.Position = 0;
                using (StreamReader reader = new StreamReader(xfdfStream))
                {
                    string xfdfContent = reader.ReadToEnd();
                    Console.WriteLine("XFDF content of highlight annotations:");
                    Console.WriteLine(xfdfContent);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with highlight annotation to '{outputPath}'.");
    }
}
