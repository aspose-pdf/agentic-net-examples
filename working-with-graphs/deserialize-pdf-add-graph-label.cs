using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF in memory – this acts as the "input"
        //    PDF so the example does not depend on an external file.
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var seedDoc = new Document())
        {
            // Ensure the document has at least one page.
            seedDoc.Pages.Add();

            // Optional: add a simple text fragment so the seed PDF is not empty.
            var firstPage = seedDoc.Pages[1];
            var placeholder = new TextFragment("Original PDF");
            placeholder.Position = new Position(100, 700);
            placeholder.TextState.FontSize = 14;
            placeholder.TextState.Font = FontRepository.FindFont("Helvetica");
            firstPage.Paragraphs.Add(placeholder);

            // Save the seed document to a byte array.
            using (var ms = new MemoryStream())
            {
                seedDoc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Deserialize the byte array back into a Document instance.
        // ------------------------------------------------------------
        using (var inputStream = new MemoryStream(pdfBytes))
        using (var doc = new Document(inputStream))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Work with the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // --------------------------------------------------------
            // 3. Create a new Graph that spans the whole page and add
            //    vector shapes (rectangle + line).
            // --------------------------------------------------------
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 300f, 600f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            Line line = new Line(new float[] { 100f, 500f, 300f, 600f });
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(line);

            // Attach the graph to the page.
            page.Paragraphs.Add(graph);

            // --------------------------------------------------------
            // 4. Add a text label that points to the newly added graph.
            // --------------------------------------------------------
            TextFragment label = new TextFragment("Sample Graph");
            label.Position = new Position(110, 620); // x, y coordinates
            label.TextState.FontSize = 12;
            label.TextState.Font = FontRepository.FindFont("Helvetica");
            label.TextState.ForegroundColor = Color.Blue;

            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(label);

            // --------------------------------------------------------
            // 5. Save the modified PDF.
            // --------------------------------------------------------
            doc.Save("output.pdf");
        }
    }
}
