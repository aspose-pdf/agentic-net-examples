using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document must contain at least 3 pages.");
                return;
            }

            // Helper to create a CaretAnnotation on a given page
            void AddCaretAnnotation(int pageNumber, string title, string contents, Aspose.Pdf.Rectangle rect)
            {
                // Pages are 1‑based in Aspose.Pdf
                Page page = doc.Pages[pageNumber];

                // Create the annotation with the specified rectangle
                CaretAnnotation caret = new CaretAnnotation(page, rect)
                {
                    Title    = title,      // Distinct title for each annotation
                    Contents = contents,   // Optional visible text when the annotation is opened
                    // Optional appearance settings can be added here
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(caret);
            }

            // Define rectangles for each annotation (coordinates are in points)
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 150, 750);
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(200, 600, 250, 650);
            Aspose.Pdf.Rectangle rect3 = new Aspose.Pdf.Rectangle(300, 500, 350, 550);

            // Add caret annotations to pages 1, 2 and 3 with distinct titles
            AddCaretAnnotation(1, "Caret on Page 1", "Note for page 1", rect1);
            AddCaretAnnotation(2, "Caret on Page 2", "Note for page 2", rect2);
            AddCaretAnnotation(3, "Caret on Page 3", "Note for page 3", rect3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Caret annotations added and saved to '{outputPath}'.");
    }
}