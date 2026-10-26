using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_3d.pdf";
        const string model3dPath = "model.u3d"; // U3D or PRC file

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3D model file not found: {model3dPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define the rectangle that will host the 3D annotation
            Rectangle rect = new Rectangle(100, 500, 400, 800);

            // Load the 3D content (U3D or PRC)
            PDF3DContent content = new PDF3DContent(model3dPath);
            // For U3D files use LoadAsU3D, for PRC use LoadAsPRC
            content.LoadAsU3D(model3dPath);

            // Choose a preset lighting scheme (e.g., Headlamp)
            PDF3DLightingScheme lighting = new PDF3DLightingScheme(LightingSchemeType.Headlamp);

            // Optional render mode – default is Solid; you can change if needed
            PDF3DRenderMode renderMode = new PDF3DRenderMode(RenderModeType.Solid);

            // Create the 3D artwork object linking document, content, lighting and render mode
            PDF3DArtwork artwork = new PDF3DArtwork(pdfDoc, content, lighting, renderMode);

            // Create the 3D annotation. Activation determines when the model becomes visible.
            PDF3DAnnotation threeD = new PDF3DAnnotation(
                pdfDoc.Pages[1],
                rect,
                artwork,
                PDF3DActivation.activeWhenOpen);

            // Style the annotation border (color via Color property, width via Border)
            threeD.Color = Color.LightGray; // border colour
            threeD.Border = new Border(threeD) { Width = 1 };

            // Add the annotation to the first page
            pdfDoc.Pages[1].Annotations.Add(threeD);

            // Embed the 3D stream – required for the model to be saved in the PDF
            PDF3DStream stream = new PDF3DStream(pdfDoc, artwork);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation with custom lighting saved to '{outputPdfPath}'.");
    }
}
