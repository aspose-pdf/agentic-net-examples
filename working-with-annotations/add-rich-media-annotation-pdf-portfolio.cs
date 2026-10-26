using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string portfolioPath = "portfolio.pdf";
        const string outputPath = "output_with_richmedia.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page (1‑based indexing)
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Source PDF contains no pages.");
                return;
            }

            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------------------------------------------------------------------
            // 1) Attach the portfolio file using RichMediaContent (if the type exists)
            // ---------------------------------------------------------------------
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                // Create an instance of RichMediaContent
                object richMediaContent = Activator.CreateInstance(richMediaContentType);
                // Call AddFile(string) to embed the portfolio PDF
                MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                addFileMethod?.Invoke(richMediaContent, new object[] { portfolioPath });
                // Assign the content to the annotation
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, richMediaContent);
            }
            else
            {
                Console.Error.WriteLine("RichMediaContent type not found – the current Aspose.Pdf version does not support embedding files via RichMediaContent.");
            }

            // ---------------------------------------------------------------------
            // 2) Configure initial view (if RichMediaConfiguration type exists)
            // ---------------------------------------------------------------------
            Type configType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
            if (configType != null)
            {
                object configInstance = Activator.CreateInstance(configType);

                // Set Activation = RichMediaActivation.OnPageOpen
                Type activationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                if (activationEnum != null)
                {
                    object onPageOpen = Enum.Parse(activationEnum, "OnPageOpen");
                    PropertyInfo activationProp = configType.GetProperty("Activation");
                    activationProp?.SetValue(configInstance, onPageOpen);
                }

                // Set Instance = RichMediaInstance.Pdf
                Type instanceEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");
                if (instanceEnum != null)
                {
                    object pdfInstance = Enum.Parse(instanceEnum, "Pdf");
                    PropertyInfo instanceProp = configType.GetProperty("Instance");
                    instanceProp?.SetValue(configInstance, pdfInstance);
                }

                // Set InitialFile = file name of the portfolio
                PropertyInfo initialFileProp = configType.GetProperty("InitialFile");
                initialFileProp?.SetValue(configInstance, Path.GetFileName(portfolioPath));

                // Assign the configuration to the annotation
                PropertyInfo configProp = typeof(RichMediaAnnotation).GetProperty("RichMediaConfiguration");
                configProp?.SetValue(richMedia, configInstance);
            }
            else
            {
                Console.Error.WriteLine("RichMediaConfiguration type not found – initial view configuration will be skipped.");
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMedia annotation added. Output saved to '{outputPath}'.");
    }
}
