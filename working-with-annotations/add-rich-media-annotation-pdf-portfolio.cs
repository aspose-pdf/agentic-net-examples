using System;
using System.IO;
using System.Collections;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // PDF files that will form the portfolio (embedded files)
        string[] portfolioFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        foreach (string f in portfolioFiles)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Portfolio file not found: {f}");
                return;
            }
        }

        // Load the source PDF and add a RichMediaAnnotation
        using (Document doc = new Document(inputPdf))
        {
            // Ensure at least one page exists
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // 1‑based page indexing
            Page page = doc.Pages[1];

            // Define annotation rectangle (lower‑left x,y and upper‑right x,y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation on the page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------- RichMediaConfiguration (if available) ----------
            Type configType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
            if (configType != null)
            {
                object configInstance = Activator.CreateInstance(configType);

                // Try to create RichMediaContent and add files (if the type exists)
                Type contentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (contentType != null)
                {
                    object contentInstance = Activator.CreateInstance(contentType);

                    // RichMediaInstance type
                    Type instanceType = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");
                    if (instanceType != null)
                    {
                        PropertyInfo instancesProp = contentType.GetProperty("Instances");
                        IList instancesList = instancesProp?.GetValue(contentInstance) as IList;

                        foreach (string filePath in portfolioFiles)
                        {
                            object instance = Activator.CreateInstance(instanceType);
                            // Set the File property (FileSpecification)
                            PropertyInfo fileProp = instanceType.GetProperty("File");
                            if (fileProp != null)
                            {
                                fileProp.SetValue(instance, new FileSpecification(filePath));
                            }
                            instancesList?.Add(instance);
                        }
                    }

                    // Add the content to the configuration's Contents collection
                    PropertyInfo contentsProp = configType.GetProperty("Contents");
                    IList contentsList = contentsProp?.GetValue(configInstance) as IList;
                    contentsList?.Add(contentInstance);
                }

                // Assign the configuration to the annotation
                PropertyInfo configProp = typeof(RichMediaAnnotation).GetProperty("RichMediaConfiguration");
                configProp?.SetValue(richMedia, configInstance);
            }

            // ---------- RichMediaSettings (if available) ----------
            Type settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (settingsType != null)
            {
                object settingsInstance = Activator.CreateInstance(settingsType);

                // Set Activation = OnPageOpen
                PropertyInfo activationProp = settingsType.GetProperty("Activation");
                Type activationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings+ActivationEnum, Aspose.Pdf");
                if (activationProp != null && activationEnum != null)
                {
                    object onPageOpen = Enum.Parse(activationEnum, "OnPageOpen");
                    activationProp.SetValue(settingsInstance, onPageOpen);
                }

                // Set StartupMode = First (show first file)
                PropertyInfo startupProp = settingsType.GetProperty("StartupMode");
                Type startupEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings+StartupModeEnum, Aspose.Pdf");
                if (startupProp != null && startupEnum != null)
                {
                    object first = Enum.Parse(startupEnum, "First");
                    startupProp.SetValue(settingsInstance, first);
                }

                // Assign the settings to the annotation
                PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(richMedia, settingsInstance);
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMedia portfolio saved to '{outputPdf}'.");
    }
}
