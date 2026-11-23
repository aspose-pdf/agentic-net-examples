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
        const string mediaFile = "sample.mp4"; // path to the multimedia file to embed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Multimedia file not found: {mediaFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create RichMedia (multimedia) annotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // -----------------------------------------------------------------
            // Attach multimedia content using reflection – the RichMediaContent
            // type may not exist in older Aspose.Pdf versions. If it does, we add
            // the file and assign the instance to the annotation.
            // -----------------------------------------------------------------
            var richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                var richMediaContent = Activator.CreateInstance(richMediaContentType);
                var addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                addFileMethod?.Invoke(richMediaContent, new object[] { mediaFile });

                var contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, richMediaContent);
            }

            // -----------------------------------------------------------------
            // Configure activation settings via reflection – RichMediaSettings
            // may also be absent in older releases.
            // -----------------------------------------------------------------
            var richMediaSettingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (richMediaSettingsType != null)
            {
                var settingsInstance = Activator.CreateInstance(richMediaSettingsType);

                // Set Activation = RichMediaActivation.OnPageOpen if the enum exists
                var activationProp = richMediaSettingsType.GetProperty("Activation");
                var activationEnumType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                if (activationProp != null && activationEnumType != null)
                {
                    var onPageOpenValue = Enum.Parse(activationEnumType, "OnPageOpen");
                    activationProp.SetValue(settingsInstance, onPageOpenValue);
                }

                var settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(richMedia, settingsInstance);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Export all annotations (including the multimedia one) to XFDF via a MemoryStream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // The core API method is ExportAnnotationsToXfdf
                doc.ExportAnnotationsToXfdf(xfdfStream);

                // Optionally, retrieve the XFDF as a string (e.g., for logging)
                xfdfStream.Position = 0;
                string xfdfContent = new StreamReader(xfdfStream).ReadToEnd();
                Console.WriteLine("Exported XFDF:");
                Console.WriteLine(xfdfContent);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with multimedia annotation: {outputPdf}");
    }
}
