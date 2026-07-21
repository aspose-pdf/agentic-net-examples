using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "popup_extracted.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the source PDF
        using (Document srcDoc = new Document(inputPdf))
        {
            PopupAnnotation popup = null;

            // Search for the first PopupAnnotation in the document
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                Page page = srcDoc.Pages[i];
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];
                    if (ann is PopupAnnotation pa)
                    {
                        popup = pa;
                        break;
                    }
                }
                if (popup != null) break;
            }

            if (popup == null)
            {
                Console.WriteLine("No PopupAnnotation found in the document.");
                return;
            }

            // Create a new PDF that will contain the popup's contents
            using (Document newDoc = new Document())
            {
                // Add a blank page
                Page newPage = newDoc.Pages.Add();

                // Add the popup text as a TextFragment
                TextFragment tf = new TextFragment(popup.Contents ?? string.Empty)
                {
                    // Position the text near the top-left of the page
                    Position = new Position(50, newPage.PageInfo.Height - 50)
                };
                newPage.Paragraphs.Add(tf);

                // Optionally, preserve the open state as a visual cue
                if (popup.Open)
                {
                    // Create a rectangle that mimics the popup bounds
                    var drawRect = new Aspose.Pdf.Drawing.Rectangle(
                        (float)popup.Rect.LLX,
                        (float)popup.Rect.LLY,
                        (float)(popup.Rect.URX - popup.Rect.LLX),
                        (float)(popup.Rect.URY - popup.Rect.LLY));

                    drawRect.GraphInfo = new GraphInfo
                    {
                        Color = Color.LightGray,
                        LineWidth = 1f
                    };

                    // Graph constructor expects double values
                    Graph graph = new Graph(400.0, 200.0);
                    graph.Shapes.Add(drawRect);
                    newPage.Paragraphs.Add(graph);
                }

                // Save the new PDF containing the popup annotation data
                newDoc.Save(outputPdf);
            }

            Console.WriteLine($"Popup annotation extracted and saved to '{outputPdf}'.");
        }
    }
}
