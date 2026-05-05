using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // <-- added namespace for PdfContentEditor
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_underline.pdf";
        const string phrase     = "specific phrase"; // phrase to underline

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (Facade) to bind the PDF, modify it, and save it.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPath);

            // Access the underlying Document object.
            Document doc = editor.Document;

            // Ensure the document has at least 4 pages.
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document does not contain a fourth page.");
                return;
            }

            // Get the fourth page (1‑based indexing).
            Page page = doc.Pages[4];

            // Search for the target phrase on the fourth page.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase)
            {
                // Make the search case‑insensitive.
                TextSearchOptions = new TextSearchOptions(true)
            };
            page.Accept(absorber);

            // If the phrase is not found, inform the user.
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine($"Phrase \"{phrase}\" not found on page 4.");
                // Still save the original document (no annotation added).
                editor.Save(outputPath);
                return;
            }

            // For each occurrence, create an underline annotation.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The rectangle of the text fragment defines where the underline will appear.
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create the underline annotation.
                UnderlineAnnotation underline = new UnderlineAnnotation(page, rect)
                {
                    Color = Aspose.Pdf.Color.Red,
                    Contents = $"Underline for \"{phrase}\""
                };

                // Add the annotation to the page.
                page.Annotations.Add(underline);
            }

            // Save the modified PDF using the facade.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
    }
}
