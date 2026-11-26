using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the PDF from an XML representation
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Locate the first Graph object in the document
            Graph targetGraph = null;
            foreach (Page page in doc.Pages)
            {
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Graph graph)
                    {
                        targetGraph = graph;
                        break;
                    }
                }
                if (targetGraph != null) break;
            }

            if (targetGraph == null)
            {
                Console.Error.WriteLine("No Graph object found in the document.");
                return;
            }

            // Create a new Circle shape (center at (100,100), radius 50)
            Circle newCircle = new Circle(100, 100, 50);
            // Styling for the circle – use GraphInfo.Color for stroke and LineWidth for border width
            newCircle.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.DarkGray,   // stroke color
                LineWidth = 1             // border width
            };

            // Add the circle to the existing Graph
            targetGraph.Shapes.Add(newCircle);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
