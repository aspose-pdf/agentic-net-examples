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

        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Load the PDF and add a sticky (text) annotation
        using (Document doc = new Document(inputPath))
        {
            // First page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            var rect = new Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (sticky note) and set its properties
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "This is a sticky note.",
                Icon = TextIcon.Comment, // sticky‑note icon
                Open = true
            };

            // Add the annotation to the page
            page.Annotations.Add(sticky);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Export sticky annotations to XFDF using a MemoryStream
        using (Document docWithSticky = new Document(outputPath))
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            // Export annotations to XFDF stream
            docWithSticky.ExportAnnotationsToXfdf(xfdfStream);

            // Prepare the stream for reading
            xfdfStream.Position = 0;

            // Retrieve the XFDF content as a string
            using (StreamReader reader = new StreamReader(xfdfStream))
            {
                string xfdfContent = reader.ReadToEnd();
                Console.WriteLine("XFDF content:");
                Console.WriteLine(xfdfContent);
            }
        }
    }
}
