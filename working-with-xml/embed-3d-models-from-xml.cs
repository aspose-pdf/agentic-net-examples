using System;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlPath = "models.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML containing 3D model references
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate over each <Model> element
            foreach (XElement modelElem in xmlDoc.Root.Elements("Model"))
            {
                // Parse required attributes
                int pageNumber = (int?)modelElem.Attribute("page") ?? 1;
                string modelFile = (string)modelElem.Attribute("file");
                double llx = (double?)modelElem.Attribute("x") ?? 100;
                double lly = (double?)modelElem.Attribute("y") ?? 500;
                double urx = llx + ((double?)modelElem.Attribute("width") ?? 200);
                double ury = lly + ((double?)modelElem.Attribute("height") ?? 200);

                if (string.IsNullOrEmpty(modelFile) || !File.Exists(modelFile))
                {
                    Console.Error.WriteLine($"Model file not found: {modelFile}");
                    continue;
                }

                // Ensure the requested page exists (1‑based indexing rule)
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} out of range.");
                    continue;
                }

                // Get the target page
                Page page = pdfDoc.Pages[pageNumber];

                // Define the annotation rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a RichMediaAnnotation (3D annotation)
                RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

                // -----------------------------------------------------------------
                // Set activation if the property exists (some versions expose it)
                // -----------------------------------------------------------------
                PropertyInfo activationProp = typeof(RichMediaAnnotation).GetProperty("Activation");
                if (activationProp != null && activationProp.CanWrite)
                {
                    // Resolve the enum type (RichMediaActivation) via reflection
                    Type activationEnum = activationProp.PropertyType;
                    // Try to parse the enum value "OnPageOpen"
                    object enumValue = Enum.Parse(activationEnum, "OnPageOpen", ignoreCase: true);
                    activationProp.SetValue(richMedia, enumValue);
                }

                // ---------------------------------------------------------------
                // Attach the 3D model file if RichMediaContent type is available
                // ---------------------------------------------------------------
                Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (richMediaContentType != null)
                {
                    // Create an instance of RichMediaContent
                    object richMediaContent = Activator.CreateInstance(richMediaContentType);

                    // Find an AddFile overload that accepts a Stream and a name (most common)
                    MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(Stream), typeof(string) })
                                            ?? richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });

                    if (addFileMethod != null)
                    {
                        using (FileStream modelStream = File.OpenRead(modelFile))
                        {
                            if (addFileMethod.GetParameters().Length == 2)
                            {
                                // Stream + name overload
                                addFileMethod.Invoke(richMediaContent, new object[] { modelStream, Path.GetFileName(modelFile) });
                            }
                            else
                            {
                                // Single string overload – pass the file path
                                addFileMethod.Invoke(richMediaContent, new object[] { modelFile });
                            }
                        }
                    }

                    // Assign the content to the annotation via the RichMediaContent property (if present)
                    PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    if (contentProp != null && contentProp.CanWrite)
                    {
                        contentProp.SetValue(richMedia, richMediaContent);
                    }
                }
                else
                {
                    // If RichMediaContent type is missing, we still add the annotation without media.
                    Console.Error.WriteLine("RichMediaContent type not found in the current Aspose.Pdf version – 3D model will not be attached.");
                }

                // Add the annotation to the page
                page.Annotations.Add(richMedia);
            }

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"3D annotations processed. Output saved to '{outputPdf}'.");
    }
}
