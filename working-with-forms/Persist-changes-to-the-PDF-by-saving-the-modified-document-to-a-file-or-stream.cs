using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a simple text annotation on the first page
            Page page = doc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Sample annotation added programmatically.",
                Color = Aspose.Pdf.Color.Yellow,
                Open = true,
                Icon = TextIcon.Note
            };
            page.Annotations.Add(txtAnn);

            // Save the modified document to a file (lifecycle: save)
            doc.Save(outputPath); // uses Document.Save(string)

            // Additionally, save the document to a memory stream with explicit save options
            using (MemoryStream ms = new MemoryStream())
            {
                // PdfSaveOptions is a subclass of SaveOptions; using it demonstrates the overload
                PdfSaveOptions saveOpts = new PdfSaveOptions();
                doc.Save(ms, saveOpts); // uses Document.Save(Stream, SaveOptions)

                // The memory stream now contains the PDF bytes; you could write it elsewhere
                // For demonstration, write the stream to another file
                const string streamOutputPath = "output_from_stream.pdf";
                File.WriteAllBytes(streamOutputPath, ms.ToArray());
            }
        }

        Console.WriteLine($"Document saved to '{outputPath}' and also to a stream-derived file.");
    }
}