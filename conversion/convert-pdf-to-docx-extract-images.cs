using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";                 // source PDF
        const string outputDocxPath = "output.docx";             // converted DOCX
        const string imagesOutputDir = "ExtractedImages";        // folder for images

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        // ------------------------------------------------------------
        // Make sure a source PDF is available. If the file is missing we
        // create a minimal PDF on‑the‑fly so the sample can run without
        // external resources. This eliminates the FileNotFoundException
        // that caused the original crash.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document placeholder = new Document())
            {
                // Add a single blank page
                Page page = placeholder.Pages.Add();

                // Optionally add a simple shape so the PDF is not completely empty
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
                rect.GraphInfo.Color = Aspose.Pdf.Color.Blue;
                rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;
                graph.Shapes.Add(rect);
                page.Paragraphs.Add(graph);

                placeholder.Save(inputPdfPath);
                Console.WriteLine($"Placeholder PDF created at '{inputPdfPath}'.");
            }
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ---------- Convert PDF to DOCX ----------
            // Configure DOCX save options (use Flow mode for better editability)
            var docSaveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX,
                Mode = DocSaveOptions.RecognitionMode.Flow
            };
            pdfDoc.Save(outputDocxPath, docSaveOptions);

            // ---------- Extract embedded images ----------
            int imageIndex = 0;
            foreach (Page page in pdfDoc.Pages) // 1‑based page indexing
            {
                // Iterate over all images defined in the page resources
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;
                    // Build a unique file name for each extracted image
                    string imagePath = System.IO.Path.Combine(imagesOutputDir,
                        $"image_{imageIndex}.png"); // PNG is a safe default

                    // Save the image to the file system using a stream overload
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }
                }
            }
        }

        Console.WriteLine("Conversion to DOCX completed and images extracted.");
    }
}
