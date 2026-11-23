using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Tagged; // needed for ITaggedContent

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string mediaPath = "sample.mp4"; // path to multimedia file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Multimedia file not found: {mediaPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 9 pages
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document has fewer than 9 pages.");
                return;
            }

            // ---------- Add a Figure element (tagged PDF) ----------
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title for accessibility (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree
            var root = taggedContent.RootElement;

            // Create a Figure element, set its alternative text, and attach it to the root
            var figure = taggedContent.CreateFigureElement();
            figure.AlternativeText = "Sample figure describing the content on page 9.";
            root.AppendChild(figure);

            // ---------- Add a Rich Media (multimedia) annotation to page 9 ----------
            Page page9 = doc.Pages[9]; // 1‑based indexing

            // Define the annotation rectangle (coordinates in points)
            // llx, lly, urx, ury = lower‑left x/y and upper‑right x/y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page9, rect);

            // Try to create and assign RichMediaContent via reflection (available in newer versions only)
            // If the types are not present, the annotation will be added without embedded media.
            try
            {
                // RichMediaContent type
                Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (richMediaContentType != null)
                {
                    // Create instance of RichMediaContent
                    object richMediaContent = Activator.CreateInstance(richMediaContentType);

                    // Add the media file (method name is AddFile in the API)
                    var addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                    addFileMethod?.Invoke(richMediaContent, new object[] { mediaPath });

                    // Create activation configuration (optional – play on page open)
                    Type activationType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                    Type activationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation+ActivationType, Aspose.Pdf");
                    object activationInstance = null;
                    if (activationType != null && activationEnum != null)
                    {
                        // Get enum value for PageOpen (0)
                        var pageOpenValue = Enum.Parse(activationEnum, "PageOpen");
                        activationInstance = Activator.CreateInstance(activationType, new object[] { pageOpenValue });
                    }

                    // Create configuration and add activation
                    Type configType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
                    if (configType != null && activationInstance != null)
                    {
                        object configInstance = Activator.CreateInstance(configType);
                        var addActivationMethod = configType.GetMethod("AddActivation");
                        addActivationMethod?.Invoke(configInstance, new object[] { activationInstance });

                        // Assign configuration to content
                        var configProp = richMediaContentType.GetProperty("Configuration");
                        configProp?.SetValue(richMediaContent, configInstance);
                    }

                    // Finally assign the content to the annotation
                    var richMediaContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    richMediaContentProp?.SetValue(richMedia, richMediaContent);
                }
            }
            catch (Exception ex)
            {
                // If anything goes wrong, just continue without embedded media.
                Console.Error.WriteLine($"Rich media setup failed: {ex.Message}");
            }

            // Add the annotation to the page
            page9.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with figure and multimedia annotation: {outputPath}");
    }
}
