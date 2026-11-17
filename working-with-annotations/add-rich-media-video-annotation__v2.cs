using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputPdfPath = "output.pdf"; // result PDF with rich media
        const string videoPath = "sample.mp4";     // video to embed

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left X/Y, upper‑right X/Y)
            Rectangle rect = new Rectangle(100, 500, 300, 600);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------- Embed video (RichMediaContent) via reflection ----------
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                object richMediaContent = Activator.CreateInstance(richMediaContentType);

                // Try AddFile(string) first
                MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                if (addFileMethod != null)
                {
                    addFileMethod.Invoke(richMediaContent, new object[] { videoPath });
                }
                else
                {
                    // Fallback to AddFile(Stream, string)
                    MethodInfo addFileMethod2 = richMediaContentType.GetMethod("AddFile", new[] { typeof(Stream), typeof(string) });
                    if (addFileMethod2 != null)
                    {
                        using (FileStream videoStream = new FileStream(videoPath, FileMode.Open, FileAccess.Read))
                        {
                            addFileMethod2.Invoke(richMediaContent, new object[] { videoStream, "video/mp4" });
                        }
                    }
                }

                // Assign the content to the annotation
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, richMediaContent);
            }
            else
            {
                Console.Error.WriteLine("RichMediaContent type not found – video will not be embedded.");
            }

            // ---------- Configure activation (double‑click) via reflection ----------
            Type settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            Type activationType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
            Type activationConditionType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivationCondition, Aspose.Pdf");

            if (settingsType != null && activationType != null && activationConditionType != null)
            {
                // Get enum value for DoubleClick
                object doubleClickEnum = Enum.Parse(activationConditionType, "DoubleClick");

                // Create RichMediaActivation instance
                object activationInstance = Activator.CreateInstance(activationType, doubleClickEnum);

                // Create RichMediaSettings instance and set its Activation property
                object settingsInstance = Activator.CreateInstance(settingsType);
                PropertyInfo activationProp = settingsType.GetProperty("Activation");
                activationProp?.SetValue(settingsInstance, activationInstance);

                // Assign settings to the annotation
                PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(richMedia, settingsInstance);
            }
            else
            {
                Console.Error.WriteLine("RichMediaSettings or related types not found – activation will use defaults.");
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Rich media annotation added. Output saved to '{outputPdfPath}'.");
    }
}
