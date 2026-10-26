using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "RichMediaFlash.pdf";
        const string flashPath = "sample.swf"; // path to the Flash video file

        if (!File.Exists(flashPath))
        {
            Console.Error.WriteLine($"Flash file not found: {flashPath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (page 1) to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the RichMedia annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMedia annotation on page 1
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ----- Build RichMedia content (embed the Flash file) using reflection -----
            Type contentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            Type instanceType = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");

            if (contentType != null && instanceType != null)
            {
                // Create RichMediaContent instance
                object richMediaContent = Activator.CreateInstance(contentType);

                // Create RichMediaInstance and set its properties
                object richMediaInstance = Activator.CreateInstance(instanceType);
                var mimeProp = instanceType.GetProperty("MimeType");
                mimeProp?.SetValue(richMediaInstance, "application/x-shockwave-flash");
                var fileProp = instanceType.GetProperty("File");
                fileProp?.SetValue(richMediaInstance, new FileSpecification(flashPath));

                // Add the instance to the content collection
                var addInstanceMethod = contentType.GetMethod("AddInstance", new[] { instanceType });
                addInstanceMethod?.Invoke(richMediaContent, new[] { richMediaInstance });

                // Assign the content to the annotation (if the property exists)
                var contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, richMediaContent);
            }

            // ----- Configure playback controls (if RichMediaSettings type is available) -----
            Type settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (settingsType != null)
            {
                object settings = Activator.CreateInstance(settingsType);

                var activationProp = settingsType.GetProperty("Activation");
                var toolbarProp = settingsType.GetProperty("Toolbar");
                var presentationProp = settingsType.GetProperty("Presentation");

                Type activationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                Type toolbarEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaToolbar, Aspose.Pdf");
                Type presentationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaPresentation, Aspose.Pdf");

                if (activationEnum != null && toolbarEnum != null && presentationEnum != null)
                {
                    activationProp?.SetValue(settings, Enum.Parse(activationEnum, "OnPageOpen"));
                    toolbarProp?.SetValue(settings, Enum.Parse(toolbarEnum, "Visible"));
                    presentationProp?.SetValue(settings, Enum.Parse(presentationEnum, "Fit"));
                }

                var settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(richMedia, settings);
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMedia PDF with embedded Flash saved to '{outputPath}'.");
    }
}
