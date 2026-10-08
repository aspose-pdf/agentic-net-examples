using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Devices;

// Alias to disambiguate System.IO.Path from Aspose.Pdf.Drawing.Path
using IOPath = System.IO.Path;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Subpaths";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document srcDoc = new Document(inputPdf))
        {
            // For demonstration, process the first page only.
            Page srcPage = srcDoc.Pages[1];

            int subpathIndex = 1;

            // Iterate over all paragraphs on the page and look for Graph containers.
            foreach (var paragraph in srcPage.Paragraphs)
            {
                if (paragraph is Aspose.Pdf.Drawing.Graph srcGraph)
                {
                    // Inside a Graph, look for Path shapes.
                    foreach (var shape in srcGraph.Shapes)
                    {
                        if (shape is Aspose.Pdf.Drawing.Path srcPath)
                        {
                            // Create a temporary PDF that will contain only this Path.
                            using (Document subDoc = new Document())
                            {
                                Page newPage = subDoc.Pages.Add();

                                // Create a new Graph that matches the page size.
                                var newGraph = new Aspose.Pdf.Drawing.Graph(newPage.PageInfo.Width, newPage.PageInfo.Height);

                                // Clone the source Path (shallow copy of visual properties).
                                var newPath = new Aspose.Pdf.Drawing.Path
                                {
                                    GraphInfo = srcPath.GraphInfo
                                };

                                // Copy all segments from the source Path to the new Path.
                                // The Segments collection exists in recent versions; if unavailable, this line can be omitted.
                                // The code is kept for completeness and will compile when the property is present.
                                // newPath.Segments.AddRange(srcPath.Segments);

                                // If the Segments property is not available in the target version, add the whole source Path instead.
                                // This fallback ensures the example compiles across versions.
                                if (srcPath.GetType().GetProperty("Segments") != null)
                                {
                                    // Use reflection to copy segments when the property exists.
                                    var srcSegments = (System.Collections.IEnumerable)srcPath.GetType().GetProperty("Segments").GetValue(srcPath);
                                    var newSegmentsProp = newPath.GetType().GetProperty("Segments");
                                    var newSegments = (System.Collections.IList)newSegmentsProp.GetValue(newPath);
                                    foreach (var seg in srcSegments)
                                    {
                                        newSegments.Add(seg);
                                    }
                                }
                                else
                                {
                                    // Fallback: add the original path as a whole.
                                    newGraph.Shapes.Add(srcPath);
                                }

                                // If we successfully copied segments, add the new Path to the graph.
                                if (newPath.GetType().GetProperty("Segments") != null)
                                    newGraph.Shapes.Add(newPath);

                                // Add the Graph to the page.
                                newPage.Paragraphs.Add(newGraph);

                                // Prepare the output PNG file path.
                                string outFile = IOPath.Combine(outputDir, $"subpath_{subpathIndex}.png");

                                // Render the page to PNG with a transparent background.
                                using (FileStream outStream = File.Create(outFile))
                                {
                                    var pngDevice = new PngDevice(new Resolution(300))
                                    {
                                        TransparentBackground = true
                                    };
                                    pngDevice.Process(newPage, outStream);
                                }

                                Console.WriteLine($"Exported subpath {subpathIndex} to '{outFile}'.");
                                subpathIndex++;
                            }
                        }
                    }
                }
            }
        }
    }
}
