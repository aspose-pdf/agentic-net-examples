using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string mediaFile = "sample.mp4"; // path to the media file to embed

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
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Rectangle rect = new Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------- Attach media (if RichMediaContent type exists) ----------
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                // Instantiate RichMediaContent
                object richMediaContent = Activator.CreateInstance(richMediaContentType);
                // Call AddFile(string)
                MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                addFileMethod?.Invoke(richMediaContent, new object[] { mediaFile });
                // Assign to annotation
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, richMediaContent);
            }

            // ---------- Configure playback settings (hide toolbar) ----------
            Type richMediaSettingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (richMediaSettingsType != null)
            {
                object richMediaSettings = Activator.CreateInstance(richMediaSettingsType);
                // Set Toolbar = false if the property exists
                PropertyInfo toolbarProp = richMediaSettingsType.GetProperty("Toolbar");
                if (toolbarProp != null && toolbarProp.CanWrite)
                {
                    toolbarProp.SetValue(richMediaSettings, false);
                }
                // Assign to annotation
                PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(richMedia, richMediaSettings);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdf}'.");
    }
}
