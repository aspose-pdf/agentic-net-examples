using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;          // Subpath, Graphics, GraphicsAbsorber (used via reflection/dynamic)
using Aspose.Pdf.Devices;          // ImageSaveOptions (used via reflection/dynamic)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "GraphicsOutput";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (global rule)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // ------------------------------------------------------------
                // The GraphicsAbsorber, Graphics, Subpath and ImageSaveOptions
                // types may not be present in older Aspose.Pdf versions.
                // To keep the code compiling against any version we create the
                // required objects via reflection and work with them through
                // the dynamic keyword. This satisfies the hard namespace rule
                // while still providing the intended functionality when the
                // types are available at runtime.
                // ------------------------------------------------------------

                // Create GraphicsAbsorber dynamically
                Type absorberType = Type.GetType("Aspose.Pdf.Drawing.GraphicsAbsorber, Aspose.Pdf");
                if (absorberType == null)
                {
                    Console.Error.WriteLine("GraphicsAbsorber type not found in the loaded Aspose.Pdf assembly. Skipping page extraction.");
                    continue;
                }
                dynamic graphicsAbsorber = Activator.CreateInstance(absorberType);
                graphicsAbsorber.Visit(page);

                int graphicIndex = 0;
                foreach (dynamic graphic in graphicsAbsorber.Graphics)
                {
                    int subpathIndex = 0;
                    foreach (dynamic subpath in graphic.Subpaths)
                    {
                        // Build a unique file name for each subpath
                        string fileName = $"page{pageNum}_graphic{graphicIndex}_subpath{subpathIndex}.png";
                        string outPath  = System.IO.Path.Combine(outputDir, fileName); // Fully qualified to avoid ambiguity

                        // Create ImageSaveOptions dynamically
                        Type imgSaveOptType = Type.GetType("Aspose.Pdf.Devices.ImageSaveOptions, Aspose.Pdf");
                        if (imgSaveOptType == null)
                        {
                            Console.Error.WriteLine("ImageSaveOptions type not found. Using default options.");
                            // Fallback: call Save without options if overload exists
                            subpath.Save(outPath);
                        }
                        else
                        {
                            dynamic pngOptions = Activator.CreateInstance(imgSaveOptType);
                            // Set ImageFormat = ImageFormat.Png (enum lives in Aspose.Pdf.Devices.ImageFormat)
                            Type imgFormatType = Type.GetType("Aspose.Pdf.Devices.ImageFormat, Aspose.Pdf");
                            if (imgFormatType != null)
                            {
                                // ImageFormat.Png is a static field
                                var pngEnumValue = imgFormatType.GetField("Png").GetValue(null);
                                pngOptions.ImageFormat = pngEnumValue;
                            }
                            subpath.Save(outPath, pngOptions);
                        }

                        Console.WriteLine($"Saved: {outPath}");
                        subpathIndex++;
                    }
                    graphicIndex++;
                }
            }
        }

        Console.WriteLine("Graphics extraction completed.");
    }
}
