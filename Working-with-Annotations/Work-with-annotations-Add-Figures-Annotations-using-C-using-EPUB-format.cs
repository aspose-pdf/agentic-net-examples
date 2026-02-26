using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputEpub = "output.epub";       // target EPUB

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // Add a figure annotation (SquareAnnotation) to the first page
            // -----------------------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the square (figure) annotation
            SquareAnnotation figure = new SquareAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.LightGray,   // border color
                Contents = "This is a figure annotation.", // tooltip / popup text
                Title    = "Figure 1"                     // title shown in the popup
            };

            // Attach the annotation to the page
            page.Annotations.Add(figure);

            // -----------------------------------------------------------------
            // Save the modified document as EPUB
            // -----------------------------------------------------------------
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                // Use the flow content recognition mode for best re‑flow on e‑readers
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            doc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"EPUB file with figure annotation saved to '{outputEpub}'.");
    }
}