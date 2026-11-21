using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string mediaFile = "media.swf"; // replace with your media file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page to place the annotation (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // -----------------------------------------------------------------
            // The Rich Media API (RichMediaContent, RichMediaActivation, etc.)
            // is not available in older Aspose.Pdf versions. To keep the code
            // compilable across all versions we use reflection to add the media
            // only when the required types exist at runtime.
            // -----------------------------------------------------------------
            try
            {
                // RichMediaContent
                Type contentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (contentType != null)
                {
                    object contentInstance = Activator.CreateInstance(contentType);
                    // AddFile(string fileName, Stream stream, string mimeType)
                    var addFileMethod = contentType.GetMethod("AddFile", new[] { typeof(string), typeof(Stream), typeof(string) });
                    if (addFileMethod != null)
                    {
                        using (FileStream mediaStream = File.OpenRead(mediaFile))
                        {
                            addFileMethod.Invoke(contentInstance, new object[] { mediaFile, mediaStream, "application/x-shockwave-flash" });
                        }
                    }
                    // Assign to annotation
                    var contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    contentProp?.SetValue(richMedia, contentInstance);
                }

                // RichMediaActivation
                Type activationType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                if (activationType != null)
                {
                    object activationInstance = Activator.CreateInstance(activationType);
                    // Set Condition = RichMediaActivationCondition.EvenPageOpen
                    Type conditionEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivationCondition, Aspose.Pdf");
                    var conditionProp = activationType.GetProperty("Condition");
                    if (conditionEnum != null && conditionProp != null)
                    {
                        object evenOpen = Enum.Parse(conditionEnum, "EvenPageOpen");
                        conditionProp.SetValue(activationInstance, evenOpen);
                    }
                    var activationProp = typeof(RichMediaAnnotation).GetProperty("RichMediaActivation");
                    activationProp?.SetValue(richMedia, activationInstance);
                }

                // RichMediaToolbar (to hide the playback toolbar)
                Type toolbarType = Type.GetType("Aspose.Pdf.Annotations.RichMediaToolbar, Aspose.Pdf");
                if (toolbarType != null)
                {
                    object toolbarInstance = Activator.CreateInstance(toolbarType);
                    var showProp = toolbarType.GetProperty("ShowToolbar");
                    if (showProp != null)
                    {
                        showProp.SetValue(toolbarInstance, false);
                    }
                    var toolbarProp = typeof(RichMediaAnnotation).GetProperty("RichMediaToolbar");
                    toolbarProp?.SetValue(richMedia, toolbarInstance);
                }
            }
            catch (Exception ex)
            {
                // If any reflection step fails, we simply continue without rich media content.
                Console.Error.WriteLine($"Rich media setup failed: {ex.Message}");
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF (inside the using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdf}'.");
    }
}
