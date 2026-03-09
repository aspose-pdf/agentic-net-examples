using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a writable stream so that we can use incremental saving later.
        // The stream is opened with ReadWrite access and the original PDF is copied into it.
        using (FileStream writableStream = new FileStream("temp_rw.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            using (FileStream source = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                source.CopyTo(writableStream);
                writableStream.Position = 0; // Reset position for Document constructor.
            }

            // Load the document from the writable stream inside a using block for deterministic disposal.
            using (Document doc = new Document(writableStream))
            {
                // ---------- Edit 1: Add a text annotation on the first page ----------
                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation txtAnn = new TextAnnotation(page, annotRect)
                {
                    Title = "Note",
                    Contents = "Added via Aspose.Pdf",
                    Color = Aspose.Pdf.Color.Yellow,
                    Open = true,
                    Icon = TextIcon.Note
                };
                page.Annotations.Add(txtAnn);

                // ---------- Edit 2: Draw a rectangle shape using Graph ----------
                // Graph is a container for vector shapes.
                Graph graph = new Graph(400, 200);
                Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 100);
                shapeRect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2
                };
                graph.Shapes.Add(shapeRect);
                page.Paragraphs.Add(graph);

                // ---------- Edit 3: Add a text fragment ----------
                TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
                tf.Position = new Position(100, 700);
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 14;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                page.Paragraphs.Add(tf);

                // ---------- Persist changes incrementally ----------
                // Because the document was opened from a writable stream, Save() performs an incremental update.
                doc.Save(); // Incremental save.

                // ---------- Save a copy as HTML (requires explicit HtmlSaveOptions) ----------
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                try
                {
                    doc.Save(outputHtmlPath, htmlOpts);
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion depends on GDI+ and is Windows‑only.
                    Console.WriteLine("HTML conversion requires Windows GDI+. Skipping HTML save.");
                }

                // ---------- Save a regular PDF copy ----------
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine("Document edits saved successfully.");
    }
}